using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetAllControles
{
    public class GetALLControlesQueryHandler : IRequestHandler<GetALLControlesQuery, IEnumerable<ControleModel>>
    {
        private readonly IControleReadRepository _controleReadRepository;

        public GetALLControlesQueryHandler(IControleReadRepository controleReadRepository)
        {
            _controleReadRepository = controleReadRepository;
        }
     
        public async Task<IEnumerable<ControleModel>> Handle(GetALLControlesQuery request, CancellationToken cancellationToken)
        {
            return await _controleReadRepository.GetAllControles(cancellationToken);
        }
    }
}
