using System.Threading.Tasks;
using Application.Profiles.Commands;
using Application.Profiles.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        [HttpGet("{userName}")]
        public async Task<IActionResult> GetProfile(string userName)
        {
            return HandleResult(await Mediator.Send(new UserDetailsQuery(userName)));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProfileCommand command)
        {
            return HandleResult(await Mediator.Send(command));
        }
    }
}