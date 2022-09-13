using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System;
using System.Globalization;
using System.Linq;

namespace DEMAT.Api.Filters
{
    /// <summary>
    /// SeparatedQueryStringValueProvider
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ModelBinding.QueryStringValueProvider" />
    public class SeparatedQueryStringValueProvider : QueryStringValueProvider
    {
        private readonly string _key;
        private readonly string _separator;
        private readonly IQueryCollection _values;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeparatedQueryStringValueProvider"/> class.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="separator">The separator.</param>
        public SeparatedQueryStringValueProvider(IQueryCollection values, string separator)
            : this(null, values, separator)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeparatedQueryStringValueProvider"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="values">The values.</param>
        /// <param name="separator">The separator.</param>
        public SeparatedQueryStringValueProvider(string key, IQueryCollection values, string separator)
            : base(BindingSource.Query, values, CultureInfo.InvariantCulture)
        {
            _key = key;
            _values = values;
            _separator = separator;
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <inheritdoc />
        public override ValueProviderResult GetValue(string key)
        {
            var result = base.GetValue(key);

            if (_key != null && _key != key)
            {
                return result;
            }

            if (result != ValueProviderResult.None && result.Values.Any(x => x.IndexOf(_separator, StringComparison.OrdinalIgnoreCase) > 0))
            {
                var splitValues = new StringValues(result.Values
                    .SelectMany(x => x.Split(new[] { _separator }, StringSplitOptions.None)).ToArray());
                return new ValueProviderResult(splitValues, result.Culture);
            }

            return result;
        }
    }
}
