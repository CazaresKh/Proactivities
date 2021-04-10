using Domain;
using MediatR;
using System.Collections.Generic;
using Application.Core;

namespace Application.Activities.Queries
{
    public class ListQuery : IRequest<Result<ICollection<Activity>>> 
    {
    }
}
