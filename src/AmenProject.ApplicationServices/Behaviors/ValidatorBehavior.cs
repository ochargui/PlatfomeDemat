using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.Behaviors
{
    /// <summary>
    /// Validation behavior pipeline
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <seealso cref="MediatR.IPipelineBehavior{TRequest, TResponse}" />
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;
        private readonly IValidator<TRequest>[] _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="validators">The validators.</param>
        /// <param name="logger">The logger.</param>
        public ValidatorBehavior(IValidator<TRequest>[] validators, ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        /// <summary>
        /// Pipeline handler. Perform any additional behavior and await the <paramref name="next" /> delegate as necessary
        /// </summary>
        /// <param name="request">Incoming request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="next">Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the handler.</param>
        /// <returns>
        /// Awaitable task returning the <typeparamref name="TResponse" />
        /// </returns>
        /// <exception cref="Exceptions.ValidationException"></exception>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var typeName = request.GetType().Name;
                _logger.LogInformation("Validating request {RequestType}", typeName);

                var context = new ValidationContext(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Any())
                {
                    _logger.LogWarning("Validation errors - {RequestType} - Request: {@Request} - Errors: {@ValidationErrors}", typeName, request, failures);
                    throw new Exceptions.ValidationException(failures);
                }
            }
            return await next();
        }
    }
}
