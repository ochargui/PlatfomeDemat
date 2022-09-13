using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetDocBruteByIdLotPacket
{
    public class GetDocBruteByIdLotPacketQueryHandler : IRequestHandler<GetDocBruteByIdLotPacketQuery, IEnumerable<DocBrutePacketRow>>
    {

        private readonly IDocBruteReadRepository _docBruteReadRepository;

        public GetDocBruteByIdLotPacketQueryHandler(IDocBruteReadRepository docBruteReadRepository)
        {
            _docBruteReadRepository = docBruteReadRepository;
        }
        public async  Task<IEnumerable<DocBrutePacketRow>> Handle(GetDocBruteByIdLotPacketQuery request, CancellationToken cancellationToken)
        {
            return await _docBruteReadRepository.GetDocBruteByPacket(request.Packet, cancellationToken);
        }
    }
}
