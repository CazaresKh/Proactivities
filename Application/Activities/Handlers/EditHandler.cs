using Application.Activities.Commands;
using AutoMapper;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Handlers
{
    public class EditHandler : BaseHandler, IRequestHandler<EditActivityCommand>
    {
        private readonly IMapper _mapper;

        public EditHandler(DataContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await Context.Activities.FindAsync(request.Activity.Id);

            _mapper.Map(request.Activity, activity);

            await Context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
