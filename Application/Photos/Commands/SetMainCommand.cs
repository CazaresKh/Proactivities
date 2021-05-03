using Application.Integration;

namespace Application.Photos.Commands
{
    public class SetMainCommand : SimpleCommand<string>
    {
        public SetMainCommand(string id) : base(id)
        {
        }
    }
}