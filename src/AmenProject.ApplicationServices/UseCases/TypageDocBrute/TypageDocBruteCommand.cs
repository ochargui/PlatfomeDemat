using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.TypageDocBrute
{
    public  class TypageDocBruteCommand : IRequest<string>
    {
        public Guid OperatorId { get; set; }
        public Guid OperationId { get; set; }
        public Guid DocBruteId { get; set; }


        public TypageDocBruteCommand(Guid operatorId, Guid operationId, Guid docBruteId)
        {
            OperatorId = operatorId;
            OperationId = operationId;
            DocBruteId = docBruteId;
        }
    }
}
