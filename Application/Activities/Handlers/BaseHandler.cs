using Persistence;

namespace Application.Activities.Handlers
{
    public abstract class BaseHandler
    {
        private readonly DataContext _context;

        protected DataContext Context => _context;

        public BaseHandler(DataContext context)
        {
            _context = context;
        }
    }
}
