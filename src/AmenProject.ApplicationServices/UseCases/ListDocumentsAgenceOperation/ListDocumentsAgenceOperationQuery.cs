using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.ListDocumentsAgenceOperation
{
    public class ListDocumentsAgenceOperationQuery : IRequest<IEnumerable<DocumentsModel>>
    {
        public Guid OperationId { get; set; }

        public ListDocumentsAgenceOperationQuery(Guid operationId)
        {
            OperationId = operationId;
        }
    }
}
