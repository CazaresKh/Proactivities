using Application.Activities.Queries;
using Application.Core;
using Domain;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Handlers
{
    public class DetailsHandler : BaseHandler, IRequestHandler<ActivityQuery, Result<Activity>>
    {
        public DetailsHandler(DataContext context) : base(context) { }

        public async Task<Result<Activity>> Handle(ActivityQuery request, CancellationToken cancellationToken)
        {
            var activity =  await Context.Activities.FindAsync(request.Id);

            return Result<Activity>.Success(activity);
        }
    }
}
