using Application.Core;
using MediatR;

namespace Application.Followers.Commands
{
    public class FollowingToggleCommand : IRequest<Result<Unit>>
    {
        public string TargetUserName { get; }

        public FollowingToggleCommand(string username)
        {
            TargetUserName = username;
        }
    }
}