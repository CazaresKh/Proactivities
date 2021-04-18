using Application.Activities.Queries;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Handlers
{
    public class ListActivityHandler : BaseHandler, IRequestHandler<ListQuery, Result<ICollection<ActivityDto>>>
    {
        private readonly IMapper _mapper;

        public ListActivityHandler(DataContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<ICollection<ActivityDto>>> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            var result = await Context.Activities
                                          .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return Result<ICollection<ActivityDto>>.Success(result);
        }
    }
}
