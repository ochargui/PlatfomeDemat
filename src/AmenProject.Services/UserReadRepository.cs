using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Models;
using Microsoft.EntityFrameworkCore;
using DEMAT.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DEMAT.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using DEMAT.Infrastructure.Identity;

namespace DEMAT.Services
{
	public class UserReadRepository : ReadOnlyRepository<AppUser>, IUserReadRepository
	{
		#region Variables
		/// <summary>
		/// The user context
		/// </summary>
		/// 
		private readonly UserManager<AppUser> _userManager;
		
		/*
		private readonly IDematContext _amenBankContext;
		private readonly IArchiveReadRepository _archiveReadRepository;
		private readonly IDocBruteReadRepository _docBruteReadRepository;  
	

		private readonly IAmenUnitOfWork _amenUnitOfWork;

		*/
		private readonly AppIdentityDbContext _appIdentityContext;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="UserReadRepository"/> class.
		/// </summary>
		/// <param name="timeContext">The user context.</param>

		public UserReadRepository(IAmenUnitOfWork amenUnitOfWork, AppIdentityDbContext appIdentityContext, UserManager<AppUser> userManager, IDematContext amenBankContext, IArchiveReadRepository archiveReadRepository, IDocBruteReadRepository docBruteReadRepository) : base(amenBankContext)

		{
			/*
			_amenBankContext = amenBankContext;
			_archiveReadRepository = archiveReadRepository;
			_docBruteReadRepository = docBruteReadRepository;
			_amenUnitOfWork = amenUnitOfWork;
			*/

			_userManager = userManager;
			_appIdentityContext = appIdentityContext;
		}

        public Task<AppUser> GetUserById(Guid Id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


		#endregion

		#region Methods
		/*
		public async Task<IEnumerable<OperateurModel>> GetAllOperators(CancellationToken cancellationToken)
		{
			string fmt = "d";
			return await _amenBankContext.Operateur
						.AsNoTracking()
						.Select(x => new OperateurModel()
						{
							id = x.id,
							nom = x.nom,
							prenom = x.prenom,
							login = x.login,
							password = x.password,
							mail = x.mail,
							NumTel = x.NumTel,
							DateRecrutement = x.DateRecrutement.Date.ToString(fmt),
							Role = x.Role,
							Discipline = x.Discipline,
							Equipe = x.Equipe,

							AgenceID = (Guid)x.AgenceId,
							AgenceName = x.Agence.NomAgence


						})
						.ToListAsync(cancellationToken);
		}
		

		*/

		/*
		public async Task<AppUser> GetUserById(Guid id, CancellationToken cancellationToken)
		{
			string fmt = "d";
			return await _userManager.Users
					  //.Where(x => x.Id == IdUser)
					  .Where(x => x.UserName.Equals(id))
					 .AsNoTracking()
					 .Select(x => new AppUser()
					 {
						 UserName = x.Id,
						 DisplayName = x.DisplayName,
						 LastName = x.LastName,
						 FirstName = x.FirstName,
						 Email = x.Email,
						 PasswordHash = x.PasswordHash,


					 })
					 .FirstOrDefaultAsync(cancellationToken);

		}

*/


/*
		public async Task<AppUser> GetById(Guid id)
		{
			string sqlQuery = "SELECT * FROM Users WHERE Id = @Id";
			using (var connection = _userManager.CreateConnection())
			{
				return await connection.QuerySingleAsync<AppUser>(sqlQuery, new { Id = id });
			}
		}
*/
	} 
}

#endregion