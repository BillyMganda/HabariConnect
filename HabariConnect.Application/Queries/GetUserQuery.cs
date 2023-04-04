using HabariConnect.Domain.DTOs.User;
using MediatR;

namespace HabariConnect.Application.Queries
{
    public class GetUserQuery : IRequest<UserGetDto>
    {
        public Guid Id { get; set; }
    }
}
