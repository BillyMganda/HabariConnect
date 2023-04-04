using HabariConnect.Application.Exceptions;
using HabariConnect.Application.Queries;
using HabariConnect.Domain.DTOs.User;
using HabariConnect.Domain.Interfaces;
using MediatR;

namespace HabariConnect.Application.QueryHandlers
{
    public class GetUserByHandleQueryHandler : IRequestHandler<GetUserByHandleQuery, UserGetDto>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByHandleQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserGetDto> Handle(GetUserByHandleQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByHandleAsync(request.Handle);

            if (user == null)
            {
                throw new NotFoundException($"User with handle {request.Handle} not found.");
            }

            return new UserGetDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Handle = user.Handle,
                CreatedOn = user.CreatedOn,
                LastModified = user.LastModified,
                TermsAgreed = user.TermsAgreed,
                IsActive = user.IsActive
            };
        }
    }
}
