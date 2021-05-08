using Application.Activities.Commands;
using Application.Core;
using Application.Integration;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Application.Activities.Handlers
{
    public class DeleteHandler<Guid> : BaseHandler, IRequestHandler<DeleteActivityCommand, Result<Unit>>
    {
        public DeleteHandler(DataContext context, IUserAccessor userAccessor) : base(context, userAccessor)
        {
        }

        public async Task<Result<Unit>> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await Context.Activities.FindAsync(request.Id);

            Context.Remove(activity);

            var result = await Context.SaveChangesAsync(cancellationToken) > 0;

            return !result ? Result<Unit>.Failure("Failed to delete the activity") : Result<Unit>.Success(Unit.Value);
        }
    }
}