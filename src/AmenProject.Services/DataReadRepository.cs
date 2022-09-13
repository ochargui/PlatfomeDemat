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
	public class DataReadRepository : ReadOnlyRepository<Data>, IDataReadRepository
	{
		#region Variables
		/// <summary>
		/// The user context
		/// </summary>
		private readonly IDematContext _amenBankContext;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DataReadRepository"/> class.
		/// </summary>
		/// <param name="timeContext">The user context.</param>
		public DataReadRepository(IDematContext amenBankContext) : base(amenBankContext)
		{
			_amenBankContext = amenBankContext;
		}
		#endregion

		#region Methods
		public async Task<IEnumerable<DataModel>> GetAllDatas(CancellationToken cancellationToken)
		{
			return await _amenBankContext.Data
					.AsNoTracking()
					.Select(x => new DataModel()
					{
						IdData = x.Id,
						ControleValue = x.ControleValue,
						ControleId = x.ControleId,
						ArchiveId = x.ArchiveId
					})
					.ToListAsync(cancellationToken);
		}

        public async Task <IEnumerable<DataModel>> GetDataByArchiveControle(Guid archiveID, int CodeControle, CancellationToken cancellationToken)
        {

			 IEnumerable< DataModel> l = await _amenBankContext.Data
				     .Include(x=> x.Controle)
					.Where(x => x.ArchiveId == archiveID )
					.Where(x => x.Controle.CodeControle == CodeControle)
					.AsNoTracking()
					.Select(x => new DataModel()
					{
						IdData = x.Id,
						ControleValue = x.ControleValue,
						ControleId = x.ControleId,
						ArchiveId = x.ArchiveId
					}).ToListAsync(cancellationToken);
			return l;
		}

        public async Task<DataModel> GetDataById(Guid dataId, CancellationToken cancellationToken)
		{
			return await _amenBankContext.Data
					.Where(x => x.Id == dataId)
					.AsNoTracking()
					.Select(x => new DataModel()
					{
						IdData = x.Id,
						ControleValue = x.ControleValue,
						ControleId = x.ControleId,
						ArchiveId = x.ArchiveId
					})
					.FirstOrDefaultAsync(cancellationToken);
		}
		#endregion

	}
}
