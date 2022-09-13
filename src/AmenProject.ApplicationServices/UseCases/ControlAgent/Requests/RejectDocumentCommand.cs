using DEMAT.Domain.Entities.Documents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ControlAgent.Requests
{
    public class RejectDocumentCommand : IRequest<RawDocument>
    {

        public Guid DocumentId { get; set; }

        public RejectDocumentCommand(Guid documentId)
        {
            this.DocumentId = documentId;
        }
    }
}
