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
	public class OperationReadRepository : ReadOnlyRepository<Operation>, IOperationReadRepository
	{
		#region Variables
		/// <summary>
		/// The user context
		/// </summary>
		private readonly IDematContext _amenBankContext;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="OperationReadRepository"/> class.
		/// </summary>
		/// <param name="timeContext">The user context.</param>
		public OperationReadRepository(IDematContext amenBankContext) : base(amenBankContext)
		{
			_amenBankContext = amenBankContext;
		}


		#endregion

		#region Methods
		public async Task<IEnumerable<OperationModel>> GetAllOperations(CancellationToken cancellationToken)
		{
			return await _amenBankContext.Operation
					.AsNoTracking()
					.Select(x => new OperationModel()
					{
						Id = x.Id,
						OperationName = x.OperationName, 
						CodeOperation = x.CodeOperation,
						Ponderation = x.Ponderation
	})
					.ToListAsync(cancellationToken);
		}

        public async Task<OperationModel> GetOperationById(Guid operationId, CancellationToken cancellationToken)
        {
			return await _amenBankContext.Operation
				      .Where(x => x.Id == operationId)
					  .AsNoTracking()
					  .Select(x => new OperationModel()
					  {
						  Id = x.Id,
						  OperationName = x.OperationName,
						  CodeOperation = x.CodeOperation,
						  Ponderation = x.Ponderation
					  })
					  .FirstOrDefaultAsync(cancellationToken);

		}






        #endregion

    }
}
