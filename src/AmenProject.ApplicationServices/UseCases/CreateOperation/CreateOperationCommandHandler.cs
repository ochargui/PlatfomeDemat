using DEMAT.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreateOperation
{
    public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand, Guid>
    {

        private readonly IAmenUnitOfWork _amenUnitOfWork;

        private readonly ILogger<CreateOperationCommandHandler> _logger;

        public CreateOperationCommandHandler(IAmenUnitOfWork amenUnitOfWork, ILogger<CreateOperationCommandHandler> logger)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _logger = logger;
        }



        public async Task<Guid> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ToEntity();
            await _amenUnitOfWork.OperationRepository.AddAsync(entity, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("Operaion {Operaion} was created", entity.Id);

            return entity.Id;

        }
    }
}
