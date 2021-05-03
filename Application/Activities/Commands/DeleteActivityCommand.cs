using System;
using Application.Integration;

namespace Application.Activities.Commands
{
    public class DeleteActivityCommand : SimpleCommand<Guid>
    {
        public DeleteActivityCommand(Guid id) : base(id) { }
    }
}
