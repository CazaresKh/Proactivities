using MediatR;
using Application.Core;

namespace Application.Integration
{
    public class SimpleCommand<T> : IRequest<Result<Unit>>
    {
        public T Id { get; }

        protected SimpleCommand(T id)
        {
            Id = id;
        }
    }
}