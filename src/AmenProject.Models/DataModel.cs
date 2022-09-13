using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class DataModel
    {
        public Guid? IdData { get; set; }
        public String ControleValue { get; set; }
        public Guid? ArchiveId { get; set; }
        public Guid? ControleId { get; set; }
    }
}
