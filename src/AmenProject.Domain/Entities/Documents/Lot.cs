using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.Domain.Entities.Documents
{
    public class Lot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public RawDocument RawDocument { get; set; }
    }
}
