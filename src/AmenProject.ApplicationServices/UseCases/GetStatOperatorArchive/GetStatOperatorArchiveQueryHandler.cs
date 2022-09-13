using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetStatOperatorArchive
{
    public class GetStatOperatorArchiveQueryHandler : IRequestHandler<GetStatOperatorArchiveQuery, IEnumerable<OperateurArchiveSatModel>>
    {
        private readonly IOperateurReadRepository _operateurReadRepository;

        public GetStatOperatorArchiveQueryHandler(IOperateurReadRepository operateurReadRepository)
        {
            _operateurReadRepository = operateurReadRepository;
        }

        public async Task<IEnumerable<OperateurArchiveSatModel>> Handle(GetStatOperatorArchiveQuery request, CancellationToken cancellationToken)
        {
            return await _operateurReadRepository.GetListArchiveOperateur(request.StartDate, request.EndDate, request.Equipe,  cancellationToken);
        }
    }
}
