using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetAllPackets
{
    public class GetAllPacketsQueryHandler : IRequestHandler<GetAllPacketsQuery, IEnumerable<PacketModel>>
    {
        private readonly IPacketReadRepository _packetReadRepository;

        public GetAllPacketsQueryHandler(IPacketReadRepository packetReadRepository)
        {
            _packetReadRepository = packetReadRepository;
        }

        public async Task<IEnumerable<PacketModel>> Handle(GetAllPacketsQuery request, CancellationToken cancellationToken)
        {
            return await _packetReadRepository.GetAllPackets(cancellationToken);
        }
    }
}
