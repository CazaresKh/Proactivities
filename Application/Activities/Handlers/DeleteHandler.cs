using Application.Activities.Commands;
using Application.Core;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Handlers
{
    public class DeleteHandler : BaseHandler, IRequestHandler<DeleteActivityCommand, Result<Unit>>
    {
        public DeleteHandler(DataContext context) : base(context) { }

        public async Task<Result<Unit>> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await Context.Activities.FindAsync(request.Id);

            //if (activity == null) return null;

            Context.Remove(activity);

            var result = await Context.SaveChangesAsync(cancellationToken) > 0;

            return !result ? Result<Unit>.Failure("Failed to delete the activity") : Result<Unit>.Success(Unit.Value);
        }
    }
}
