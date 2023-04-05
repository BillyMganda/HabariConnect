using HabariConnect.Application.Commands;
using HabariConnect.Application.Exceptions;
using HabariConnect.Domain.DTOs.User;
using HabariConnect.Domain.Interfaces;
using MediatR;

namespace HabariConnect.Application.CommandHandlers
{
    public class EnableUserCommandHandler : IRequestHandler<EnableUserCommand, UserGetDto>
    {
        private readonly IUserRepository _userRepository;
        public EnableUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserGetDto> Handle(EnableUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException($"User with Id {request.Id} does not exists.");
            }

            await _userRepository.EnableUserAsync(request.Id);

            var userGetDto = new UserGetDto
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

            return userGetDto;
        }
    }
}
