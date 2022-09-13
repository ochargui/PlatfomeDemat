using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetTypologieById
{
    public class GetTypologieByIdQueryHandler : IRequestHandler<GetTypologieByIdQuery, TypologieModel>
    {
        private readonly ITypologiesReadRepository _typologiesReadRepository;

        public GetTypologieByIdQueryHandler(ITypologiesReadRepository typologiesReadRepository)
        {
            _typologiesReadRepository = typologiesReadRepository;
        }

        public async Task<TypologieModel> Handle(GetTypologieByIdQuery request, CancellationToken cancellationToken)
        {
            return await _typologiesReadRepository.GetPacketById(request.TypologieId, cancellationToken);
        }
    }
}
