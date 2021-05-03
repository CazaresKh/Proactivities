using Application.Interfaces;
using Persistence;

namespace Application.Integration
{
    public class SupportHandler : BaseHandler
    {
        protected IPhotoAccessor PhotoAccessor { get; }
        protected IUserAccessor UserAccessor { get; }

        protected SupportHandler(DataContext context, IUserAccessor userAccessor, IPhotoAccessor photoAccessor) :
            base(context)
        {
            UserAccessor = userAccessor;
            PhotoAccessor = photoAccessor;
        }
    }
}