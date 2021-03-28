using Domain;

namespace Application.Activities.Commands
{
    public class CreateActivityCommand : ActivityCommand
    {
        public CreateActivityCommand(Activity activity) : base(activity){ }
    }
}
