using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetAllOperations
{
    public class GetAllOperationsQuery : IRequest<IEnumerable<OperationModel>>
    {

    }
}
