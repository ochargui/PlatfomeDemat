using DEMAT.ApplicationServices.UseCases.ManualTypingAgent.Requests;
using DEMAT.Domain.Entities.Documents;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ManualTypingAgent.Handlers.Commands
{
    public class ManualTypingCommandHandler : IRequestHandler<ManualTypingCommand, RawDocument>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;
        private readonly IDematContext _amenBankContext;
        private readonly IRawDocumentReadRepository _rawDocumentReadRepository;
        private readonly IControlReadRepository _controlReadRepository;

        public ManualTypingCommandHandler(IAmenUnitOfWork amenUnitOfWork, IDematContext amenBankContext, 
            IRawDocumentReadRepository rawDocumentReadRepository, IControlReadRepository controlReadRepository)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _amenBankContext = amenBankContext;
            _rawDocumentReadRepository = rawDocumentReadRepository;
            _controlReadRepository = controlReadRepository;
        }

        public async Task<RawDocument> Handle(ManualTypingCommand request, CancellationToken cancellationToken)
        {
            var rawDocument = await _rawDocumentReadRepository.GetDocumentById(request.DocumentId, cancellationToken);
            
            if (rawDocument == null)
            {
                throw new Exception("There is no document with such an Id");
            }

            DocumentType documentTypeAfterManualTyping = (DocumentType)Enum.Parse(typeof(DocumentType), request.DocumentType);
            rawDocument.DocumentType = documentTypeAfterManualTyping;

            var control = await _controlReadRepository.GetControlByName(request.DocumentType,cancellationToken);

            rawDocument.State = StateTypes.Typed;
            rawDocument.Control = control;

            await _amenUnitOfWork.RawDocumentRepository.UpdateAsync(rawDocument, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            return rawDocument; 
        }
    }
}
