using MediatR;
using System;

namespace Application.Activities.Commands
{
    public class DeleteActivityCommand : SimpleCommand, IRequest
    {
        public DeleteActivityCommand(Guid id) : base(id) { }
    }
}
