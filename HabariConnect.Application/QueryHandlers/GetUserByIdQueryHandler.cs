using HabariConnect.Application.Exceptions;
using HabariConnect.Application.Queries;
using HabariConnect.Domain.DTOs.User;
using HabariConnect.Domain.Interfaces;
using MediatR;

namespace HabariConnect.Application.QueryHandlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserGetDto>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserGetDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);

            if (user == null)
            {
                throw new NotFoundException($"User with ID {request.Id} not found.");
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
