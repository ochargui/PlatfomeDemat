using DEMAT.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreateOperator
{
    public class CreateOperateurCommandHandler : IRequestHandler<CreateOperateurCommand, Guid>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;

        private readonly ILogger<CreateOperateurCommandHandler> _logger;

        public CreateOperateurCommandHandler(IAmenUnitOfWork amenUnitOfWork, ILogger<CreateOperateurCommandHandler> logger)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _logger = logger;
        }

        public async  Task<Guid> Handle(CreateOperateurCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ToEntity();

            await _amenUnitOfWork.OperateurRepository.AddAsync(entity, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("Operateur {operateur} was created", entity.id);

            return entity.id;
        }


    }
}
