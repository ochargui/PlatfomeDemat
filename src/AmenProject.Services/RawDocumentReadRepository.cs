using DEMAT.Domain.Entities.Documents;
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
    public class RawDocumentReadRepository : ReadOnlyRepository<RawDocument>, IRawDocumentReadRepository
    {
        private readonly IDematContext _amenBankContext;


        public RawDocumentReadRepository(IDematContext amenBankContext) : base(amenBankContext)
        {
            _amenBankContext = amenBankContext;

        }

        public async Task<IEnumerable<RawDocument>> GetAllRawDocuments(CancellationToken cancellationToken)
        {
            return await _amenBankContext.RawDocuments
                .AsNoTracking()
                .Include(x => x.DocumentPicture)
                .ToListAsync(cancellationToken);
        }

        public async Task<RawDocument> GetDocumentById(Guid DocumentId, CancellationToken cancellationToken)
        {
            var rawDocument = await _amenBankContext.RawDocuments
                .Where(x => x.Id == DocumentId)
                .Include(x => x.DocumentFields)
                .Include(x => x.Control)
                .FirstOrDefaultAsync(cancellationToken);


            return rawDocument;
        }
    }
}
