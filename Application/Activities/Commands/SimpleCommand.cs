using MediatR;
using System;
using Application.Core;

namespace Application.Activities.Commands
{
    public class SimpleCommand : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }

        public SimpleCommand(Guid id)
        {
            Id = id;
        }
    }
}
