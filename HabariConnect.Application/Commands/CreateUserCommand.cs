using HabariConnect.Domain.DTOs.User;
using HabariConnect.Domain.Entities;
using MediatR;

namespace HabariConnect.Application.Commands
{
    public class CreateUserCommand : IRequest<UserGetDto>
    {
        public UserCreateDto User { get; set; }
    }
}
