using Application.Core;
using MediatR;

namespace Application.Profiles.Queries
{
    public class UserDetailsQuery : IRequest<Result<Profile>>
    {
        public string UserName { get; }

        public UserDetailsQuery(string userName)
        {
            UserName = userName;
        }
    }
}