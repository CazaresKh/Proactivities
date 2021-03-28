using MediatR;
using System;

namespace Application.Activities.Commands
{
    public class SimpleCommand : IRequest
    {
        public Guid Id { get; set; }

        public SimpleCommand(Guid id)
        {
            Id = id;
        }
    }
}
