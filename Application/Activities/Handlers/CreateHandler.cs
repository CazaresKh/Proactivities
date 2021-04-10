using Application.Activities.Commands;
using Application.Core;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Handlers
{
    public class CreateHandler : BaseHandler, IRequestHandler<CreateActivityCommand, Result<Unit>>
    {
        public CreateHandler(DataContext context) : base(context) { }

        public async Task<Result<Unit>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            Context.Activities.Add(request.Activity);

            var result = await Context.SaveChangesAsync(cancellationToken) > 0;

            return !result ? Result<Unit>.Failure("Failed to create activity.") : Result<Unit>.Success(Unit.Value);
        }
    }
}
