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
    public interface IAgenceReadRepository
    {
        Task<AgenceModel> GetAgenceById(Guid agenceId, CancellationToken cancellationToken);
        Task<IEnumerable<AgenceModel>> GetAllAgences(CancellationToken cancellationToken);
        Task<IEnumerable<AgenceModel>> GetAgenceByDateEtat(int Etat, DateTimeOffset DateFin, CancellationToken cancellationToken);

    }
}    
