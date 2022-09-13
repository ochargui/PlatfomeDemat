using DEMAT.Domain.Entities;
using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.Domain.Interfaces
{
    public interface IPacketRepository : IRepository<LotPacket>
    {
        
    }
}
