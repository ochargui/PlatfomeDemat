using DEMAT.ApplicationServices.UseCases.RawDocuments.Requests;
using DEMAT.Domain.Entities.Documents;
using DEMAT.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.RawDocuments.Handlers.Queries
{
    public class GetAllRawDocumentsQueryHandler : IRequestHandler<GetAllRawDocumentsQuery, IEnumerable<RawDocument>>
    {
        private readonly IRawDocumentReadRepository _rawDocumentReadRepository;

        public GetAllRawDocumentsQueryHandler(IRawDocumentReadRepository rawDocumentReadRepository)
        {
            _rawDocumentReadRepository = rawDocumentReadRepository;
        }
        public async Task<IEnumerable<RawDocument>> Handle(GetAllRawDocumentsQuery request, CancellationToken cancellationToken)
        {
            return await _rawDocumentReadRepository.GetAllRawDocuments(cancellationToken);
        }
    }
}
