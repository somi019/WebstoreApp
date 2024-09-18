using FluentValidation;
using MediatR;
using Ordering.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviors
{
    public class ValidationBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any()) 
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults =  await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context,cancellationToken)));
                // Task.WhenAll vraca jedan Task, kao promise u js-u, ceka da se izvrse sve validacije
                // i onda ih stavlja u jedan Task<ValidationResult[]>

                var failures = validationResults.SelectMany(r => r.Errors).Where(f=> f != null);
                if (failures.Any())
                {
                    throw new ValidationFailedException(failures);
                }


            }
            
            return await next();
            // await next jer je ovo behavior koji radi proveru zahteva koji je stigao
            // pre -> obrada -> post
        }
    }
}

// ovo je jedan validator koji radi za sve moguce requestove koje smo pisali
// jedino potrebno je za svaki validator da se nasledi AbstractValidator<CommandClass>
// pogledaj u CreateOrderCommandValidator