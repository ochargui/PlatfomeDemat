using DEMAT.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.TypageDocBrute
{
    public class TypageDocBruteCommandHandler : IRequestHandler<TypageDocBruteCommand, string>
    {
        private readonly IArchiveReadRepository _archiveReadRepository;
        private readonly IAmenUnitOfWork _amenUnitOfWork;

        public TypageDocBruteCommandHandler(IAmenUnitOfWork amenUnitOfWork, IArchiveReadRepository archiveReadRepository)
        {
            _archiveReadRepository = archiveReadRepository;
            _amenUnitOfWork = amenUnitOfWork;
        }

        public async Task<string> Handle(TypageDocBruteCommand request, CancellationToken cancellationToken)
        {
            var res = await _archiveReadRepository.CreateArchive(request.DocBruteId, request.OperatorId, request.OperationId, cancellationToken);

            await _amenUnitOfWork.SaveAsync(cancellationToken);
            return res ;
        }
    }
}
