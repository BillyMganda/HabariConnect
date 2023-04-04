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
        }
    }
}
