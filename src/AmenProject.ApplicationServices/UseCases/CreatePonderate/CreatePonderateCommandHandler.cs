using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreatePonderate
{
   public class CreatePonderateCommandHandler : IRequestHandler<CreatePonderateCommand, Guid>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;

        private readonly ILogger<CreatePonderateCommandHandler> _logger;

        public CreatePonderateCommandHandler(IAmenUnitOfWork amenUnitOfWork, ILogger<CreatePonderateCommandHandler> logger)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreatePonderateCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ToEntity();

            await _amenUnitOfWork.PonderateRepository.AddAsync(entity, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("Ponderate {Ponderate} was created", entity.Id);

            return entity.Id;
        }



    }
}
