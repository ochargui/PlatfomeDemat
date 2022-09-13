using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace DEMAT.ApplicationServices.Exceptions
{
    /// <summary>
    /// Validation exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class ValidationException : Exception
    {
        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
       // public IDictionary<string, string[]> Errors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException" /> class.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="args">The args.</param>
        public ValidationException(string format, Exception innerException, params object[] args)
            : base(string.Format(CultureInfo.InvariantCulture, format, args), innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException" /> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="failures">The failures.</param>
        /// 
        public ValidationException(IEnumerable<ValidationFailure> failures)
                : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public ValidationException(IEnumerable<IdentityError> errors) : this()
        {
            Errors = errors
                .GroupBy(e => e.Code, e => e.Description)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }


}
