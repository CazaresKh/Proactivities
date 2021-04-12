using Application.Activities.Commands;
using Application.Activities.Queries;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ICollection<Activity>>> GetActivities(CancellationToken ct)
        {
            return HandleResult(await Mediator.Send(new ListQuery(), ct));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            return HandleResult(await Mediator.Send(new ActivityQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            return HandleResult(await Mediator.Send(new CreateActivityCommand(activity)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditActivity(Guid id, Activity activity)
        {
            activity.Id = id;
            return HandleResult(await Mediator.Send(new EditActivityCommand(activity)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Activity>> DeleteActivity(Guid id)
        {
            return HandleResult(await Mediator.Send(new DeleteActivityCommand(id)));
        }
    }
}