using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreateAllPonderations
{
   public class CreateAllPonderationsCommandHandler : IRequestHandler<CreateAllPonderationsCommand,Guid>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;

        private readonly ILogger<CreateAllPonderationsCommandHandler> _logger;

        public CreateAllPonderationsCommandHandler(IAmenUnitOfWork amenUnitOfWork, ILogger<CreateAllPonderationsCommandHandler> logger)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _logger = logger;
        }
        public async Task<Guid> Handle(CreateAllPonderationsCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ToEntity();

            await _amenUnitOfWork.AllPonderationRepository.AddAsync(entity, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("AllPonderations {AllPonderations} was created", entity.Id);

            return entity.Id;
        }



    }
}
