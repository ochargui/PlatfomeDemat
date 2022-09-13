using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetAllPonderations
{
    public class GetAllPonderationsQueryHandler : IRequestHandler<GetAllPonderationsQuery, IEnumerable<AllPonderationModel>>
    {
        private readonly IAllPonderationReadRepository _allPonderationReadRepository;

        public GetAllPonderationsQueryHandler(IAllPonderationReadRepository allPonderationReadRepository)
        {
            _allPonderationReadRepository = allPonderationReadRepository;
        }

        public async Task<IEnumerable<AllPonderationModel>> Handle(GetAllPonderationsQuery request, CancellationToken cancellationToken)
        {
            return await _allPonderationReadRepository.GetAllAllPonderations(cancellationToken);
        }
    }
}
