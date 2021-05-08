using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Followers.Queries;
using Application.Integration;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Profile = Application.Profiles.Profile;

namespace Application.Followers.Handlers
{
    public class FollowerListHandler : BaseHandler, IRequestHandler<FollowerListQuery, Result<ICollection<Profile>>>
    {
        private readonly IMapper _mapper;

        public FollowerListHandler(DataContext context, IMapper mapper, IUserAccessor userAccessor) : base(context,
            userAccessor)
        {
            _mapper = mapper;
        }

        public async Task<Result<ICollection<Profile>>> Handle(FollowerListQuery request,
            CancellationToken cancellationToken)
        {
            var profiles = new List<Profile>();

            switch (request.Predicate)
            {
                case "followers":
                    profiles = await Context.UserFollowings
                        .Where(x => x.Target.UserName == request.UserName)
                        .Select(u => u.Observer)
                        .ProjectTo<Profile>(_mapper.ConfigurationProvider,
                            new {currentUserName = UserAccessor.GetUserName()})
                        .ToListAsync(cancellationToken);
                    break;
                case "following":
                    profiles = await Context.UserFollowings
                        .Where(x => x.Observer.UserName == request.UserName)
                        .Select(u => u.Target)
                        .ProjectTo<Profile>(_mapper.ConfigurationProvider,
                            new {currentUserName = UserAccessor.GetUserName()})
                        .ToListAsync(cancellationToken);
                    break;
            }

            return Result<ICollection<Profile>>.Success(profiles);
        }
    }
}