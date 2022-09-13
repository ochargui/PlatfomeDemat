using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.RapportFichierJournalier
{
     public class RapportFichierJournalierQuery : IRequest<IEnumerable<RapportJournalierModel>>
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public RapportFichierJournalierQuery(DateTimeOffset Datedebut, DateTimeOffset DateFin)
        {
            StartDate = Datedebut;
            EndDate = DateFin;
        }
    }
}
