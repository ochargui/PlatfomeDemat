using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Infrastructure.Repositories
{
    public class OperationRepository : Repository<Operation>, IOperationRepository
    {
        private readonly IDematContext _amenContext;

        public OperationRepository(IDematContext amenContext)
            : base(amenContext)
        {
            _amenContext = amenContext;
        }
    }
}
