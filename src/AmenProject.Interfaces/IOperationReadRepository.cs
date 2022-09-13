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
    public interface IOperationReadRepository 
    {
        Task<OperationModel> GetOperationById(Guid operationId, CancellationToken cancellationToken);
        Task<IEnumerable<OperationModel>> GetAllOperations(CancellationToken cancellationToken);
    }
}
