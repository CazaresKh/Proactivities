using System.Collections.Generic;
using Application.Core;
using MediatR;

namespace Application.Profiles.Queries
{
    public class UserActivitiesQuery : IRequest<Result<ICollection<UserActivityDto>>>
    {
        public string UserName { get; }

        public string Predicate { get; }

        public UserActivitiesQuery(string userName, string predicate)
        {
            UserName = userName;
            Predicate = predicate;
        }
    }
}