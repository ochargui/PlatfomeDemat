using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.DownloadJCRepport
{
    public class JCRepportQuery: IRequest<IEnumerable<RapportJCModel>>
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

      
        public JCRepportQuery(DateTimeOffset Datedebut, DateTimeOffset DateFin)
        {
            StartDate = Datedebut;
            EndDate = DateFin;
        }
    }
}
