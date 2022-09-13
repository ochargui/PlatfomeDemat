using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetOperationById
{
    public class GetOperationByIdQuery : IRequest<OperationModel>
    {
        public Guid OperationId { get; set; }

        public GetOperationByIdQuery(Guid operationId)
        {
            OperationId = operationId;
        }
    }
}
