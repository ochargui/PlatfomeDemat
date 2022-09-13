using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.Behaviors
{
    /// <summary>
    /// Logging behavior pipeline
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <seealso cref="MediatR.IPipelineBehavior{TRequest, TResponse}" />
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
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
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("Handling {TRequest}", typeof(TRequest).Name);
            var response = await next();
            _logger.LogInformation("Handled {TRequest} with {TResponse} response", typeof(TRequest).Name, typeof(TResponse).Name);

            return response;
        }
    }
}
