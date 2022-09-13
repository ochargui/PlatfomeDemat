using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetAgenceById
{
    public class GetAgenceByIdQueryHandler : IRequestHandler<GetAgenceByIdQuery, AgenceModel>
    {
        private readonly IAgenceReadRepository _agenceReadRepository;

        public GetAgenceByIdQueryHandler(IAgenceReadRepository agenceReadRepository)
        {
            _agenceReadRepository = agenceReadRepository;
        }

        public async Task<AgenceModel> Handle(GetAgenceByIdQuery request, CancellationToken cancellationToken)
        {
           return await _agenceReadRepository.GetAgenceById(request.AgenceId, cancellationToken);
        }
    }
}
