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
    public interface IControleReadRepository
    {
        Task<ControleModel> GetControleById(Guid controleId, CancellationToken cancellationToken);

        Task<IEnumerable<ControleModel>> GetAllControles(CancellationToken cancellationToken);
        Task<ControleModel> GetControleByCode(int CodeControle, CancellationToken cancellationToken);
    }
}
