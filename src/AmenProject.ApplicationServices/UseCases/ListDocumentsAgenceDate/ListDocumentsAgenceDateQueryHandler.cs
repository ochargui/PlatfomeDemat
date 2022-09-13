using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ListDocumentsAgenceDate
{
    public class ListDocumentsAgenceDateQueryHandler : IRequestHandler<ListDocumentsAgenceDateQuery, IEnumerable<DocumentsModel>>
    {
        private readonly IArchiveReadRepository _archiveReadRepository;
        private readonly IAgenceReadRepository _agenceReadRepository;


        public ListDocumentsAgenceDateQueryHandler(IArchiveReadRepository archiveReadRepository, IAgenceReadRepository agenceReadRepository)
        {

            _archiveReadRepository = archiveReadRepository;
            _agenceReadRepository = agenceReadRepository;
        }


        public async  Task<IEnumerable<DocumentsModel>> Handle(ListDocumentsAgenceDateQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<AgenceModel> agences = await _agenceReadRepository.GetAllAgences(cancellationToken);
            return await _archiveReadRepository.ListDocumentsAgenceDate(request.StartDate , request.EndDate, agences, cancellationToken);
        }
    }
}
