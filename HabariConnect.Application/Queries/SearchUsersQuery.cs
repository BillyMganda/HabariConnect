using HabariConnect.Domain.DTOs.User;
using MediatR;

namespace HabariConnect.Application.Queries
{
    public class SearchUsersQuery : IRequest<IEnumerable<UserGetDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
