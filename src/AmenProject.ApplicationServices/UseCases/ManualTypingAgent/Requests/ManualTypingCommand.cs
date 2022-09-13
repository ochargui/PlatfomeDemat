using DEMAT.Domain.Entities.Documents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ManualTypingAgent.Requests
{
    public class ManualTypingCommand : IRequest<RawDocument>
    {
        public Guid DocumentId { get; set; }

        public string DocumentType { get; set; }
        public ManualTypingCommand(Guid documentId , string documentType)
        {
            DocumentId = documentId;
            DocumentType = documentType;
        }
    }
}
