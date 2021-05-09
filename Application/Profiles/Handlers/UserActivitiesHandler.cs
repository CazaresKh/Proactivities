using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Integration;
using Application.Interfaces;
using Application.Profiles.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Handlers
{
    public class UserActivitiesHandler : BaseHandler,
        IRequestHandler<UserActivitiesQuery, Result<ICollection<UserActivityDto>>>
    {
        private readonly IMapper _mapper;

        public UserActivitiesHandler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
            : base(context, userAccessor)
        {
            _mapper = mapper;
        }

        public async Task<Result<ICollection<UserActivityDto>>> Handle(UserActivitiesQuery request,
            CancellationToken cancellationToken)
        {
            var query = Context.ActivityAttendees
                .Where(u => u.AppUser.UserName == request.UserName)
                .OrderBy(a => a.Activity.Date)
                .ProjectTo<UserActivityDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            query = request.Predicate switch
            {
                "past" => query.Where(a => a.Date <= DateTime.Now),
                "hosting" => query.Where(a => a.HostUserName == request.UserName),
                _ => query.Where(a => a.Date >= DateTime.Now)
            };

            var result = await query.ToListAsync(cancellationToken);

            return Result<ICollection<UserActivityDto>>.Success(result);
        }
    }
}