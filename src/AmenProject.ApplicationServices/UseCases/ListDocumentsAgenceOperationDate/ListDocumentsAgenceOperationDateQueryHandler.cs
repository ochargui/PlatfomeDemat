using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ListDocumentsAgenceOperationDate
{
    public class ListDocumentsAgenceOperationDateQueryHandler : IRequestHandler<ListDocumentsAgenceOperationDateQuery, IEnumerable<DocumentsModel>>
    {
        private readonly IArchiveReadRepository _archiveReadRepository;
        private readonly IAgenceReadRepository _agenceReadRepository;


        public ListDocumentsAgenceOperationDateQueryHandler(IArchiveReadRepository archiveReadRepository, IAgenceReadRepository agenceReadRepository)
        {
            _archiveReadRepository = archiveReadRepository;
            _agenceReadRepository = agenceReadRepository;
        }

        public async Task<IEnumerable<DocumentsModel>> Handle(ListDocumentsAgenceOperationDateQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<AgenceModel> agences = await _agenceReadRepository.GetAllAgences(cancellationToken);
            return  await _archiveReadRepository.ListDocumentsAgenceDateOperation(request.DateDebut,request.DateFin,request.OperationId, agences, cancellationToken);
        }
    }
}
