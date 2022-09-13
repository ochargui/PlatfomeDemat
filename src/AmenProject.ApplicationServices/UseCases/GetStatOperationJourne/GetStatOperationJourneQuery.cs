using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetStatOperationJourne
{
    public class GetStatOperationJourneQuery : IRequest<IEnumerable<JourneOperationModel>>
    {
    }
}
