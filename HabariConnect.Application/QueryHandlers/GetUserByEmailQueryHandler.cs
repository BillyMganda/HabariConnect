using HabariConnect.Application.Exceptions;
using HabariConnect.Application.Queries;
using HabariConnect.Domain.DTOs.User;
using HabariConnect.Domain.Interfaces;
using MediatR;

namespace HabariConnect.Application.QueryHandlers
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserGetDto>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserGetDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                throw new NotFoundException($"User with email {request.Email} not found.");
            }

            return new UserGetDto
            {                
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Handle = user.Handle,
                CreatedOn = user.CreatedOn,
                LastModified = user.LastModified,
                TermsAgreed = user.TermsAgreed,
                IsActive = user.IsActive
            };
        }
    }
}
