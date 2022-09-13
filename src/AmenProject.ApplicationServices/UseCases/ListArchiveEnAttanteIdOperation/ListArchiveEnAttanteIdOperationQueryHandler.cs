using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ListArchiveEnAttanteOperation
{
    public class ListArchiveEnAttanteIdOperationQueryHandler : IRequestHandler< ListArchiveEnAttanteIdOperationQuery, IEnumerable<ArchiveOperation>>
    {
        private readonly IArchiveReadRepository _archiveReadRepository;

        public ListArchiveEnAttanteIdOperationQueryHandler(IArchiveReadRepository archiveReadRepository)
        {
            _archiveReadRepository = archiveReadRepository;
        }

        public async Task<IEnumerable<ArchiveOperation>> Handle(ListArchiveEnAttanteIdOperationQuery request, CancellationToken cancellationToken)
        {
            
            return await _archiveReadRepository.GetListArchiveOperation(2,cancellationToken);      
        }
    }
}
