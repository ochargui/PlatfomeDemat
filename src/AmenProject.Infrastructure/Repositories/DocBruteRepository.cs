using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DEMAT.Infrastructure.Repositories
{
    public class DocBruteRepository : Repository<DocBrute>, IDocBruteRepository
    {
        private readonly IDematContext _amenContext;

        public DocBruteRepository(IDematContext amenContext)
            : base(amenContext)
        {
            _amenContext = amenContext;
        }

        
    }
}
