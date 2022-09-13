using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.ListDocumentsAgenceOperationDate
{
    public class ListDocumentsAgenceOperationDateQuery : IRequest<IEnumerable<DocumentsModel>>
    {
        public DateTimeOffset DateDebut { get; set; }
        public DateTimeOffset DateFin { get; set; }
        public Guid OperationId { get; set; }

        public ListDocumentsAgenceOperationDateQuery(Guid IdOperation, DateTimeOffset StartDate, DateTimeOffset EndDate)
        {
            OperationId = IdOperation;
            DateDebut = StartDate;
            DateFin = EndDate;
        }
    
    }
}
