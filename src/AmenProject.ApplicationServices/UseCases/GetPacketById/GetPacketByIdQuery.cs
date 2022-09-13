using MediatR;
using DEMAT.Models;
using System;

namespace DEMAT.ApplicationServices.UseCases.GetPacketById
{
    public class GetPacketByIdQuery : IRequest<PacketModel>
    {
        public Guid PacketId { get; set; }

        public GetPacketByIdQuery(Guid packetId)
        {
            PacketId = packetId;
        }
    }
}
