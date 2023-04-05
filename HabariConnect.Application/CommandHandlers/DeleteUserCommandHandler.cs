using HabariConnect.Application.Commands;
using HabariConnect.Application.Exceptions;
using HabariConnect.Domain.Interfaces;
using MediatR;

namespace HabariConnect.Application.CommandHandlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            if(user == null)
            {
                throw new NotFoundException($"user with id {request.Id} not found");
            }

            await _userRepository.DeleteUserAsync(user);

            return request.Id;
        }
    }
}
