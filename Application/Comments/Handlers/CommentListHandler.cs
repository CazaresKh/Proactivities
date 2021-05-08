using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Comments.Queries;
using Application.Core;
using Application.Integration;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Comments.Handlers
{
    public class CommentListHandler : BaseHandler, IRequestHandler<CommentListQuery, Result<ICollection<CommentDto>>>
    {
        private readonly IMapper _mapper;

        public CommentListHandler(DataContext context, IMapper mapper, IUserAccessor userAccessor) : base(context,
            userAccessor)
        {
            _mapper = mapper;
        }

        public async Task<Result<ICollection<CommentDto>>> Handle(CommentListQuery request,
            CancellationToken cancellationToken)
        {
            var comments = await Context.Comments.Where(x => x.Activity.Id == request.ActivityId)
                .OrderByDescending(x => x.CreateAt).ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<ICollection<CommentDto>>.Success(comments);
        }
    }
}