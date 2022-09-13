using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class RapportJournalierModel
    {
        public string AgenceName { get; set; }
        public int AgenceCode { get; set; }
        public string LotArchiveName { get; set; }
        public string OperationName { get; set; }
        public string ArchiveName { get; set; }
        public DateTimeOffset DateFinSaisie { get; set; }
        public string Path { get; set; }
    }
}
