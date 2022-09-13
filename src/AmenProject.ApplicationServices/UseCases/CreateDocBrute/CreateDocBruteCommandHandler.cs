using DEMAT.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreateDocBrute
{
    public class CreateDocBruteCommandHandler : IRequestHandler<CreateDocBruteCommand, Guid>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;

        private readonly ILogger<CreateDocBruteCommandHandler> _logger;


        public CreateDocBruteCommandHandler(IAmenUnitOfWork amenUnitOfWork, ILogger<CreateDocBruteCommandHandler> logger)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _logger = logger;
        }

        

        public async Task<Guid> Handle(CreateDocBruteCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ToEntity();

            //await _amenUnitOfWork.PacketRepository.AddAsync(entity, cancellationToken);
            await _amenUnitOfWork.DocBruteRepository.AddAsync(entity, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("DOcBrute  was created", entity.Id);

            return entity.Id;
        }
    }
}
