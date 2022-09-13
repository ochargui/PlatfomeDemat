using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetDocBruteByEtatByLotPacketId
{
    public class GetDocBruteByEtatByLotPacketIdQuery : IRequest<IEnumerable<DocBruteModel>>
    {
        public int Etat { get; set; }
        public Guid LotPacketID { get; set; }

        public GetDocBruteByEtatByLotPacketIdQuery(int etat, Guid lotPacketID)
        {
            Etat = etat;
            LotPacketID = lotPacketID;
        }
    }
}
