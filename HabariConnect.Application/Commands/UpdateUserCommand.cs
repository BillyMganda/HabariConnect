using HabariConnect.Domain.DTOs.User;
using MediatR;

namespace HabariConnect.Application.Commands
{
    public class UpdateUserCommand : IRequest<UserGetDto>
    {
        public UserModifyDto UserModifyDto { get; set; }
    }
}
