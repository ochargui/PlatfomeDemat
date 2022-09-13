using DEMAT.Domain.Entities;
using DEMAT.Infrastructure;
using DEMAT.Interfaces;
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
    public class ControlReadRepository : ReadOnlyRepository<Control>, IControlReadRepository
    {
        private readonly IDematContext _amenBankContext;
        public ControlReadRepository(IDematContext amenBankContext) : base(amenBankContext)
        {
            _amenBankContext = amenBankContext;

        }
        public async Task<Control> GetControlById(Guid controlId, CancellationToken cancellationToken)
        {
            var control = await _amenBankContext.Controls
                .Where(x => x.Id == controlId)
                .FirstOrDefaultAsync(cancellationToken);
            return control;
        }

        public async Task<Control> GetControlByName(string ControlName, CancellationToken cancellationToken)
        {
            var control = await _amenBankContext.Controls
               .Where(x => x.Name == ControlName)
               .FirstOrDefaultAsync(cancellationToken);
            return control;
        }

        public async Task<IEnumerable<Control>> GetAllControls(CancellationToken cancellationToken)
        {
            return await _amenBankContext.Controls
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
