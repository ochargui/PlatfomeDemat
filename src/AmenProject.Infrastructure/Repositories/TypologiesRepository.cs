using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Infrastructure.Repositories
{
    public class TypologiesRepository : Repository<Typologies>, ITypologiesRepository
    {
        private readonly IDematContext _amenContext;

        public TypologiesRepository(IDematContext amenContext)
            : base(amenContext)
        {
            _amenContext = amenContext;
        }
    }
}
