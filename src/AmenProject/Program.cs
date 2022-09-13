using Autofac.Extensions.DependencyInjection;
using DEMAT;
using DEMAT.Infrastructure.Identity;
using DEMAT.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AmenProject
{
    /// <summary>
    /// Program 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static async Task Main(string[] args)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .Build();


            Log.Logger = new LoggerConfiguration()
                //.Enrich.WithCorrelationIdHeader("X-Correlation-ID")
#if !DEBUG
                .WriteTo.Console(new Serilog.Formatting.Elasticsearch.ElasticsearchJsonFormatter())
#endif
                //.WriteTo.Seq("http://localhost:5341")
                //.Destructure.ByTransforming<Customer>(c => new { c.Id, c.Name, c.Email })
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    Log.Information("Starting up");
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await identityContext.Database.MigrateAsync();
                    await AppIdentityDbContextSeed.SeedUsersAsync(userManager, roleManager);

                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "Application start-up failed");
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }
            host.Run();
          
        }

        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
