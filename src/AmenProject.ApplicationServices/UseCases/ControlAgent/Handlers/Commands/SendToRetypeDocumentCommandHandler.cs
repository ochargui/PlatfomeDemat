using DEMAT.ApplicationServices.UseCases.ControlAgent.Requests;
using DEMAT.Domain.Entities.Documents;
using DEMAT.Domain.Interfaces;
using DEMAT.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ControlAgent.Handlers.Commands
{
    public class SendToRetypeDocumentCommandHandler : IRequestHandler<SendToRetypeDocumentCommand, RawDocument>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;
        private readonly IRawDocumentReadRepository _rawDocumentReadRepository;

        public SendToRetypeDocumentCommandHandler(IAmenUnitOfWork amenUnitOfWork,
            IRawDocumentReadRepository rawDocumentReadRepository)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _rawDocumentReadRepository = rawDocumentReadRepository;
        }
        public async Task<RawDocument> Handle(SendToRetypeDocumentCommand request, CancellationToken cancellationToken)
        {
           
            var rawDocument = await _rawDocumentReadRepository.GetDocumentById(request.DocumentId,cancellationToken);
            if (rawDocument == null)
            {
                throw new Exception("Document not found");
            }
            rawDocument.State = StateTypes.ToRetyping;

           await _amenUnitOfWork.RawDocumentRepository.UpdateAsync(rawDocument, cancellationToken);
           await _amenUnitOfWork.SaveAsync(cancellationToken);
            return rawDocument;

        }
    }
}
