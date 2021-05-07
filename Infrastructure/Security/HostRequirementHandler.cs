using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Security
{
    public class HostRequirementHandler : AuthorizationHandler<HostRequirement>
    {
        private readonly DataContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HostRequirementHandler(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = context;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HostRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) { return Task.CompletedTask; }

            var activityId = Guid.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues.SingleOrDefault(x => x.Key == "id").Value?.ToString());

            var attendee = _dbContext.ActivityAttendees
                                    .AsNoTracking()
                                    .SingleOrDefaultAsync(x => x.AppUserId == userId && x.ActivityId == activityId).Result;

            if (attendee == null)
            {
                return Task.CompletedTask;
            }

            if (attendee.IsHost)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}