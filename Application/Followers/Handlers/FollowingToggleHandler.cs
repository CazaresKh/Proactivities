using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Followers.Commands;
using Application.Integration;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Followers.Handlers
{
    public class FollowingToggleHandler : BaseHandler, IRequestHandler<FollowingToggleCommand, Result<Unit>>
    {
        public FollowingToggleHandler(DataContext context, IUserAccessor userAccessor) : base(context, userAccessor)
        {
        }

        public async Task<Result<Unit>> Handle(FollowingToggleCommand request, CancellationToken cancellationToken)
        {
            var observer = await Context.Users
                .FirstOrDefaultAsync(x => x.UserName == UserAccessor.GetUserName(), cancellationToken);

            var target = await Context.Users
                .FirstOrDefaultAsync(x => x.UserName == request.TargetUserName, cancellationToken);

            if (target == null)
            {
                return null;
            }

            var following = await Context.UserFollowings.FindAsync(observer.Id, target.Id);

            if (following == null)
            {
                following = new UserFollowing
                {
                    Observer = observer,
                    Target = target
                };
                Context.UserFollowings.Add(following);
            }
            else
            {
                Context.UserFollowings.Remove(following);
            }

            var success = await Context.SaveChangesAsync(cancellationToken) > 0;

            return success ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to update following");
        }
    }
}