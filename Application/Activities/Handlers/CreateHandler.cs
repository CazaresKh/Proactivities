using Application.Activities.Commands;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Handlers
{
    public class CreateHandler : BaseHandler, IRequestHandler<CreateActivityCommand>
    {
        public CreateHandler(DataContext context) : base(context) { }

        public async Task<Unit> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            Context.Activities.Add(request.Activity);

            await Context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
