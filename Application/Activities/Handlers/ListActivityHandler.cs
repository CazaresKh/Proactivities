using Application.Activities.Queries;
using Application.Core;
using Application.Integration;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Application.Activities.Handlers
{
    public class ListActivityHandler : BaseHandler, IRequestHandler<ListQuery, Result<PagedList<ActivityDto>>>
    {
        private readonly IMapper _mapper;

        public ListActivityHandler(DataContext context, IMapper mapper, IUserAccessor userAccessor) : base(context,
            userAccessor)
        {
            _mapper = mapper;
        }

        public async Task<Result<PagedList<ActivityDto>>> Handle(ListQuery request,
            CancellationToken cancellationToken)
        {
            var query = Context.Activities
                .Where(d => d.Date >= request.Params.StartDate)
                .OrderBy(d => d.Date)
                .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider,
                    new {currentUserName = UserAccessor.GetUserName()})
                .AsQueryable();

            if (request.Params.IsGoing && !request.Params.IsHost)
            {
                query = query.Where(x => x.Attendees.Any(a => a.UserName == UserAccessor.GetUserName()));
            }

            if (request.Params.IsHost && !request.Params.IsGoing)
            {
                query = query.Where(x => x.HostUserName == UserAccessor.GetUserName());
            }

            var result = Result<PagedList<ActivityDto>>.Success(
                await PagedList<ActivityDto>.CreateAsync(query, request.Params.PageNumber,
                    request.Params.PageSize)
            );

            return result;
        }
    }
}