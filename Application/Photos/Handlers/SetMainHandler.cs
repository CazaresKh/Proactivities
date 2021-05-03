using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Integration;
using Application.Interfaces;
using Application.Photos.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos.Handlers
{
    public class SetMainHandler : SupportHandler, IRequestHandler<SetMainCommand, Result<Unit>>
    {
        public SetMainHandler(DataContext context, IUserAccessor userAccessor, IPhotoAccessor photoAccessor) : base(
            context, userAccessor, photoAccessor)
        {
        }

        public async Task<Result<Unit>> Handle(SetMainCommand request, CancellationToken cancellationToken)
        {
            var user = await Context.Users.Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.UserName == UserAccessor.GetUserName(), cancellationToken);

            var photo = user?.Photos.FirstOrDefault(x => x.Id == request.Id);

            if (photo == null)
            {
                return null;
            }

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

            if (currentMain != null)
            {
                currentMain.IsMain = false;
            }

            photo.IsMain = true;

            var success = await Context.SaveChangesAsync(cancellationToken) > 0;

            return success ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem setting main photo");
        }
    }
}