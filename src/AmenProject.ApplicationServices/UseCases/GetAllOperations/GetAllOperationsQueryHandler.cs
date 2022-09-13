using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetAllOperations
{
    public  class GetAllOperationsQueryHandler : IRequestHandler<GetAllOperationsQuery, IEnumerable<OperationModel>>
    {

        private readonly IOperationReadRepository _operationReadRepository;

        public GetAllOperationsQueryHandler(IOperationReadRepository operationReadRepository)
        {
            _operationReadRepository = operationReadRepository;
        }

        public async Task<IEnumerable<OperationModel>> Handle(GetAllOperationsQuery request, CancellationToken cancellationToken)
        {
            return await _operationReadRepository.GetAllOperations(cancellationToken);

        }
    }
}
