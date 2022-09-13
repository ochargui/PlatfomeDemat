using DEMAT.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreateZoneAgence
{
    public class CreateZoneAgenceCommandHandler : IRequestHandler<CreateZoneAgenceCommand, Guid>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;

        private readonly ILogger<CreateZoneAgenceCommandHandler> _logger;

        public CreateZoneAgenceCommandHandler(IAmenUnitOfWork amenUnitOfWork, ILogger<CreateZoneAgenceCommandHandler> logger)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _logger = logger;
        }

        public async  Task<Guid> Handle(CreateZoneAgenceCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ToEntity();

            await _amenUnitOfWork.ZoneAgenceRepository.AddAsync(entity, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("ZoneAgence {ZoneAgence} was created", entity.id);

            return entity.id;
        }
    }
}

