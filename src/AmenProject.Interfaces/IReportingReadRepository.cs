using DEMAT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.Interfaces
{
    public interface IReportingReadRepository
    {
      
        Task <IEnumerable<RapportJCModel>> SelectJourneeByDateComptable(DateTimeOffset DateDebut, DateTimeOffset DateFin, CancellationToken cancellationToken);
        Task <IEnumerable<RapportQuotidienModel>> DataRapportQuotidien(DateTimeOffset DateTraitement, Boolean journe, CancellationToken cancellationToken);
        Task <IEnumerable<RapportJournalierModel>> DataFichierJournalier (DateTimeOffset DateDebut, DateTimeOffset DateFin, CancellationToken cancellationToken);
        Task <IEnumerable<RapportFacturationModel>> DataRapportFacturation (DateTimeOffset DateDebut, DateTimeOffset DateFin, CancellationToken cancellationToken);



    }
}
