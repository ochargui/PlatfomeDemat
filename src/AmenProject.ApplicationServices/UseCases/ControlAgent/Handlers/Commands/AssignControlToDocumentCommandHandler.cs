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
    public class AssignControlToDocumentCommandHandler : IRequestHandler<AssignControlToDocumentCommand, RawDocument>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;
        private readonly IRawDocumentReadRepository _rawDocumentReadRepository;
        private readonly IControlReadRepository _controlReadRepository;

        public AssignControlToDocumentCommandHandler(IAmenUnitOfWork amenUnitOfWork, 
            IRawDocumentReadRepository rawDocumentReadRepository,
            IControlReadRepository controlReadRepository)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _rawDocumentReadRepository = rawDocumentReadRepository;
            _controlReadRepository = controlReadRepository;

        }
        public async Task<RawDocument> Handle(AssignControlToDocumentCommand request, CancellationToken cancellationToken)
        {
            var rawDocument = await _rawDocumentReadRepository.GetDocumentById(request.RawDocumentId,cancellationToken);

            if (rawDocument == null)
            {
                throw new Exception("There is no document with such an Id");
            }
            var control =  await _controlReadRepository.GetControlById(request.ControlId, cancellationToken);

            if (control == null)
            {
                throw new Exception("There is no control with such an Id");
            }

            rawDocument.Control = control;
            await _amenUnitOfWork.RawDocumentRepository.UpdateAsync(rawDocument, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            return rawDocument;
        }
    }
}
