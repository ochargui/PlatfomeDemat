using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Infrastructure.Repositories
{
    public class OperateurRepository : Repository<Operateur>, IOperateurRepository
    {
        private readonly IDematContext _amenContext;

        public OperateurRepository(IDematContext amenContext)
            : base(amenContext)
        {
            _amenContext = amenContext;
        }
    }
}
