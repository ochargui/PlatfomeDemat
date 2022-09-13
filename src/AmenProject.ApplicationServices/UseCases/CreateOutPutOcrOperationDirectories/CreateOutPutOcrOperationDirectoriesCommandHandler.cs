using DEMAT.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.CreateOutPutOcrOperationDirectories
{
    public class CreateOutPutOcrOperationDirectoriesCommandHandler : IRequestHandler<CreateOutPutOcrOperationDirectoriesCommand, string>
    {
        private readonly ILotArchiveReadRepository _lotArchiveRepository;

        public CreateOutPutOcrOperationDirectoriesCommandHandler(ILotArchiveReadRepository lotArchiveRepository)
        {
            _lotArchiveRepository = lotArchiveRepository;
        }

        public async Task<string> Handle(CreateOutPutOcrOperationDirectoriesCommand request, CancellationToken cancellationToken)
        {
            return await _lotArchiveRepository.CreateOutOutOcrOperationDiercotries(request.journee,request.AgenceCode,cancellationToken);
        }
    }
}
