using HabariConnect.Domain.DTOs.User;
using MediatR;

namespace HabariConnect.Application.Commands
{
    public class CreateUserCommand : IRequest<Unit>
    {
        public UserCreateDto UserCreateDto { get; set; }
    }
}
