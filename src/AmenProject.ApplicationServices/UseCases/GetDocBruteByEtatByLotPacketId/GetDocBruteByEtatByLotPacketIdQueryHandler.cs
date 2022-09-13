using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetDocBruteByEtatByLotPacketId
{
    public class GetDocBruteByEtatByLotPacketIdQueryHandler : IRequestHandler<GetDocBruteByEtatByLotPacketIdQuery, IEnumerable<DocBruteModel>>
    {
        private readonly IDocBruteReadRepository _docBruteReadRepository;

        public GetDocBruteByEtatByLotPacketIdQueryHandler(IDocBruteReadRepository docBruteReadRepository)
        {
            _docBruteReadRepository = docBruteReadRepository;
        }

        public async  Task<IEnumerable<DocBruteModel>> Handle(GetDocBruteByEtatByLotPacketIdQuery request, CancellationToken cancellationToken)
        {
            return await _docBruteReadRepository.GetDocBruteByEtatByLotPacketId( request.Etat,request.LotPacketID,  cancellationToken);
        }
    }
}
