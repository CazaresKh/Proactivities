using Application.Interfaces;
using AutoMapper;
using Persistence;

namespace Application.Integration
{
    public abstract class BaseHandler
    {
        protected DataContext Context { get; }

        protected IUserAccessor UserAccessor { get; }

        protected BaseHandler(DataContext context, IUserAccessor userAccessor)
        {
            Context = context;
            UserAccessor = userAccessor;
        }
    }
}
