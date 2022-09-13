using DEMAT.Api.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace DEMAT.Filters
{
    /// <summary>
    /// SeparatedQueryStringValueProviderFactory
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ModelBinding.IValueProviderFactory" />
    public class SeparatedQueryStringValueProviderFactory : IValueProviderFactory
    {
        private readonly string _separator;
        private readonly string _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeparatedQueryStringValueProviderFactory"/> class.
        /// </summary>
        /// <param name="separator">The separator.</param>
        public SeparatedQueryStringValueProviderFactory(string separator) : this(null, separator)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeparatedQueryStringValueProviderFactory"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="separator">The separator.</param>
        public SeparatedQueryStringValueProviderFactory(string key, string separator)
        {
            _key = key;
            _separator = separator;
        }

        /// <summary>
        /// Creates a <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.IValueProvider" /> with values from the current request
        /// and adds it to <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ValueProviderFactoryContext.ValueProviders" /> list.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ValueProviderFactoryContext" />.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that when completed will add an <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.IValueProvider" /> instance
        /// to <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ValueProviderFactoryContext.ValueProviders" /> list if applicable.
        /// </returns>
        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            context.ValueProviders.Insert(0, new SeparatedQueryStringValueProvider(_key, context.ActionContext.HttpContext.Request.Query, _separator));
            return Task.CompletedTask;
        }
    }
}
