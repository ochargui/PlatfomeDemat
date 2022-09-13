using DEMAT.Domain.Entities;
using DEMAT.Models;
using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.Domain.Interfaces
{
    public interface IArchiveReadRepository
    {
        Task<ArchiveModel> GetArchiveById(Guid archiveId, CancellationToken cancellationToken);
        Task<IEnumerable<ArchiveModel>> GetAllArchives(CancellationToken cancellationToken);
        Task<string> InsertArchiveUpdateEtatPacket(Guid PacketId , CancellationToken cancellationToken);
        Task <IEnumerable<ArchiveDetailsModel>> GetListArchiveByIdAgence(Guid agenceId, CancellationToken cancellationToken);
        
      
        Task<IEnumerable<DocumentsModel>> ListDocumentsAgence(IEnumerable<AgenceModel> agences, CancellationToken cancellationToken);

        Task<IEnumerable<ArchiveDetailsModel>> GetListArchiveByAgenceOperation(Guid agenceId, Guid opearationId, CancellationToken cancellationToken);
        Task<IEnumerable<DocumentsModel>> ListDocumentsAgenceOperation(Guid IdOperation ,IEnumerable<AgenceModel> agences, CancellationToken cancellationToken);
       
        Task<IEnumerable<ArchiveDetailsModel>> GetListArchiveByAgenceDate(Guid agenceId, DateTimeOffset DateDebut, DateTimeOffset dateFin, CancellationToken cancellationToken);
        Task<IEnumerable<DocumentsModel>> ListDocumentsAgenceDate(DateTimeOffset DateDebut, DateTimeOffset dateFin, IEnumerable<AgenceModel> agences, CancellationToken cancellationToken);

      
        Task<IEnumerable<ArchiveDetailsModel>> GetListArchiveByAgenceDateOperation (Guid agenceId, Guid opearationId, DateTimeOffset DateDebut, DateTimeOffset dateFin, CancellationToken cancellationToken);
        Task<IEnumerable<DocumentsModel>> ListDocumentsAgenceDateOperation(DateTimeOffset DateDebut, DateTimeOffset DateFin, Guid opearationId, IEnumerable<AgenceModel> agences, CancellationToken cancellationToken);

        Task<IEnumerable<ArchiveDetailsModel>> GetListArchiveEtatCodeOperation(int Etat, CancellationToken cancellationToken);
        Task<IEnumerable<ArchiveModel>> ListArchiveByEtatJourne(int Etat , string NomLotPcket, CancellationToken cancellationToken);
       
        Task<IEnumerable<JourneOperationModel>> ListArchiveJourne(CancellationToken cancellationToken);

        Task<IEnumerable<DocumentsEnAttenteModel>> ListArchiveByEtatAgence(int Etat, IEnumerable<AgenceModel> agences, CancellationToken cancellationToken);

        Task<IEnumerable<ArchiveModel>> GetListArchiveEtatOperationId(int Etat,Guid IdOperation, CancellationToken cancellationToken);
        Task<IEnumerable<ArchiveOperation>> GetListArchiveOperation(int Etat, CancellationToken cancellationToken);
        
        Task<IEnumerable<ArchiveDetailsModel>> GetListArchiveOperatorDate(DateTimeOffset DateDebut, DateTimeOffset DateFin, Guid OpearatorId, int Etat, CancellationToken cancellationToken);

        Task<IEnumerable<ArchiveDetailsModel>> GetListArchiveByAgenceEtatDateOperation(Guid agenceId,int Etat, Guid opearationId, DateTimeOffset DateFinTypage, CancellationToken cancellationToken);

        Task<IEnumerable<ArchiveDetailsModel>> GetListArchiveByAgenceDateOperationLotArchiveId(Guid agenceId, Guid opearationId, DateTimeOffset DateTraitement, Guid LotArchiveId, CancellationToken cancellationToken);

        Task<IEnumerable<ArchiveDetailsModel>> GetArchiveByDateFinSaisieEtatAgence(Guid agenceId, int Etat, DateTimeOffset DateDebutTypage, DateTimeOffset DateFinTypage, CancellationToken cancellationToken);
        Task<IEnumerable<ArchiveDetailsModel>> ListArchiveByEtatAgenceLotDate(Guid agenceId, int Etat,Guid LotArchiveId, DateTimeOffset DateDebutTypage, DateTimeOffset DateFinTypage, CancellationToken cancellationToken);
        Task<IEnumerable<ArchiveDetailsModel>> ListArchiveByEtatAgenceLotArchive(Guid agenceId, int Etat,Guid LotArchiveId, CancellationToken cancellationToken);

        Task<ArchiveDetailsModel> GetLotArchiveByLotArchiveId(Guid LotArchiveId, CancellationToken cancellationToken);
        Task<IEnumerable<ArchiveDetailsModel>> GetLotArchiveByLotArchiveIdEtatAgenceId(Guid LotArchiveId, Guid AgenceId, int Etat, CancellationToken cancellationToken);
        Task<string> CreateArchive(Guid DocId, Guid OperatorId, Guid OperatiobID, CancellationToken cancellationToken);



    }

    
}
