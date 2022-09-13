using DEMAT.ApplicationServices.UseCases.ControlAgent.Requests;
using DEMAT.Domain.Entities;
using DEMAT.Domain.Entities.Documents;
using DEMAT.Domain.Interfaces;
using DEMAT.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ControlAgent.Handlers.Commands
{
    public class ControlCommandHandler : IRequestHandler<ControlCommand, RawDocument>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;
        private readonly IRawDocumentReadRepository _rawDocumentReadRepository;

        public ControlCommandHandler(IAmenUnitOfWork amenUnitOfWork,
            IRawDocumentReadRepository rawDocumentReadRepository)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _rawDocumentReadRepository = rawDocumentReadRepository;

        }
        public async Task<RawDocument> Handle(ControlCommand request, CancellationToken cancellationToken)
        {
            var rawDocument = await _rawDocumentReadRepository.GetDocumentById(request.DocumentId, cancellationToken);

            if (rawDocument == null)
            {
                throw new Exception("There is no document with such an Id");
            }
            var controlDocumentFields = new DocumentFields();
            if (rawDocument.DocumentFields == null)
            {
                controlDocumentFields = new DocumentFields
                {
                    FieldNumber = request.FieldNumber,
                    ClientSignature = request.ClientSignature,
                    BankStamp = request.BankStamp,

                    
                };
            }
            else
            {
                controlDocumentFields = rawDocument.DocumentFields;
                controlDocumentFields.FieldNumber = request.FieldNumber;
                controlDocumentFields.ClientSignature = request.ClientSignature;
                controlDocumentFields.BankStamp = request.BankStamp;
                
            }


            if (rawDocument.Control ==null)
            {
                throw new Exception("control is empty");
            }
            //Calculate total  Score of fields
            var totalScore = 0;

            if (request.FieldNumber != null)
            {
                totalScore += rawDocument.Control.FieldNumberScore;
            }

            if (request.ClientSignature != null)
            {
                totalScore += rawDocument.Control.ClientSignatureScore;
            }
   
            if (request.BankStamp != null)
            {
                totalScore += rawDocument.Control.BankStampScore;
            }


            if (totalScore<rawDocument.Control.ScoreLimit)
            {
                rawDocument.State = StateTypes.PotentielRejected;
            }
            else
            {

            rawDocument.State =StateTypes.ValidatedByOperator;
            }
                
            rawDocument.DocumentFields = controlDocumentFields;
            rawDocument.ControledBy = request.Email;
            await _amenUnitOfWork.RawDocumentRepository.UpdateAsync(rawDocument, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);


            return rawDocument;



        }
    }
}
