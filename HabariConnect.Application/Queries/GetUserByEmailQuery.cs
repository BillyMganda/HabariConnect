using HabariConnect.Domain.DTOs.User;
using MediatR;

namespace HabariConnect.Application.Queries
{
    public class GetUserByEmailQuery : IRequest<UserGetDto>
    {
        public string Email { get; set; } = string.Empty;
    }
}
