using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetArchiveById
{
    public class GetArchiveByIdQueryHandler : IRequestHandler<GetArchiveByIdQuery, ArchiveModel>
    {
        private readonly IArchiveReadRepository _archiveReadRepository;

        public GetArchiveByIdQueryHandler(IArchiveReadRepository archiveReadRepository)
        {
            _archiveReadRepository = archiveReadRepository;
        }

        public async  Task<ArchiveModel> Handle(GetArchiveByIdQuery request, CancellationToken cancellationToken)
        {
          return await _archiveReadRepository.GetArchiveById(request.ArchiveId, cancellationToken);
        }
    }
}
