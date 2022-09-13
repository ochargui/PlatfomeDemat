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
	public class TypologiesReadRepository : ReadOnlyRepository<Typologies>, ITypologiesReadRepository
	{
		#region Variables
		/// <summary>
		/// The user context
		/// </summary>
		private readonly IDematContext _amenBankContext;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="TypologiesReadRepository"/> class.
		/// </summary>
		/// <param name="timeContext">The user context.</param>
		public TypologiesReadRepository(IDematContext amenBankContext) : base(amenBankContext)
		{
			_amenBankContext = amenBankContext;
		}


		#endregion

		#region Methods
		public async Task<IEnumerable<TypologieModel>> GetAllTypologies(CancellationToken cancellationToken)
		{
			return await _amenBankContext.Typologie
					.AsNoTracking()
					.Select(x => new TypologieModel()
					{
						Id = x.Id,
						CibleControle = x.CibleControle,
						CiblePondaration = x.CiblePondaration
					})
					.ToListAsync(cancellationToken);
		}

        public async Task<TypologieModel> GetPacketById(Guid typologieId, CancellationToken cancellationToken)
        {
			return await _amenBankContext.Typologie
                     .Where(x => x.Id == typologieId) 
					.AsNoTracking()
					.Select(x => new TypologieModel()
					{
						Id = x.Id,
						CibleControle = x.CibleControle,
						CiblePondaration = x.CiblePondaration
					})
					.FirstOrDefaultAsync(cancellationToken);
		}

        #endregion

    }
}
