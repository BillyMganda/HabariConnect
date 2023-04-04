using HabariConnect.Domain.DTOs.User;
using HabariConnect.Domain.Entities;
using MediatR;

namespace HabariConnect.Application.Commands
{
    public class CreateUserCommand : IRequest<UserGetDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Handle { get; set; }
        public string Password { get; set; }
        public bool TermsAgreed { get; set; }
    }
}
