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
	public class PonderateReadRepository : ReadOnlyRepository<Ponderate>, IPonderateReadRepository
	{
		#region Variables
		/// <summary>
		/// The user context
		/// </summary>
		private readonly IDematContext _amenBankContext;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="PonderateRepository"/> class.
		/// </summary>
		/// <param name="timeContext">The user context.</param>
		public PonderateReadRepository(IDematContext amenBankContext) : base(amenBankContext)
		{
			_amenBankContext = amenBankContext;
		}
		#endregion

		#region Methods
		public async Task<IEnumerable<PonderateModel>> GetAllPonderates(CancellationToken cancellationToken)
		{
			return await _amenBankContext.Ponderate
					.AsNoTracking()
					.Select(x => new PonderateModel()
					{
						IdPonderate = x.Id,
						Nom = x.Nom,
						Valeur = x.Valeur,
						OperationId = x.OperationId
					})
					.ToListAsync(cancellationToken);
		}

		public async Task<PonderateModel> GetPonderateById(Guid ponderateId, CancellationToken cancellationToken)
		{
			return await _amenBankContext.Ponderate
					.Where(x => x.Id == ponderateId)
					.AsNoTracking()
					.Select(x => new PonderateModel()
					{
						IdPonderate = x.Id,
						Nom = x.Nom,
						Valeur = x.Valeur,
						OperationId = x.OperationId
					})
					.FirstOrDefaultAsync(cancellationToken);
		}
		#endregion

	}
}
