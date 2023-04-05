using HabariConnect.Domain.DTOs.User;
using MediatR;

namespace HabariConnect.Application.Commands
{
    public class EnableUserCommand : IRequest<UserGetDto>
    {
        public Guid Id { get; set; }
    }
}
