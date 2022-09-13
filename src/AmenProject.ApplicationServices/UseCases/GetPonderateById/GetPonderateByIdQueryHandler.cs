using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetPonderateById
{
    public class GetPonderateByIdQueryHandler : IRequestHandler<GetPonderateByIdQuery, PonderateModel>
    {
        private readonly IPonderateReadRepository _ponderateReadRepository;

        public GetPonderateByIdQueryHandler(IPonderateReadRepository ponderateReadRepository)
        {
            _ponderateReadRepository = ponderateReadRepository;
        }

        public async Task<PonderateModel> Handle(GetPonderateByIdQuery request, CancellationToken cancellationToken)
        {
            return await _ponderateReadRepository.GetPonderateById(request.PonderateId, cancellationToken);
        }
    }
}
