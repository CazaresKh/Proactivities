using AutoMapper;
using Persistence;

namespace Application.Integration
{
    public abstract class BaseHandler
    {
        protected DataContext Context { get; }

        protected BaseHandler(DataContext context)
        {
            Context = context;
        }
    }
}
