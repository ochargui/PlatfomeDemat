using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.Infrastructure.Repositories
{
    public class PacketRepository : Repository<LotPacket>, IPacketRepository
    {
      
        private readonly IDematContext _amenContext;

        public PacketRepository(IDematContext amenContext)
            : base(amenContext)
        {
            _amenContext = amenContext;
        }



       
    }
}
