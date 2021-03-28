using Application.Activities.Queries;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Handlers
{
    public class ListActivityHandler : BaseHandler, IRequestHandler<ListQuery, ICollection<Activity>>
    {
        public ListActivityHandler(DataContext context) : base(context) { }

        public async Task<ICollection<Activity>> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            return await Context.Activities.ToListAsync(cancellationToken);
        }
    }
}
