using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetOperatorById
{
    public class GetOperatorByIdQuery : IRequest<OperateurModel>
    {
        public Guid OperatorId { get; set; }

        public GetOperatorByIdQuery(Guid operatorId)
        {
            OperatorId = operatorId;
        }
    }
}
