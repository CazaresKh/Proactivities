using Application.Core;
using MediatR;
using System;

namespace Application.Activities.Queries
{
    public class ActivityQuery : IRequest<Result<ActivityDto>>
    {
        public Guid Id { get; set; }

        public ActivityQuery(Guid id)
        {
            Id = id;
        }
    }
}
