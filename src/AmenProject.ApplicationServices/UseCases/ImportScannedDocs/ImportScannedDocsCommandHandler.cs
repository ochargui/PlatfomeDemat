using DEMAT.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ImportScannedDocs
{
    public class ImportScannedDocsCommandHandler : IRequestHandler<ImportScannedDocsCommand, string>
    {
        private readonly IPacketReadRepository _packetReadRepository;
        private readonly IOperateurReadRepository _operateurReadRepository;
        private IConfiguration Configuration;


        public ImportScannedDocsCommandHandler(IOperateurReadRepository operateurReadRepository  ,IPacketReadRepository packetReadRepository, IConfiguration configuration)
        {
            _operateurReadRepository = operateurReadRepository;
               _packetReadRepository = packetReadRepository;
            Configuration = configuration;
        }
        public async Task<string> Handle(ImportScannedDocsCommand request, CancellationToken cancellationToken)
        {
           var operateur = await _operateurReadRepository.GetOperatorById(request.IdOperateur, cancellationToken);


            return await _packetReadRepository.CopieInputFilesToOutputLocal(operateur, cancellationToken);
            

        }
    }
}
