using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this  IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            // GetExecutingAssembly prolazi kroz nas kod (kroz assembly)
            // pokupice kod koji ga interesuje (klase koje implementiraju IRequest,IRequestHandler...)
            // i registrovace ih u dependency injection

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(UnhandledExceptionBehavior<,>));
            // ne buni se jer : ce tacno znati za Pipeline odgovarajuceg tipa
            // ti pozovi UnhandledExceptionBehavior za te tipove

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            

            return services;
        }
    }
}
