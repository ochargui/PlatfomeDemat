using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.Models
{
    public class PacketModel
    {
        public Guid Id { get; set; }
        public String NomPAcket { get; set; }
        public int EtatLotPacket { get; set; }
        public int NombreDoc { get; set; }

        public PacketModel(Guid id, string nomPAcket, int etatLotPacket, int nombreDoc)
        {
            Id = id;
            NomPAcket = nomPAcket;
            EtatLotPacket = etatLotPacket;
            NombreDoc = nombreDoc;
        }

        public PacketModel()
        {
        }
    }
}
