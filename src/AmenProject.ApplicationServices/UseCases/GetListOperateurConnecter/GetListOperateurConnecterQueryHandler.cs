using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetListOperateurConnecter
{
    public class GetListOperateurConnecterQueryHandler : IRequestHandler<GetListOperateurConnecterQuery, IEnumerable<OperateurModel>>
    {
        private readonly IOperateurReadRepository _operateurReadRepository;

        public GetListOperateurConnecterQueryHandler(IOperateurReadRepository operateurReadRepository)
        {
            _operateurReadRepository = operateurReadRepository;
        }

        public async Task<IEnumerable<OperateurModel>> Handle(GetListOperateurConnecterQuery request, CancellationToken cancellationToken)
        {
            return await _operateurReadRepository.GetOnlineOperators(cancellationToken);
        }
    }
}
