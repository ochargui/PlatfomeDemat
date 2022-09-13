using DEMAT.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreateLotArchive
{
    public class CreateLotArchiveCommandHandler : IRequestHandler<CreateLotArchiveCommand, Guid>
    {

        private readonly IAmenUnitOfWork _amenUnitOfWork;

        private readonly ILogger<CreateLotArchiveCommandHandler> _logger;

        public CreateLotArchiveCommandHandler(IAmenUnitOfWork amenUnitOfWork, ILogger<CreateLotArchiveCommandHandler> logger)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateLotArchiveCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ToEntity();
            await _amenUnitOfWork.LotArchiveRepository.AddAsync(entity, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("LotPackte  was created", entity.Id);

            return entity.Id;
        }
    }
}
