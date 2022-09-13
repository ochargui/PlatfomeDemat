using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetAllArchives
{
   public  class GetAllArchivesQueryHandler : IRequestHandler<GetAllArchivesQuery, IEnumerable<ArchiveModel>>
    {
        private readonly IArchiveReadRepository _archiveReadRepository;

        public GetAllArchivesQueryHandler(IArchiveReadRepository archiveReadRepository)
        {
            _archiveReadRepository = archiveReadRepository;
        }

        public async Task<IEnumerable<ArchiveModel>> Handle(GetAllArchivesQuery request, CancellationToken cancellationToken)
        {
            return await _archiveReadRepository.GetAllArchives(cancellationToken);
        }
    }
}
