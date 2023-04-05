using HabariConnect.Application.Exceptions;
using HabariConnect.Application.Queries;
using HabariConnect.Domain.DTOs.User;
using HabariConnect.Domain.Interfaces;
using MediatR;

namespace HabariConnect.Application.QueryHandlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserGetDto>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserGetDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAllUsersAsync();
            if (user == null)
            {
                throw new NotFoundException($"Users not found.");
            }

            return (List<UserGetDto>)await _userRepository.GetAllUsersAsync();
        }
    }
}
