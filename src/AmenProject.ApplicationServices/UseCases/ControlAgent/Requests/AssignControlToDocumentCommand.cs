using DEMAT.Domain.Entities;
using DEMAT.Domain.Entities.Documents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ControlAgent.Requests
{
    public class AssignControlToDocumentCommand: IRequest<RawDocument>
    {
        public Guid RawDocumentId { get; set; }
        public Guid ControlId { get; set; }

        public AssignControlToDocumentCommand(Guid rawDocumentId , Guid controlId)
        {
            RawDocumentId = rawDocumentId;
            ControlId = controlId;
        }
    }
}
