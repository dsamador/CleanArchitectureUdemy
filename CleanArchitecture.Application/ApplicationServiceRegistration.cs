using CleanArchitecture.Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // comenzamos el registro, tenemos que poner el assembly de la libreria que permite hacer el mapping de
            // las clases de origen a las clases destino, para que automaticamente vaya a todas las clases que
            // usen implementen las interfaces de AutoMapper y las inyecte
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Tambien vamos a agregar todas las clases del proyecto que hagan uso de AbstracValidation y
            // FluentValidation y autmaticamente va a inyectar esos objetos para que esa validacion sea posible
            // dentro del proyecto.
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Lo mismo con MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // Ahora agregamos un AddTransient para lo que son los BehaviorPipelines de validation y de las 
            // excepciones
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>) );
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            //queremos que nos devuelva el objeto services
            return services;
        }
    }
}
