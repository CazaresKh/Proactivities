using Application.Activities.Commands;
using Application.Core;
using AutoMapper;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Handlers
{
    public class EditHandler : BaseHandler, IRequestHandler<EditActivityCommand, Result<Unit>>
    {
        private readonly IMapper _mapper;

        public EditHandler(DataContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(EditActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await Context.Activities.FindAsync(request.Activity.Id);

            if (activity == null)
            {
                return null;
            }
            _mapper.Map(request.Activity, activity);

            var result = await Context.SaveChangesAsync(cancellationToken) > 0;

            return !result ? Result<Unit>.Failure("Failed to update the activity") : Result<Unit>.Success(Unit.Value);
        }
    }
}
