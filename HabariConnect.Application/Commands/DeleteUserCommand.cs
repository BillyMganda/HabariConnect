using MediatR;

namespace HabariConnect.Application.Commands
{
    public class DeleteUserCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
