using Application.Activities.Commands;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Handlers
{
    public class DeleteHandler : BaseHandler, IRequestHandler<DeleteActivityCommand>
    {
        public DeleteHandler(DataContext context) : base(context) { }

        public async Task<Unit> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await Context.Activities.FindAsync(request.Id);

            Context.Remove(activity);

            await Context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
