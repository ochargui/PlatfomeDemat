using DEMAT.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreateInputOputDirectory
{
    public class CreatenputOutputDirectoryCommandHandler : IRequestHandler<CreatenputOutputDirectoryCommand, string>
    {
        
        private readonly IPacketReadRepository _packetReadRepository;

        public CreatenputOutputDirectoryCommandHandler(IPacketReadRepository packetReadRepository)
        {
            _packetReadRepository = packetReadRepository;
        }

        public async  Task<string> Handle(CreatenputOutputDirectoryCommand request, CancellationToken cancellationToken)
        {
            return await _packetReadRepository.CreateDirectories(cancellationToken);






        }
    }
}
