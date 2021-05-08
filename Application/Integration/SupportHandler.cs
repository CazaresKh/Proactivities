using Application.Interfaces;
using Persistence;

namespace Application.Integration
{
    public class SupportHandler : BaseHandler
    {
        protected IPhotoAccessor PhotoAccessor { get; }

        protected SupportHandler(DataContext context, IUserAccessor userAccessor, IPhotoAccessor photoAccessor) :
            base(context, userAccessor)
        {
            PhotoAccessor = photoAccessor;
        }
    }
}