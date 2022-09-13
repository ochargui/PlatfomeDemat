using DEMAT.Domain.Entities.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.Interfaces
{
    public interface IRawDocumentReadRepository
    {
        Task<RawDocument> GetDocumentById(Guid DocumentId, CancellationToken cancellationToken);

        Task<IEnumerable<RawDocument>> GetAllRawDocuments(CancellationToken cancellationToken);

    }
}
