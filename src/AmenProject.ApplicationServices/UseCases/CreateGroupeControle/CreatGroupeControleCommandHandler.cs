using DEMAT.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreateGroupeControle
{
    public class CreatGroupeControleCommandHandler : IRequestHandler<CreatGroupeControleCommand, Guid>
    {

        private readonly IAmenUnitOfWork _amenUnitOfWork;

        private readonly ILogger<CreatGroupeControleCommandHandler> _logger;


        public CreatGroupeControleCommandHandler(IAmenUnitOfWork amenUnitOfWork, ILogger<CreatGroupeControleCommandHandler> logger)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _logger = logger;
        }


        public async Task<Guid> Handle(CreatGroupeControleCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ToEntity();

            //await _amenUnitOfWork.PacketRepository.AddAsync(entity, cancellationToken);
            //await _amenUnitOfWork.DocBruteRepository.AddAsync(entity, cancellationToken);
            await _amenUnitOfWork.GroupeControleRepository.AddAsync(entity, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("DOcBrute  was created", entity.Id);

            return entity.Id;
        }
    }
}
 