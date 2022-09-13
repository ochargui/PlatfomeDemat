using Api.Extensions;
using Autofac;
using DEMAT.Api.Extensions;
using DEMAT.Api.Filters;
using DEMAT.ApplicationService.Module;
using DEMAT.ApplicationServices.Helper;
using DEMAT.ApplicationServices.Identity;
using DEMAT.ApplicationServices.Jwt;
using DEMAT.ApplicationServices.Sendgrid;
using DEMAT.Filters;
using DEMAT.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Prometheus;

namespace DEMAT
{
    /// <summary>	
    /// Startup app	
    /// </summary>
    public class Startup
    {
        private readonly string _policyName = "DEMAT API";

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors(_policyName);
            services.AddControllers(options =>
            {
                options.Filters.Add(new ApiExceptionFilterAttribute());
                options.ValueProviderFactories.Insert(0, new SeparatedQueryStringValueProviderFactory(","));
            }
            );
            //services.ConfigureAuthentication(Configuration);
            services.ConfigureContext(Configuration);
            services.AddDbContext<AppIdentityDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("IdentityConnection")));
           
            services.AddIdentityServices(Configuration);
            services.ConfigureSwagger();
            services.ConfigureMediatR();
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
              );

            // Cloudinary Services
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
        }

        /// <summary>
        /// Configures the container.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());

            builder.RegisterType<AuthUserService>().As<IAuthUserService>();
            builder.RegisterType<TokenService>().As<ITokenService>();
            builder.RegisterType<EmailSender>().As<IEmailSender>();
            builder.RegisterType<AuthUserService>().As<IAuthUserService>();
            builder.RegisterType<TokenService>().As<ITokenService>();
            //builder.RegisterType<BasicAuthenticationUserService>().As<IBasicAuthenticationUserService>();

        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Utilisation des metrics Prometheus
            app.UseMetricServer();
            app.UseHttpMetrics();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();  // Autorest/NSwag won't work if redirect are enabled     
            }

            app.UseCors(_policyName);
            app.UseAuthentication();        // Attention Authentication before Authorization
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ConfigureSwagger();
        }
    }
}
