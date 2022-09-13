using DEMAT.ApplicationServices.UseCases.GetControlById;
using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetControleById
{
    public class GetControleByIdQueryHandler : IRequestHandler<GetControleByIdQuery, ControleModel>
    {
        private readonly IControleReadRepository _controleReadRepository;

        public GetControleByIdQueryHandler(IControleReadRepository controleReadRepository)
        {
            _controleReadRepository = controleReadRepository;
        }
    
        public async  Task<ControleModel> Handle(GetControleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _controleReadRepository.GetControleById(request.ControleId, cancellationToken);
        }
    }
}
