using HabariConnect.Domain.DTOs.User;
using MediatR;

namespace HabariConnect.Application.Queries
{
    public class GetUserByHandleQuery : IRequest<UserGetDto>
    {
        public string Handle { get; set; } = string.Empty;
    }
}
