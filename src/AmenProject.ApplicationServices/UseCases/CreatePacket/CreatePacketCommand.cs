using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.CreatePacket
{
    public class CreatePacketCommand : IRequest<Guid>
    {
        public string Nom { get; set; }
        public int Etat { get; set; }
        public int NbDoc { get; set; }

        internal LotPacket ToEntity()
        {
            return new LotPacket
            {
                NomPAcket = Nom,
                EtatLotPacket = Etat,
                NombreDoc = NbDoc
            };
        }


    }
}
