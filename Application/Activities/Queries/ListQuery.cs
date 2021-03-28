using Domain;
using MediatR;
using System.Collections.Generic;

namespace Application.Activities.Queries
{
    public class ListQuery : IRequest<ICollection<Activity>> 
    {
        public ICollection<Activity> Activities { get; set; }
    }
}
