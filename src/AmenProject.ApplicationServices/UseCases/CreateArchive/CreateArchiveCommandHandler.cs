using DEMAT.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreateArchive
{
    public class CreateArchiveCommandHandler : IRequestHandler<CreateArchiveCommand, Guid>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;

        private readonly ILogger<CreateArchiveCommandHandler> _logger;

        public CreateArchiveCommandHandler(IAmenUnitOfWork amenUnitOfWork, ILogger<CreateArchiveCommandHandler> logger)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateArchiveCommand request, CancellationToken cancellationToken)
        {

            var entity = request.ToEntity();

            await _amenUnitOfWork.ArchiveRepository.AddAsync(entity, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("LotPacket {LotPacket} was created", entity.Id);

            return entity.Id;
        }
    }
}
