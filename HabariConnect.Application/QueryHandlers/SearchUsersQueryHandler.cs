using HabariConnect.Application.Queries;
using HabariConnect.Domain.DTOs.User;
using HabariConnect.Domain.Interfaces;
using MediatR;

namespace HabariConnect.Application.QueryHandlers
{
    public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, IEnumerable<UserGetDto>>
    {
        private readonly IUserRepository _userRepository;
        public SearchUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<IEnumerable<UserGetDto>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.SearchUsersAsync(request.FirstName, request.LastName);

            var userGetDtos = users.Select(user => new UserGetDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Handle = user.Handle,
                CreatedOn = user.CreatedOn,
                LastModified = user.LastModified,
                TermsAgreed = user.TermsAgreed,
                IsActive = user.IsActive
            });

            return userGetDtos;
        }
    }
}
