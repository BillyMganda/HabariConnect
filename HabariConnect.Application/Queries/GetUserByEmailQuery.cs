using HabariConnect.Domain.DTOs.User;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HabariConnect.Application.Queries
{
    public class GetUserByEmailQuery : IRequest<UserGetDto>
    {        
        public string Email { get; set; } = string.Empty;
    }
}
