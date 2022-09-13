using DEMAT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.Interfaces
{
    public interface ILotArchiveReadRepository
    {
        Task<Guid> CreateLotArchiveExist(Guid DocBruteId, CancellationToken cancellationToken);
        Task <IEnumerable<LotArchiveModel>> SelectJourneeByDateFinSaisieEtat(int Etat, DateTimeOffset DateTraitement, CancellationToken cancellationToken);
        Task<LotArchiveModel> GetLotArchiveById(Guid LotArchiveId, CancellationToken cancellationToken);
        Task<IEnumerable<LotArchiveModel>> GetAllLotArchive( CancellationToken cancellationToken);
        Task<LotArchiveModel> GetLotArchiveByName(string LotName, CancellationToken cancellationToken);
        Task<string> CreateOutOutOcrOperationDiercotries(string journee,int AgenceCode,CancellationToken cancellationToken);




    }
}
