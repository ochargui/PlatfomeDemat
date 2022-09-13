using DEMAT.Domain.Entities;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetDocBruteByIdLotPacket
{
    public class GetDocBruteByIdLotPacketQuery : IRequest<IEnumerable<DocBrutePacketRow>>
    {

        public PacketModel Packet { get; set; }

        public GetDocBruteByIdLotPacketQuery(PacketModel packet)
        {
            Packet = packet;
        }
    }
}
