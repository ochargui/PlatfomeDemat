using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreateControle
{
    public class CreateControleCommandHandler : IRequestHandler<CreateControleCommand, Guid>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;

        private readonly ILogger<CreateControleCommandHandler> _logger;

        public CreateControleCommandHandler(IAmenUnitOfWork amenUnitOfWork, ILogger<CreateControleCommandHandler> logger)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _logger = logger;
        }
    
        public async Task<Guid> Handle(CreateControleCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ToEntity();

            await _amenUnitOfWork.ControleRepository.AddAsync(entity, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("COntrole {Controle} was created", entity.Id);

            return entity.Id;
        }
    }
}
