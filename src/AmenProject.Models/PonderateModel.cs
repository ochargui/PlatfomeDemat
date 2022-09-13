using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class PonderateModel
    {
        public Guid IdPonderate { get; set; }
        public String Nom { get; set; }
        public String Valeur { get; set; }

        public Guid? OperationId { get; set; }
    }
}
