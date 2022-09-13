using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetAllDocBrutes
{
    public class GetAllDocBrutesQueryHandler : IRequestHandler<GetAllDocBrutesQuery, IEnumerable<DocBruteModel>>
    {
        private readonly IDocBruteReadRepository _docBruteReadRepository;

        public GetAllDocBrutesQueryHandler(IDocBruteReadRepository docBruteReadRepository)
        {
            _docBruteReadRepository = docBruteReadRepository;
        }
        public async Task<IEnumerable<DocBruteModel>> Handle(GetAllDocBrutesQuery request, CancellationToken cancellationToken)
        {
          //  return await _packetReadRepository.GetAllPackets(cancellationToken);
          return await _docBruteReadRepository.GetAllDocBrutes(cancellationToken);

        }
    }
}
