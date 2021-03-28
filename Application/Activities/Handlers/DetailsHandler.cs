using Application.Activities.Queries;
using Domain;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Handlers
{
    public class DetailsHandler : BaseHandler, IRequestHandler<ActivityQuery, Activity>
    {
        public DetailsHandler(DataContext context) : base(context) { }

        public async Task<Activity> Handle(ActivityQuery request, CancellationToken cancellationToken)
        {
            return await Context.Activities.FindAsync(request.Id);
        }
    }
}
