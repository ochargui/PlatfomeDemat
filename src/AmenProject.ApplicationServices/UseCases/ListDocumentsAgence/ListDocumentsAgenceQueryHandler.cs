using DEMAT.Domain.Interfaces;
using DEMAT.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ListDocumentsAgence
{
    public class ListDocumentsAgenceQueryHandler : IRequestHandler<ListDocumentsAgenceQuery, IEnumerable<DocumentsModel>>
    {
      
        
        private readonly IArchiveReadRepository _archiveReadRepository;
        private readonly IAgenceReadRepository _agenceReadRepository;


        public ListDocumentsAgenceQueryHandler(IArchiveReadRepository archiveReadRepository, IAgenceReadRepository agenceReadRepository)
        {
          
            _archiveReadRepository = archiveReadRepository;
            _agenceReadRepository = agenceReadRepository;
        }

        public  async Task<IEnumerable<DocumentsModel>> Handle(ListDocumentsAgenceQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<AgenceModel> agences = await _agenceReadRepository.GetAllAgences(cancellationToken);

            return await _archiveReadRepository.ListDocumentsAgence(agences, cancellationToken);
        }
    }
}
