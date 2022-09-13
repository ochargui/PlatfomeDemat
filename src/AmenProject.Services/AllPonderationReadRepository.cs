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
	public class AllPonderationReadRepository : ReadOnlyRepository<AllPonderation>, IAllPonderationReadRepository
	{
		#region Variables
		/// <summary>
		/// The user context
		/// </summary>
		private readonly IDematContext _amenBankContext;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="AllPonderationReadRepository"/> class.
		/// </summary>
		/// <param name="timeContext">The user context.</param>
		public AllPonderationReadRepository(IDematContext amenBankContext) : base(amenBankContext)
		{
			_amenBankContext = amenBankContext;
		}
		#endregion

		#region Methods
		public async Task<IEnumerable<AllPonderationModel>> GetAllAllPonderations(CancellationToken cancellationToken)
		{
			return await _amenBankContext.AllPonderation
					.AsNoTracking()
					.Select(x => new AllPonderationModel()
					{
						Id = x.Id,
						Ponderation = x.Ponderation,
						TotalPoints = x.TotalPoints,
						ControleId = x.ControleId
					})
					.ToListAsync(cancellationToken);
		}

		public async Task<AllPonderationModel> GetAllPonderationById(Guid allPonderationId, CancellationToken cancellationToken)
		{
			return await _amenBankContext.AllPonderation
					.Where(x => x.Id == allPonderationId)
					.AsNoTracking()
					.Select(x => new AllPonderationModel()
					{
						Id = x.Id,
						Ponderation = x.Ponderation,
						TotalPoints = x.TotalPoints,
						ControleId = x.ControleId
					})
					.FirstOrDefaultAsync(cancellationToken);
		}
		#endregion

	}
}
