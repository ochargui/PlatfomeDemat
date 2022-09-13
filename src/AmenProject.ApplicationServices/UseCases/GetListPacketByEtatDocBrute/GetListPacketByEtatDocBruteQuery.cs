using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetListPacketByEtatDocBrute
{
    public class GetListPacketByEtatDocBruteQuery : IRequest<IEnumerable<PacketModel>>
    {
        public int Etat { get; set; }

        public GetListPacketByEtatDocBruteQuery(int etat)
        {
            Etat = etat;
        }
    }
}
