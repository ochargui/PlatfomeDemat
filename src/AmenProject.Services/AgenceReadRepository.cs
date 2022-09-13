using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Models;
using DEMAT.Persistence;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.Services
{
    public class AgenceReadRepository : ReadOnlyRepository<Agence>, IAgenceReadRepository
    {
		#region Variables
		/// <summary>
		/// The user context
		/// </summary>
		private readonly IDematContext _amenBankContext;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AgenceReadRepository"/> class.
		/// </summary>
		/// <param name="timeContext">The user context.</param>
		public AgenceReadRepository(IDematContext amenBankContext) : base(amenBankContext)
		{
			_amenBankContext = amenBankContext;
		}

    #endregion

        #region Methods

        public async Task<AgenceModel> GetAgenceById(Guid agenceId, CancellationToken cancellationToken)
		{
			return await _amenBankContext.Agence
					.Where(x => x.Id == agenceId)
					.AsNoTracking()
					.Select(x => new AgenceModel()
					{
						Id = x.Id,
						CodeAgence = x.CodeAgence,
						NomAgence = x.NomAgence,
						Adresse = x.Adresse,
						ZoneAgenceId = x.ZoneAgenceId
					})
					.FirstOrDefaultAsync(cancellationToken);

		}
		public async  Task<IEnumerable<AgenceModel>> GetAllAgences(CancellationToken cancellationToken)
		{
			return await _amenBankContext.Agence
					.AsNoTracking()
					.Select(x => new AgenceModel()
					{
						Id = x.Id,
						CodeAgence = x.CodeAgence,
						NomAgence = x.NomAgence,
						Adresse = x.Adresse,
						ZoneAgenceId = x.ZoneAgenceId
                    })
					.OrderBy(x => x.NomAgence)
					.ToListAsync(cancellationToken);
		}
		public async Task<IEnumerable<AgenceModel>> GetAgenceByDateEtat(int etat, DateTimeOffset DateFin, CancellationToken cancellationToken)
		{
			DateTimeOffset start = new DateTimeOffset(DateFin.DateTime.Date);
			DateTimeOffset Latest = new DateTimeOffset(DateFin.DateTime.Date).AddDays(1).AddSeconds(-1);

			IEnumerable<AgenceModel>Result2 = await (from a in _amenBankContext.Archive
													 join l in _amenBankContext.Agence on a.AgenceId equals  l.Id
													 where ( a.Etat==etat ) &&
													 (a.CreatedDate >= start ) && (a.CreatedDate <= Latest)
													 select new AgenceModel()
													 {
														 Id = l.Id,
														 CodeAgence = l.CodeAgence,
														 NomAgence = l.NomAgence,
														 ZoneAgenceId = l.ZoneAgenceId

													 }).Distinct()
													 .ToListAsync(cancellationToken);
	


			return Result2; 
		}


		#endregion

	}
}
