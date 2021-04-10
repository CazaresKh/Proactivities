using Application.Activities.Queries;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;

namespace Application.Activities.Handlers
{
    public class ListActivityHandler : BaseHandler, IRequestHandler<ListQuery, Result<ICollection<Activity>>>
    {
        public ListActivityHandler(DataContext context) : base(context) { }

        public async Task<Result<ICollection<Activity>>> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            var result = await Context.Activities.ToListAsync(cancellationToken);
            return Result<ICollection<Activity>>.Success(result);
        }
    }
}
