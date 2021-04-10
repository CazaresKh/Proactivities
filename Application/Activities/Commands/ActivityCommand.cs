using Application.Core;
using Domain;
using MediatR;

namespace Application.Activities.Commands
{
    public abstract class ActivityCommand : IRequest<Result<Unit>>
    {
        public Activity Activity { get; set; }

        protected ActivityCommand(Activity activity)
        {
            Activity = activity;
        }
    }
}
