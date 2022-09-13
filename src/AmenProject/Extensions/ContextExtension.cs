using DEMAT.ApplicationServices.Identity;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Infrastructure.Identity;
using DEMAT.Infrastructure.Identity.Models;
using DEMAT.Infrastructure.Repositories;
using DEMAT.Interfaces;
using DEMAT.Interfaces.Repositories;
using DEMAT.Persistence;
using DEMAT.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace DEMAT.Api.Extensions
{
	/// <summary>
	/// Contexte extension
	/// </summary>
	public static class ContextExtension
	{
		/// <summary>
		/// Registers the context.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="configuration">The configuration.</param>
		public static void ConfigureContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<DematContext>(options => options.UseNpgsql(GetConnectionInfo(configuration).ToString()));
			services.AddDbContext<AppIdentityDbContext>(options => options.UseNpgsql(GetIdentityConnectionInfo(configuration).ToString()));

			services.TryAddScoped<IDematContext, DematContext>();
			services.TryAddScoped<IdentityDbContext<AppUser>, AppIdentityDbContext>();
			//services.TryAddScoped<IEntityTypeConfiguration<AppUser>, AppIdentityDbContextSeed>();
			services.TryAddScoped<IAmenUnitOfWork, DematUnitOfWork>();
			/*services.AddIdentity<AppUser, IdentityRole>(options =>
			{
				options.SignIn.RequireConfirmedEmail = true;
			})
.AddEntityFrameworkStores<AppIdentityDbContext>()
.AddDefaultTokenProviders();*/

			// Read repository
			services.TryAddScoped<IAgenceReadRepository, AgenceReadRepository>();
			services.TryAddScoped<IAllPonderationReadRepository, AllPonderationReadRepository>();
			services.TryAddScoped<IArchiveReadRepository, ArchiveReadRepository>();
			services.TryAddScoped<IControleReadRepository, ControleReadRepository>();
			services.TryAddScoped<IDataReadRepository, DataReadRepository>();
			services.TryAddScoped<IDocBruteReadRepository, DocBruteReadRepository>();
			services.TryAddScoped<IGroupeControleReadRepository, GroupeControleReadRepository>();
			services.TryAddScoped<IOperateurReadRepository, OperateurReadRepository>();
			services.TryAddScoped<IOperationReadRepository, OperationReadRepository>();
			services.TryAddScoped<IPacketReadRepository, PacketReadRepository>();
			services.TryAddScoped<IPonderateReadRepository, PonderateReadRepository>();
			services.TryAddScoped<ITypologiesReadRepository, TypologiesReadRepository>();
			services.TryAddScoped<IZoneAgenceReadRepository, ZoneAgenceReadRepository>();
			services.TryAddScoped<IReportingReadRepository, ReportingReadRepository>();
			services.TryAddScoped<ILotArchiveReadRepository, LotArchiveReadRepository>();
			services.TryAddScoped<IRawDocumentReadRepository, RawDocumentReadRepository>();
			services.TryAddScoped<IControlReadRepository, ControlReadRepository>();

			services.TryAddScoped<IAuthUserService, AuthUserService>();

			// Create / Update repository
			services.TryAddScoped<IAgenceRepository, AgenceRepository>();
			services.TryAddScoped<IAllPonderationRepository, AllPonderationRepository>();
			services.TryAddScoped<IArchiveRepository, ArchiveRepository>();
			services.TryAddScoped<IControleRepository, ControleRepository>();
			services.TryAddScoped<IDataRepository, DataRepository>();
			services.TryAddScoped<IDocBruteRepository, DocBruteRepository>();
			services.TryAddScoped<IGroupeControleRepository, GroupeControleRepository>();
			services.TryAddScoped<IOperateurRepository, OperateurRepository>();
			services.TryAddScoped<IOperationRepository, OperationRepository>();
			services.TryAddScoped<IPacketRepository, PacketRepository>();
			services.TryAddScoped<IPonderateRepository, PonderateRepository>();
			services.TryAddScoped<ITypologiesRepository, TypologiesRepository>();
			services.TryAddScoped<IZoneAgenceRepository, ZoneAgenceRepository>();
			services.TryAddScoped<ILotArchiveRepository, LotArchiveRepository>();
			services.TryAddScoped<IRawDocumentRepository, RawDocumentRepository>();
			services.TryAddScoped<IControlRepository, ControlRepository>();


		}

		/// <summary>
		/// Gets the connection information.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <returns></returns>
		private static DbConnectionInfo GetConnectionInfo(IConfiguration configuration)
		{
			DbConnectionInfo dbConnectionInfo;

			if (Environment.OSVersion.Platform == PlatformID.Unix)
			{
				dbConnectionInfo = new DbConnectionInfo
				{
					Host = Environment.GetEnvironmentVariable("PG_HOST"),
					Database = Environment.GetEnvironmentVariable("PG_DATABASE"),
					Username = Environment.GetEnvironmentVariable("PG_USERNAME"),
					Password = Environment.GetEnvironmentVariable("PG_PASSWORD")
				};
			}
			else
			{
				dbConnectionInfo = new DbConnectionInfo
				{
					Host = configuration.GetValue<string>("DataConnection:Hostname"),
					Database = configuration.GetValue<string>("DataConnection:Database"),
					Username = configuration.GetValue<string>("DataConnection:Username"),
					Password = configuration.GetValue<string>("DataConnection:Password")
				};
			}

			return dbConnectionInfo;
		}
		private static DbConnectionInfo GetIdentityConnectionInfo(IConfiguration configuration)
		{
			DbConnectionInfo dbConnectionInfo;



			if (Environment.OSVersion.Platform == PlatformID.Unix)
			{
				dbConnectionInfo = new DbConnectionInfo
				{
					Host = Environment.GetEnvironmentVariable("PG_HOST"),
					Database = Environment.GetEnvironmentVariable("PG_IDENTITY_DATABASE"),
					Username = Environment.GetEnvironmentVariable("PG_IDENTITY_USERNAME"),
					Password = Environment.GetEnvironmentVariable("PG_IDENTITY_PASSWORD")
				};
			}
			else
			{
				dbConnectionInfo = new DbConnectionInfo
				{
					Host = configuration.GetValue<string>("DataConnectionIdentity:Hostname"),
					Database = configuration.GetValue<string>("DataConnectionIdentity:Database"),
					Username = configuration.GetValue<string>("DataConnectionIdentity:Username"),
					Password = configuration.GetValue<string>("DataConnectionIdentity:Password")
				};
			}



			return dbConnectionInfo;
		}
	}
}
