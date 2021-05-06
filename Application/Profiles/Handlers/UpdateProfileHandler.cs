using Application.Core;
using Application.Integration;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Profiles.Commands;
using Microsoft.EntityFrameworkCore;

namespace Application.Profiles.Handlers
{
    public class UpdateProfileHandler : SupportHandler, IRequestHandler<UpdateProfileCommand, Result<Unit>>
    {
        public UpdateProfileHandler(DataContext context, IUserAccessor userAccessor, IPhotoAccessor photoAccessor) :
            base(context, userAccessor, photoAccessor)
        {
        }

        public async Task<Result<Unit>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await Context.Users.FirstOrDefaultAsync(x =>
                x.UserName == UserAccessor.GetUserName(), cancellationToken);

            user.Bio = request.Bio ?? user.Bio;
            user.DisplayName = request.DisplayName ?? user.DisplayName;

            Context.Entry(user).State = EntityState.Modified;

            var success = await Context.SaveChangesAsync(cancellationToken) > 0;

            return success ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem updating profile");
        }
    }
}