using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetPacketById
{
    public class GetPacketByIdQueryHandler : IRequestHandler<GetPacketByIdQuery, PacketModel>
    {
        private readonly IPacketReadRepository _packetReadRepository;

        public GetPacketByIdQueryHandler(IPacketReadRepository packetReadRepository)
        {
            _packetReadRepository = packetReadRepository;
        }

        public async Task<PacketModel> Handle(GetPacketByIdQuery request, CancellationToken cancellationToken)
        {
            return await _packetReadRepository.GetPacketById(request.PacketId, cancellationToken);
        }
    }
}
