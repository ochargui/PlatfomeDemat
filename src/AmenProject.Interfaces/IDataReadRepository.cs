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
    public interface IDataReadRepository
    {
        Task<DataModel> GetDataById(Guid dataId, CancellationToken cancellationToken);

        Task<IEnumerable<DataModel>> GetAllDatas(CancellationToken cancellationToken);

        Task<IEnumerable<DataModel>> GetDataByArchiveControle (Guid archiveID , int CodeControle , CancellationToken cancellationToken);

    }
}

