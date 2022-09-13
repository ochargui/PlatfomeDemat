using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Infrastructure.Repositories
{
    public class ArchiveRepository : Repository<Archive>, IArchiveRepository
    {
        private readonly IDematContext _amenContext;

        public ArchiveRepository(IDematContext amenContext)
            : base(amenContext)
        {
            _amenContext = amenContext;
        }
    }
}
