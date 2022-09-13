using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetOperatorByLogin
{
    public class GetOperatorByLoginQueryHandler : IRequestHandler<GetOperatorByLoginQuery, OperateurModel>
    {
        private readonly IOperateurReadRepository _operateurReadRepository;

        public GetOperatorByLoginQueryHandler(IOperateurReadRepository operateurReadRepository)
        {
            _operateurReadRepository = operateurReadRepository;
        }

        public  async Task<OperateurModel> Handle(GetOperatorByLoginQuery request, CancellationToken cancellationToken)
        {
            return await _operateurReadRepository.GetOperatorByLogin(request.OperatorLogin, cancellationToken);
        }
    }
}
