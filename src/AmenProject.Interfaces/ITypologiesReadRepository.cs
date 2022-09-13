using DEMAT.Domain.Entities;
using DEMAT.Models;
using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.Domain.Interfaces
{
    public interface ITypologiesReadRepository
    {
        Task<TypologieModel> GetPacketById(Guid typologieId, CancellationToken cancellationToken);

        Task<IEnumerable<TypologieModel>> GetAllTypologies(CancellationToken cancellationToken);
    }
}
