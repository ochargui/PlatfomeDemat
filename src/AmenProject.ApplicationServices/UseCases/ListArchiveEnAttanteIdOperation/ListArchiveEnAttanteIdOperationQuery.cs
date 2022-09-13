using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.ListArchiveEnAttanteOperation
{
    public class ListArchiveEnAttanteIdOperationQuery : IRequest<IEnumerable<ArchiveOperation>>
    {
       
    }
}
