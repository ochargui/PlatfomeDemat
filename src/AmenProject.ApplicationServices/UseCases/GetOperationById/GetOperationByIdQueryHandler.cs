using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetOperationById
{
    public class GetOperationByIdQueryHandler : IRequestHandler<GetOperationByIdQuery, OperationModel>
    {
        private readonly IOperationReadRepository _operationReadRepository;

        public GetOperationByIdQueryHandler(IOperationReadRepository operationReadRepository)
        {
            _operationReadRepository = operationReadRepository;
        }

        public async Task<OperationModel> Handle(GetOperationByIdQuery request, CancellationToken cancellationToken)
        {
           return await _operationReadRepository.GetOperationById(request.OperationId, cancellationToken);
            //   return await _packetReadRepository.GetPacketById(request.PacketId, cancellationToken);
        }
    }
}
