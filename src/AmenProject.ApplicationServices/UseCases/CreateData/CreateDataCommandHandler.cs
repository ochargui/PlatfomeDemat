using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreateData
{
   public class CreateDataCommandHandler : IRequestHandler<CreateDataCommand, Guid>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;

        private readonly ILogger<CreateDataCommandHandler> _logger;

        public CreateDataCommandHandler(IAmenUnitOfWork amenUnitOfWork, ILogger<CreateDataCommandHandler> logger)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateDataCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ToEntity();

            await _amenUnitOfWork.DataRepository.AddAsync(entity, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("Data {Data} was created", entity.Id);

            return entity.Id;
        }



    }
}
