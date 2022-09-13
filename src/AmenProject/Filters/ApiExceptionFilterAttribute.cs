using DEMAT.ApplicationServices.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;


namespace DEMAT.Api.Filters
{
    /// <summary>
    /// Api Exception Filter
    /// </summary>
    /// <seealso cref="ExceptionFilterAttribute" />
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiExceptionFilterAttribute"/> class.
        /// </summary>
        public ApiExceptionFilterAttribute()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                //{ typeof(NotFoundException), HandleNotFoundException },
            };
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc />
        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="context">The context.</param>
        private void HandleException(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService<ILogger<ApiExceptionFilterAttribute>>();
            logger.LogError(context.Exception, "{ExceptionMessage}", context.Exception.GetBaseException().Message);

            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        /// <summary>
        /// Handles the unknown exception.
        /// </summary>
        /// <param name="context">The context.</param>
        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = $"An error occurred while processing your request : {context.Exception.GetBaseException()}",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Handles the validation exception.
        /// </summary>
        /// <param name="context">The context.</param>
        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;

            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        //private void HandleNotFoundException(ExceptionContext context)
        //{
        //    var exception = context.Exception as NotFoundException;

        //    var details = new ProblemDetails()
        //    {
        //        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
        //        Title = "The specified resource was not found.",
        //        Detail = exception.Message
        //    };

        //    context.Result = new NotFoundObjectResult(details);

        //    context.ExceptionHandled = true;
        //}
    }
}
