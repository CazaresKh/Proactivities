using System;

namespace Application.Activities.Commands
{
    public class DeleteActivityCommand : SimpleCommand
    {
        public DeleteActivityCommand(Guid id) : base(id) { }
    }
}
