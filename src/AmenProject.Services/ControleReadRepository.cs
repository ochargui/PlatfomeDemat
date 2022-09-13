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
	public class ControleReadRepository : ReadOnlyRepository<Controle>, IControleReadRepository
	{
		#region Variables
		/// <summary>
		/// The user context
		/// </summary>
		private readonly IDematContext _amenBankContext;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ControleReadRepository"/> class.
		/// </summary>
		/// <param name="timeContext">The user context.</param>
		public ControleReadRepository(IDematContext amenBankContext) : base(amenBankContext)
		{
			_amenBankContext = amenBankContext;
		}


		#endregion

		#region Methods
		public async Task<IEnumerable<ControleModel>> GetAllControles(CancellationToken cancellationToken)
		{
			return await _amenBankContext.Controle
					.AsNoTracking()
					.Select(x => new ControleModel()
					{
						Id = x.Id,
						Name = x.Name,
						CodeAnomalie = x.CodeAnomalie,
						GroupeControleId = x.GroupeControleId,
						CodeControle = x.CodeControle
					})
					.ToListAsync(cancellationToken);
		}

       
        public async Task<ControleModel> GetControleById(Guid controleId, CancellationToken cancellationToken)
        {
			return await _amenBankContext.Controle
				    .Where(x => x.Id == controleId)
					.AsNoTracking()
					.Select(x => new ControleModel()
					{
						Id = x.Id,
						Name = x.Name,
						CodeAnomalie = x.CodeAnomalie,
						GroupeControleId = x.GroupeControleId,
						CodeControle = x.CodeControle
					})
					.FirstOrDefaultAsync(cancellationToken);
		}

		public async  Task<ControleModel> GetControleByCode(int CodeControle, CancellationToken cancellationToken)
		{
			return await _amenBankContext.Controle
					.Where(x => x.CodeControle  == CodeControle )
					.AsNoTracking()
					.Select(x => new ControleModel()
					{
						Id = x.Id,
						Name = x.Name,
						CodeAnomalie = x.CodeAnomalie,
						GroupeControleId = x.GroupeControleId
					})
					.FirstOrDefaultAsync(cancellationToken);
		}



		#endregion

	}
}
