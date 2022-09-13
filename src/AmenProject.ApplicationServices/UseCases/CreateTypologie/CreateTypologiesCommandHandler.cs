using DEMAT.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreateTypologie
{
    class CreateTypologiesCommandHandler : IRequestHandler<CreateTypologiesCommand, Guid>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;

        private readonly ILogger<CreateTypologiesCommandHandler> _logger;

        public CreateTypologiesCommandHandler(IAmenUnitOfWork amenUnitOfWork, ILogger<CreateTypologiesCommandHandler> logger)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _logger = logger;
        }


        public async Task<Guid> Handle(CreateTypologiesCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ToEntity();

            await _amenUnitOfWork.TypologiesRepository.AddAsync(entity, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("Typologies {Typologie} was created", entity.Id);

            return entity.Id;
        }
    }
}
