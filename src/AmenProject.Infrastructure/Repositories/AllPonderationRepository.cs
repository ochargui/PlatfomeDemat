using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Interfaces.Repositories
{
    public class AllPonderationRepository : Repository<AllPonderation>, IAllPonderationRepository
    {
        private readonly IDematContext _amenContext;

        public AllPonderationRepository(IDematContext amenContext)
            : base(amenContext)
        {
            _amenContext = amenContext;
        }
    }
}
