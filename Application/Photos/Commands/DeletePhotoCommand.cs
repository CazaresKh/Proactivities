using Application.Integration;

namespace Application.Photos.Commands
{
    public class DeletePhotoCommand : SimpleCommand<string>
    {
        public DeletePhotoCommand(string id) : base(id)
        {
        }
    }
}