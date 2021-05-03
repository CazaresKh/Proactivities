using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Integration;
using Application.Interfaces;
using Application.Photos.Commands;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos.Handlers
{
    public class AddPhotoHandler : SupportHandler, IRequestHandler<PhotoAddCommand, Result<Photo>>
    {
        public AddPhotoHandler(DataContext context, IPhotoAccessor photoAccessor, IUserAccessor userAccessor) :
            base(context, userAccessor, photoAccessor)
        {
        }

        public async Task<Result<Photo>> Handle(PhotoAddCommand request, CancellationToken cancellationToken)
        {
            var user = await Context.Users.Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.UserName == UserAccessor.GetUserName(),
                    cancellationToken: cancellationToken);

            if (user == null)
            {
                return null;
            }

            var photoUploadResult = await PhotoAccessor.AddPhoto(request.File);

            var photo = new Photo
            {
                Url = photoUploadResult.Url,
                Id = photoUploadResult.PublicId
            };

            if (!user.Photos.Any(x => x.IsMain))
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            var result = await Context.SaveChangesAsync(cancellationToken) > 0;

            return result ? Result<Photo>.Success(photo) : Result<Photo>.Failure("Problem adding photo");
        }
    }
}