using HabariConnect.Application.Commands;
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
            services.AddScoped<IRequestHandler<GetAllCustomerQuery, List<Customer>>, GetAllCustomerHandler>();
            services.AddScoped<IRequestHandler<GetCustomerByEmailQuery, Customer>, GetCustomerByEmailHandler>();
            services.AddScoped<IRequestHandler<GetCustomerByIdQuery, Customer>, GetCustomerByIdHandler>();
            services.AddScoped<IRequestHandler<CreateCustomerCommand, CustomerResponse>, CreateCustomerHandler>();
            services.AddScoped<IRequestHandler<DeleteCustomerCommand, string>, DeleteCustomerHandler>();
            services.AddScoped<IRequestHandler<EditCustomerCommand, CustomerResponse>, EditCustomerHandler>();
            services.AddMediatR(typeof(GetCustomerByEmailHandler).GetTypeInfo().Assembly);
        }
    }
}
