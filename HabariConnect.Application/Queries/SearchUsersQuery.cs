using HabariConnect.Domain.DTOs.User;
using MediatR;

namespace HabariConnect.Application.Queries
{
    public class SearchUsersQuery : IRequest<UserGetDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
