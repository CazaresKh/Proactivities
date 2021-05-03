using System;
using Application.Integration;

namespace Application.Activities.Commands
{
    public class UpdateAttendanceCommand : SimpleCommand<Guid>
    {
        public UpdateAttendanceCommand(Guid id) : base(id)
        {
        }
    }
}