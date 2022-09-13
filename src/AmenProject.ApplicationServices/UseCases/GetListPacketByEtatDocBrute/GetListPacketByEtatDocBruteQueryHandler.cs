using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetListPacketByEtatDocBrute
{
    class GetListPacketByEtatDocBruteQueryHandler : IRequestHandler<GetListPacketByEtatDocBruteQuery, IEnumerable<PacketModel>>
    {
        private readonly IPacketReadRepository _packetReadRepository;

        public GetListPacketByEtatDocBruteQueryHandler(IPacketReadRepository packetReadRepository)
        {
            _packetReadRepository = packetReadRepository;
        }

        public async Task<IEnumerable<PacketModel>> Handle(GetListPacketByEtatDocBruteQuery request, CancellationToken cancellationToken)
        {
            return await _packetReadRepository.GetListPacketByEtatDocBrute(request.Etat, cancellationToken);
        }
    }
}
