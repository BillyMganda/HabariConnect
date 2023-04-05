using HabariConnect.Application.CommandHandlers;
using HabariConnect.Application.Commands;
using HabariConnect.Application.Queries;
using HabariConnect.Application.QueryHandlers;
using HabariConnect.Domain.DTOs.User;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HabariConnect.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {            
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddScoped<IMediator, Mediator>();
            // User
            services.AddScoped<IRequestHandler<GetUserByIdQuery, UserGetDto>, GetUserByIdQueryHandler>();
            services.AddScoped<IRequestHandler<CreateUserCommand, UserGetDto>, CreateUserCommandHandler>();
            services.AddScoped<IRequestHandler<GetUserByEmailQuery, UserGetDto>, GetUserByEmailQueryHandler>();
            services.AddScoped<IRequestHandler<GetUserByHandleQuery, UserGetDto>, GetUserByHandleQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllUsersQuery, IEnumerable<UserGetDto>>, GetAllUsersQueryHandler>();
            services.AddScoped<IRequestHandler<DisableUserCommand, UserGetDto>, DisableUserCommandHandler>();
            services.AddScoped<IRequestHandler<EnableUserCommand, UserGetDto>, EnableUserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, UserGetDto>, UpdateUserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand, Guid>, DeleteUserCommandHandler>();
        }
    }
}
