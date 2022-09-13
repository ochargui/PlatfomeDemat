using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class DocBrutePacketRow
    {
        public Guid Idpacket { get; set; }
        public String NomPAcket { get; set; }
        public Guid IdDocBrute { get; set; }
        public String NomDoc { get; set; }
        public String Commentaire { get; set; }
        public int Etat { get; set; }
        public DateTimeOffset DateCreation { get; set; }
        public DateTimeOffset DateDebutTypage { get; set; }
        public DateTimeOffset DateFinTypage { get; set; }
    }

}
