using HabariConnect.Application.Commands;
using HabariConnect.Application.Exceptions;
using HabariConnect.Domain.DTOs.User;
using HabariConnect.Domain.Entities;
using HabariConnect.Domain.Interfaces;
using MediatR;

namespace HabariConnect.Application.CommandHandlers
{
    public class DisableUserCommandHandler : IRequestHandler<DisableUserCommand, UserGetDto>
    {
        private readonly IUserRepository _userRepository;
        public DisableUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserGetDto> Handle(DisableUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException($"User with Id {request.Id} does not exists.");
            }

            await _userRepository.DisableUserAsync(request.Id);            

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
