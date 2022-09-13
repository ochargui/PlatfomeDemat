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
    public interface IAllPonderationReadRepository
    {
        Task<IEnumerable<AllPonderationModel>> GetAllAllPonderations(CancellationToken cancellationToken);
        
        Task<AllPonderationModel> GetAllPonderationById(Guid allPonderationId, CancellationToken cancellationToken);
    }
}
