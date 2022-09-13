using DEMAT.Domain.Entities.Documents;
using DEMAT.Domain.Interfaces;
using DEMAT.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.Infrastructure.Repositories
{
    public class RawDocumentRepository : Repository<RawDocument>, IRawDocumentRepository
    {
        private readonly IDematContext _amenContext;

        public RawDocumentRepository(IDematContext amenContext) : base(amenContext)
        {
            _amenContext = amenContext;
        }
    }
}
