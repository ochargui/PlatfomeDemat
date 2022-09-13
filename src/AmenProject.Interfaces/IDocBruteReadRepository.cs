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
    public interface IDocBruteReadRepository
    {
        Task<DocBruteModel> GetDocBruteById(Guid docBruteId, CancellationToken cancellationToken);

        Task<IEnumerable<DocBrute>> GetDocBruteByIdPacket(Guid IdPacket, CancellationToken cancellationToken);

        Task<IEnumerable<DocBruteModel>> GetAllDocBrutes(CancellationToken cancellationToken);

        Task<IEnumerable<DocBrutePacketRow>> GetDocBruteByPacket(PacketModel packet, CancellationToken cancellationToken);
        Task<IEnumerable<DocBruteModel>> GetDocBruteByEtatByLotPacketId(int EtatDoc,Guid IdPacket, CancellationToken cancellationToken);
        
        Task<string> InsertDocBrute(string path, PacketModel packet, OperateurModel op);

        Task<string> UpdateEtatDocBrute(Guid docBruteId ,int etat, CancellationToken cancellationToken);

        Task<IEnumerable<DocumentsEnAttenteModel>> ListDocsByEtatAgence(int Etat, IEnumerable<AgenceModel> agences, CancellationToken cancellationToken);
        Task<IEnumerable<JourneOperationModel>> ListDOcsJourne(CancellationToken cancellationToken);
        Task<IEnumerable<DocBruteModel>> ListDocsByEtatJourne(int Etat, string Journe, CancellationToken cancellationToken);
    }
}
