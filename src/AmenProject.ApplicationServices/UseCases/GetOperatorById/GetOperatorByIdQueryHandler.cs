using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetOperatorById
{
    public class GetOperatorByIdQueryHandler : IRequestHandler<GetOperatorByIdQuery,OperateurModel>
    {
        private readonly IOperateurReadRepository _operateurReadRepository;

        public GetOperatorByIdQueryHandler(IOperateurReadRepository operateurReadRepository)
        {
            _operateurReadRepository = operateurReadRepository;
        }

        public async  Task<OperateurModel> Handle(GetOperatorByIdQuery request, CancellationToken cancellationToken)
        {
            return await _operateurReadRepository.GetOperatorById(request.OperatorId, cancellationToken);
        }
    }
}
