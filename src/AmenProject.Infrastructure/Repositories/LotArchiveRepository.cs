using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Infrastructure.Repositories
{
     public class LotArchiveRepository : Repository<LotArchive>, ILotArchiveRepository
    {
        private readonly IDematContext _amenContext;

        public LotArchiveRepository(IDematContext amenContext)
            : base(amenContext)
        {
            _amenContext = amenContext;
        }
    }
}
