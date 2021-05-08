using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities.Commands;
using Application.Core;
using Application.Integration;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Handlers
{
    public class UpdateAttendanceHandler : BaseHandler, IRequestHandler<UpdateAttendanceCommand, Result<Unit>>
    {
        public UpdateAttendanceHandler(DataContext context, IUserAccessor userAccessor) : base(context, userAccessor)
        {
        }

        public async Task<Result<Unit>> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
        {
            var activity = await Context.Activities
                .Include(a => a.Attendees)
                .ThenInclude(u => u.AppUser)
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (activity == null)
            {
                return null;
            }

            var user = await Context.Users
                .FirstOrDefaultAsync(x => x.UserName == UserAccessor.GetUserName(), cancellationToken);

            if (user == null)
            {
                return null;
            }

            var hostUserName = activity.Attendees.FirstOrDefault(x => x.IsHost)?.AppUser?.UserName;

            var attendance = activity.Attendees.FirstOrDefault(x => x.AppUser.UserName == user.UserName);

            if (attendance != null && hostUserName == user.UserName)
            {
                activity.IsCanceled = !activity.IsCanceled;
            }

            if (attendance != null && hostUserName != user.UserName)
            {
                activity.Attendees.Remove(attendance);
            }

            if (attendance == null)
            {
                attendance = new ActivityAttendee
                {
                    AppUser = user,
                    Activity = activity,
                    IsHost = false
                };

                activity.Attendees.Add(attendance);
            }

            var result = await Context.SaveChangesAsync(cancellationToken) > 0;

            return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem updating attendance");
        }
    }
}