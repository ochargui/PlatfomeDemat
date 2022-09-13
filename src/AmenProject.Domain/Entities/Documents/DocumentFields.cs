using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.Domain.Entities.Documents
{
    public class DocumentFields
    {
        public Guid Id { get; set; }
        public string FieldNumber { get; set; }
        public string ClientSignature { get; set; }
        public string BankStamp { get; set; }

    }
}
