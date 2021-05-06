using Application.Core;
using MediatR;

namespace Application.Profiles.Commands
{
    public class UpdateProfileCommand : IRequest<Result<Unit>>
    {
        public string DisplayName { get; set; }

        public string Bio { get; set; }

        public UpdateProfileCommand(string displayName)
        {
            DisplayName = displayName;
        }
    }
}