using DEMAT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.Interfaces
{
    public interface IControlReadRepository
    {
        Task<Control> GetControlById(Guid ControlId, CancellationToken cancellationToken);
        Task<Control> GetControlByName(string ControlName, CancellationToken cancellationToken);
        Task<IEnumerable<Control>> GetAllControls(CancellationToken cancellationToken);

    }
}
