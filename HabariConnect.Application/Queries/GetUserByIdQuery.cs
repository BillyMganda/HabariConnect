using HabariConnect.Domain.DTOs.User;
using MediatR;

namespace HabariConnect.Application.Queries
{
    public class GetUserByIdQuery : IRequest<UserGetDto>
    {
        public Guid Id { get; set; }
    }
}
