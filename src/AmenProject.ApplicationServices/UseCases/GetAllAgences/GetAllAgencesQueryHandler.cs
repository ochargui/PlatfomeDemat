using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetAllAgences
{
    public  class GetAllAgencesQueryHandler : IRequestHandler<GetAllAgencesQuery, IEnumerable<AgenceModel>>
    {
        private readonly IAgenceReadRepository _agenceReadRepository;

        public GetAllAgencesQueryHandler(IAgenceReadRepository agenceReadRepository)
        {
            _agenceReadRepository = agenceReadRepository;
        }

        public async Task<IEnumerable<AgenceModel>> Handle(GetAllAgencesQuery request, CancellationToken cancellationToken)
        {
           return  await _agenceReadRepository.GetAllAgences(cancellationToken);
        }
    }
}
