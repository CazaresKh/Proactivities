using MediatR;
using Application.Core;

namespace Application.Activities.Queries
{
    public class ListQuery : IRequest<Result<PagedList<ActivityDto>>>
    {
        public ListQuery(ActivityParams pagingParams)
        {
            Params = pagingParams;
        }

        public ActivityParams Params { get; }
    }
}