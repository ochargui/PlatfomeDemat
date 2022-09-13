using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public  class OperationModel
    {
        public Guid Id { get; set; }
        public string OperationName { get; set; }
        public int CodeOperation { get; set; }
        public int Ponderation { get; set; }
    }
}
