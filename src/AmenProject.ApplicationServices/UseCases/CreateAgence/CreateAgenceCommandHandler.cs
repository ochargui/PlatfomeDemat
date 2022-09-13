using DEMAT.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreateAgence
{
    public class CreateAgenceCommandHandler : IRequestHandler<CreateAgenceCommand, Guid>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;

        private readonly ILogger<CreateAgenceCommandHandler> _logger;

        public CreateAgenceCommandHandler(IAmenUnitOfWork amenUnitOfWork, ILogger<CreateAgenceCommandHandler> logger)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _logger = logger;
        }
    
        public async  Task<Guid> Handle(CreateAgenceCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ToEntity();
            await _amenUnitOfWork.AgenceRepository.AddAsync(entity, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("Agence {Agence} was created", entity.Id);

            return entity.Id;

        }

    }
}
