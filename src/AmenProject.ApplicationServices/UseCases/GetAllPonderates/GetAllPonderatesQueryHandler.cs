using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetAllPonderates
{
    public class GetAllPonderatesQueryHandler : IRequestHandler<GetAllPonderatesQuery, IEnumerable<PonderateModel>>
    {
        private readonly IPonderateReadRepository _ponderateReadRepository;

        public GetAllPonderatesQueryHandler(IPonderateReadRepository ponderateReadRepository)
        {
            _ponderateReadRepository = ponderateReadRepository;
        }

        public async Task<IEnumerable<PonderateModel>> Handle(GetAllPonderatesQuery request, CancellationToken cancellationToken)
        {
            return await _ponderateReadRepository.GetAllPonderates(cancellationToken);
        }
    }
}
