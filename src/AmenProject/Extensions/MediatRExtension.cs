using DEMAT;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DEMAT.Api.Extensions
{
    /// <summary>
    /// MediatR extension
    /// </summary>
    public static class MediatRExtension
    {
        /// <summary>
        /// Configures the MediatR.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
        }
    }
}
