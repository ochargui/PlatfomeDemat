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
    public interface IPacketReadRepository 
    {
        Task<PacketModel> GetPacketById(Guid packetId, CancellationToken cancellationToken);      
        
        Task<IEnumerable<PacketModel>> GetAllPackets(CancellationToken cancellationToken);

        Task<string> CopieInputFilesToOutputLocal(OperateurModel op ,CancellationToken cancellationToken);

        Task<PacketModel> GetPacketByIdDocBrute(Guid DocBruteID, CancellationToken cancellationToken);

        Task<string> CreateDirectories( CancellationToken cancellationToken);
        Task<IEnumerable<PacketModel>>  GetListPacketByEtatDocBrute( int Etat , CancellationToken cancellationToken);






    }
}
