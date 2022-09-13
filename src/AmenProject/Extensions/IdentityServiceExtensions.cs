using DEMAT.Infrastructure.Identity;
using DEMAT.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services , IConfiguration config)
        {
            var builder = services.AddIdentity<AppUser,IdentityRole>((option)=> {
                //Lockout account parameters
                option.Lockout.MaxFailedAccessAttempts = 3;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders(); ;

            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<AppIdentityDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option => {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                    ValidIssuer = config["Token:Issuer"],
                    ValidateIssuer = true,
                    RequireExpirationTime=true,
                    ValidateAudience = false

                };
            });
            services.Configure<DataProtectionTokenProviderOptions>(opt =>opt.TokenLifespan = TimeSpan.FromHours(2));
            return services;

        }
    }
}
