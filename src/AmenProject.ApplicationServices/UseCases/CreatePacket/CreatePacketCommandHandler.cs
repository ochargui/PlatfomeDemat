using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreatePacket
{
   public class CreatePacketCommandHandler : IRequestHandler<CreatePacketCommand,Guid>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;

        private readonly ILogger<CreatePacketCommandHandler> _logger;

        public CreatePacketCommandHandler(IAmenUnitOfWork amenUnitOfWork, ILogger<CreatePacketCommandHandler> logger)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _logger = logger;
        }
        public async Task<Guid> Handle(CreatePacketCommand request, CancellationToken cancellationToken)
        {
            var entity = request.ToEntity();

            await _amenUnitOfWork.PacketRepository.AddAsync(entity, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            _logger.LogInformation("LotPacket {LotPacket} was created", entity.Id);

            return entity.Id;
        }



    }
}
