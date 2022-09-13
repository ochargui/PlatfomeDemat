using Autofac;
using FluentValidation;
using DEMAT.ApplicationServices.Behaviors;
using MediatR;
using System.Reflection;

namespace DEMAT.ApplicationService.Module
{
    /// <summary>
    /// Autofac module
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public class AutofacModule : Autofac.Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            Assembly applicationServicesAssembly = typeof(LoggingBehavior<,>).Assembly;
            RegisterHandlers(builder, applicationServicesAssembly);
            RegisterValidation(builder, applicationServicesAssembly);
            RegisterBehaviors(builder);
        }

        /// <summary>
        /// Registers the validation.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="assembly">The assembly.</param>
        private static void RegisterValidation(ContainerBuilder builder, Assembly assembly)
        {
            // Register the Command's Validators (Validators based on FluentValidation library)
            builder
                .RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .AsImplementedInterfaces();
        }

        /// <summary>
        /// Registers the handlers.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="assembly">The assembly.</param>
        private static void RegisterHandlers(ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                //.Where(t => t.Name.EndsWith("Handler"))
                .AsImplementedInterfaces();
        }

        /// <summary>
        /// Registers the behaviors.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private void RegisterBehaviors(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
