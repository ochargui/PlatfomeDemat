using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DEMAT.Api.Extensions
{
    /// <summary>
    /// Swagger configuration
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// Configure the Swagger generator, defining one or more Swagger documents
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DEMAT.Api",
                    Version = "v1",
                    Description = "DEMAT.Api"
                });

                // To manage enums
                c.SchemaFilter<XEnumNamesSchemaFilter>();

                // Autorest issue 
                c.OperationFilter<ArrayInQueryParametersFilter>();
                c.OperationFilter<SwaggerOperationFilter>(); // AutoRest uses operationId to determine the class name/method name for a given API

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Enable middleware to serve generated Swagger as a JSON endpoint.
        /// Enable middleware to serve swagger-ui.
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AmenProject Api - v1");
            });
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ISchemaFilter" />
    public class XEnumNamesSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Applies the specified schema.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var typeInfo = context.Type.GetTypeInfo();
            if (typeInfo.IsEnum)
            {
                var names = Enum.GetNames(context.Type);
                schema.Extensions.Add("x-enumNames", new PropertiesExtension { PropertyNames = names });
            }
        }
    }

    /// <summary>
    /// Write properties names
    /// </summary>
    /// <seealso cref="IOpenApiExtension" />
    public class PropertiesExtension : IOpenApiExtension
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesExtension"/> class.
        /// </summary>
        public PropertiesExtension()
        {

        }

        /// <summary>
        /// Gets or sets the property names.
        /// </summary>
        /// <value>
        /// The property names.
        /// </value>
        public IEnumerable<string> PropertyNames { get; set; }

        /// <summary>
        /// Write out contents of custom extension
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="specVersion">Version of the OpenAPI specification that that will be output.</param>
        public void Write(IOpenApiWriter writer, OpenApiSpecVersion specVersion)
        {
            writer.WriteStartArray();

            foreach (var propName in PropertyNames)
            {
                writer.WriteValue(propName);
            }

            writer.WriteEndArray();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IOperationFilter" />
    public class ArrayInQueryParametersFilter : IOperationFilter
    {
        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (var parameter in operation.Parameters.Where(x => x.In == ParameterLocation.Query && x.Schema.Type == "array"))
            {
                if (!parameter.Style.HasValue)
                {
                    parameter.Style = ParameterStyle.Form;
                }
            }
        }
    }

    /// <summary>
    /// Swagger Operation filter
    /// </summary>
    /// <seealso cref="IOperationFilter" />
    public class SwaggerOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                operation.OperationId = $"{controllerActionDescriptor.ControllerName}_{controllerActionDescriptor.ActionName.Replace("Async", string.Empty)}";
            }
        }
    }
}
