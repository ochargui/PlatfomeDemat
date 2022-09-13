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

namespace DEMAT.Services
{
	public class ZoneAgenceReadRepository : ReadOnlyRepository<ZoneAgence>, IZoneAgenceReadRepository
	{
		#region Variables
		/// <summary>
		/// The user context
		/// </summary>
		private readonly IDematContext _amenBankContext;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ZoneAgenceReadRepository"/> class.
		/// </summary>
		/// <param name="timeContext">The user context.</param>
		public ZoneAgenceReadRepository(IDematContext amenBankContext) : base(amenBankContext)
		{
			_amenBankContext = amenBankContext;
		}


		#endregion

		#region Methods

		public async Task<ZoneAgenceModel> GetZoneAgenceById(Guid zoneAgenceId, CancellationToken cancellationToken)
		{
			return await _amenBankContext.ZoneAgence
				.Where(x => x.id == zoneAgenceId)
					.AsNoTracking()
					.Select(x => new ZoneAgenceModel()
					{
						id = x.id,
						codeZoneAgence = x.codeZoneAgence,
						ZoneAgenceAdresse = x.ZoneAgenceAdresse
					})
				    .FirstOrDefaultAsync(cancellationToken);
		}

		public async Task<IEnumerable<ZoneAgenceModel>> GetAllZoneAgences(CancellationToken cancellationToken)
		{
			return await _amenBankContext.ZoneAgence
					.AsNoTracking()
					.Select(x => new ZoneAgenceModel()
					{
						id = x.id,
						codeZoneAgence = x.codeZoneAgence,
						ZoneAgenceAdresse = x.ZoneAgenceAdresse
					})
					.ToListAsync(cancellationToken);
		}

       




        #endregion

    }
}
