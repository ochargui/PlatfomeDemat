using DEMAT.Domain.Entities.Documents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.RawDocuments.Requests
{
    public class GetAllRawDocumentsQuery : IRequest<IEnumerable<RawDocument>>
    {
    }
}
