using HabariConnect.Application.Commands;
using HabariConnect.Application.Exceptions;
using HabariConnect.Domain.DTOs.User;
using HabariConnect.Domain.Entities;
using HabariConnect.Domain.Interfaces;
using MediatR;

namespace HabariConnect.Application.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserGetDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserGetDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //var user_ = await _userRepository.GetUserByEmailAsync(request.User.Email);
            //if (user_ != null)
            //{
            //    throw new NotFoundException($"User with email {request.User.Email} exists.");
            //}

            var user = new User
            {
                FirstName = request.User.FirstName,
                LastName = request.User.LastName,
                Email = request.User.Email,
                Handle = "@" + request.User.Handle,
                CreatedOn = DateTime.Now,
                LastModified = DateTime.Now,
                ForgotPasswordToken = "",
                TermsAgreed = request.User.TermsAgreed,
                IsActive = true
            };

            var (passwordHash, passwordSalt) = _passwordHasher.HashPassword(request.User.Password);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _userRepository.AddUserAsync(user);

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
