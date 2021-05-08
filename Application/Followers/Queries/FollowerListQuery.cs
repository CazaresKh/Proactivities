using System.Collections.Generic;
using Application.Core;
using Application.Profiles;
using MediatR;

namespace Application.Followers.Queries
{
    public class FollowerListQuery : IRequest<Result<ICollection<Profile>>>
    {
        public string Predicate { get; }

        public string UserName { get; }

        public FollowerListQuery(string userName, string predicate)
        {
            Predicate = predicate;
            UserName = userName;
        }
    }
}