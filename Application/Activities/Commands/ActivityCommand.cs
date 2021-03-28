using Domain;
using MediatR;

namespace Application.Activities.Commands
{
    public abstract class ActivityCommand : IRequest
    {
        public Activity Activity { get; set; }

        public ActivityCommand(Activity activity)
        {
            Activity = activity;
        }
    }
}
