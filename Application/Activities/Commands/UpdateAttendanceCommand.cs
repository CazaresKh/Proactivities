using System;

namespace Application.Activities.Commands
{
    public class UpdateAttendanceCommand : SimpleCommand
    {
        public UpdateAttendanceCommand(Guid id) : base(id)
        {
        }
    }
}