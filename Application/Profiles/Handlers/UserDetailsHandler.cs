using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Integration;
using Application.Profiles.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Handlers
{
    public class UserDetailsHandler : BaseHandler, IRequestHandler<UserDetailsQuery, Result<Profile>>
    {
        private readonly IMapper _mapper;
        public UserDetailsHandler(DataContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Result<Profile>> Handle(UserDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await Context.Users
            .ProjectTo<Profile>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);

            return user == null ? null : Result<Profile>.Success(user);
        }
    }
}