using System.Threading;
using System.Threading.Tasks;
using Application.Comments.Commands;
using Application.Core;
using Application.Integration;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Comments.Handlers
{
    public class CreateCommentHandler : BaseHandler, IRequestHandler<CreateCommentCommand, Result<CommentDto>>
    {
        private readonly IMapper _mapper;

        public CreateCommentHandler(DataContext context, IUserAccessor userAccessor, IMapper mapper) : base(context,
            userAccessor)
        {
            _mapper = mapper;
        }

        public async Task<Result<CommentDto>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var activity = await Context.Activities.FindAsync(request.ActivityId);

            if (activity == null)
            {
                return null;
            }

            var user = await Context.Users.Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == UserAccessor.GetUserName(), cancellationToken);

            var comment = new Comment
            {
                Author = user,
                Activity = activity,
                Body = request.Body
            };

            activity.Comments.Add(comment);

            var success = await Context.SaveChangesAsync(cancellationToken) > 0;

            return success
                ? Result<CommentDto>.Success(_mapper.Map<CommentDto>(comment))
                : Result<CommentDto>.Failure("Failed to add comment");
        }
    }
}