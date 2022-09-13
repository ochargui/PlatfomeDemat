using DEMAT.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreateArchiveUpdateEtatDocBrute
{
    public class CreateArchiveUpdateEtatDocBruteCommandHandler : IRequestHandler<CreateArchiveUpdateEtatDocBruteCommand, string>
    {
        private readonly IArchiveReadRepository _archiveReadRepository;

        public CreateArchiveUpdateEtatDocBruteCommandHandler(IArchiveReadRepository archiveReadRepository)
        {
            _archiveReadRepository = archiveReadRepository;
        }
        public async Task<string> Handle(CreateArchiveUpdateEtatDocBruteCommand request, CancellationToken cancellationToken)
        {
            return await _archiveReadRepository.InsertArchiveUpdateEtatPacket(request.PacketId, cancellationToken);
        }
    }
}
