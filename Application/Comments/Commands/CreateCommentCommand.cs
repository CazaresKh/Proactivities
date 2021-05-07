using System;
using Application.Core;
using MediatR;

namespace Application.Comments.Commands
{
    public class CreateCommentCommand : IRequest<Result<CommentDto>>
    {
        public string Body { get; set; }

        public Guid ActivityId { get; set; }
        
    }
}