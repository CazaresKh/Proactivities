using System;
using System.Collections.Generic;
using Application.Core;
using MediatR;

namespace Application.Comments.Queries
{
    public class CommentListQuery : IRequest<Result<ICollection<CommentDto>>> 
    {
        public Guid ActivityId { get; set; }

        public CommentListQuery(Guid activityId)
        {
            ActivityId = activityId;
        }
    }
}