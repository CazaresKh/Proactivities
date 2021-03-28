using Domain;

namespace Application.Activities.Commands
{
    public class EditActivityCommand : ActivityCommand
    {
        public EditActivityCommand(Activity activity) : base(activity)
        {
        }
    }
}
