using Domain;
using MediatR;
using System;

namespace Application.Activities.Queries
{
    public class ActivityQuery : IRequest<Activity>
    {
        public Guid Id { get; set; }

        public ActivityQuery(Guid id)
        {
            Id = id;
        }
    }
}
