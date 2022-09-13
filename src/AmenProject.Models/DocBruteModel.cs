using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class DocBruteModel
    {
        public Guid Id { get; set; }
        public String NomDoc { get; set; }
        public String Commentaire { get; set; }
        public int Etat { get; set; }
        public string PacketName { get; set; }
        public Guid LotPacketId { get; set; }
        public Guid AgenceId { get; set; }




    }
}
