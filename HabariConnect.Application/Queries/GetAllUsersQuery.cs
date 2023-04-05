using HabariConnect.Domain.DTOs.User;
using MediatR;

namespace HabariConnect.Application.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserGetDto>>
    {
    }
}
