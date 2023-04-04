using HabariConnect.Application.Commands;
using HabariConnect.Domain.Entities;
using HabariConnect.Domain.Interfaces;
using MediatR;

namespace HabariConnect.Application.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Check if the user with the same email address already exists.
            if (await _userRepository.GetByEmailAsync(request.Email) != null)
            {
                throw new BadRequestException("User with the same email address already exists.");
            }

            // Check if the user with the same handle already exists.
            if (await _userRepository.GetByHandleAsync(request.Handle) != null)
            {
                throw new BadRequestException("User with the same handle already exists.");
            }

            // Hash the password and create a new user.
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Handle = request.Handle,
                TermsAgreed = request.TermsAgreed,
                CreatedOn = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,

            };
            user.SetPassword(request.Password);

            // Add the user to the repository.
            await _userRepository.CreateAsync(user);

            return user;
        }
    }
}
