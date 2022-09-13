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
	public class GroupeControleReadRepository : ReadOnlyRepository<GroupeControle>, IGroupeControleReadRepository
	{
		#region Variables
		/// <summary>
		/// The user context
		/// </summary>
		private readonly IDematContext _amenBankContext;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="GroupeControleReadRepository"/> class.
		/// </summary>
		/// <param name="timeContext">The user context.</param>
		public GroupeControleReadRepository(IDematContext amenBankContext) : base(amenBankContext)
		{
			_amenBankContext = amenBankContext;
		}
		#endregion


		#region Methods

		public async Task<GroupeControleModel> GetGroupeControleById(Guid GroupeControleId, CancellationToken cancellationToken)
		{
			return await _amenBankContext.GroupeControle
					.Where(x => x.Id == GroupeControleId)
					.AsNoTracking()
					.Select(x => new GroupeControleModel()
					{
						Id = x.Id,
						Name = x.Name,
						GroupePond = x.GroupePond

					})
					.FirstOrDefaultAsync(cancellationToken);
		}
		public async Task<IEnumerable<GroupeControleModel>> GetAllGroupeControles(CancellationToken cancellationToken)
		{
			return await _amenBankContext.GroupeControle
					.AsNoTracking()
					.Select(x => new GroupeControleModel()
					{
						Id = x.Id,
						Name = x.Name,
						GroupePond = x.GroupePond

					})
					.ToListAsync(cancellationToken);
		}



     
        #endregion

    }
}
