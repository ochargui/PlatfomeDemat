using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetAllTypologies
{
    class GetAllTypologiesQueryHandler : IRequestHandler<GetAllTypologiesQuery, IEnumerable<TypologieModel>>
    {
        private readonly ITypologiesReadRepository _typologiesReadRepository;

        public GetAllTypologiesQueryHandler(ITypologiesReadRepository typologiesReadRepository)
        {
            _typologiesReadRepository = typologiesReadRepository;
        }



        public async  Task<IEnumerable<TypologieModel>> Handle(GetAllTypologiesQuery request, CancellationToken cancellationToken)
        {
           return await _typologiesReadRepository.GetAllTypologies(cancellationToken);
        }
    }
}
