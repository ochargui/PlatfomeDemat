using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.Infrastructure.Repositories
{
    public class ControlRepository : Repository<Control>, IControlRepository
    {
        private readonly IDematContext _amenContext;

        public ControlRepository(IDematContext amenContext)
            : base(amenContext)
        {
            _amenContext = amenContext;
        }
    }
}
