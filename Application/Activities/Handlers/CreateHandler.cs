using Application.Activities.Commands;
using Application.Core;
using Application.Integration;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities.Handlers
{
    public class CreateHandler : BaseHandler, IRequestHandler<CreateActivityCommand, Result<Unit>>
    {
        private readonly IUserAccessor _userAccessor;
        public CreateHandler(DataContext context, IUserAccessor userAccessor) : base(context)
        {
            _userAccessor = userAccessor;
        }

        public async Task<Result<Unit>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            var user = await Context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());

            var attendee = new ActivityAttendee
            {
                AppUser = user,
                Activity = request.Activity,
                IsHost = true
            };

            request.Activity.Attendees.Add(attendee);

            Context.Activities.Add(request.Activity);

            var result = await Context.SaveChangesAsync(cancellationToken) > 0;

            return !result ? Result<Unit>.Failure("Failed to create activity.") : Result<Unit>.Success(Unit.Value);
        }
    }
}
