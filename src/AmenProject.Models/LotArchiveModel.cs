using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class LotArchiveModel
    {

        public Guid Id { get; set; }
        public int CodeLotArchive { get; set; }
        public string LotArchiveName { get; set; }
        public DateTimeOffset DateDebutSaisie { get; set; }
        public DateTimeOffset DateFinSaisie { get; set; }

    }
}
