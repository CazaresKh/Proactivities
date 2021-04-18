using Application.Activities.Queries;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Handlers
{
    public class DetailsHandler : BaseHandler, IRequestHandler<ActivityQuery, Result<ActivityDto>>
    {
        private readonly IMapper _mapper;

        public DetailsHandler(DataContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<ActivityDto>> Handle(ActivityQuery request, CancellationToken cancellationToken)
        {
            var activity = await Context.Activities
                                        .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider)
                                        .FirstOrDefaultAsync(x => x.Id == request.Id);

            return Result<ActivityDto>.Success(activity);
        }
    }
}
