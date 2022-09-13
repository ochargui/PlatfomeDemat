using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.ListDocumentsAgenceDate
{
    public class ListDocumentsAgenceDateQuery:  IRequest<IEnumerable<DocumentsModel>>
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public ListDocumentsAgenceDateQuery(DateTimeOffset Datedebut , DateTimeOffset DateFin)
        {
            StartDate = Datedebut;
            EndDate = DateFin;
        }

    }


}
