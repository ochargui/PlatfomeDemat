using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ListDocEtatAgence
{
    public class ListDocEtatAgenceQueryHandler : IRequestHandler<ListDocEtatAgenceQuery, IEnumerable<DocumentsEnAttenteModel>>
    {
        private readonly IArchiveReadRepository _archiveReadRepository;
        private readonly IAgenceReadRepository _agenceReadRepository;
        private readonly IPacketReadRepository _packetReadRepository;
        private readonly IDocBruteReadRepository _docBruteReadRepository;


        public ListDocEtatAgenceQueryHandler(IDocBruteReadRepository docBruteReadRepository,
        IPacketReadRepository packetReadRepository ,IArchiveReadRepository archiveReadRepository, IAgenceReadRepository agenceReadRepository)
        {
            _docBruteReadRepository = docBruteReadRepository;
            _archiveReadRepository = archiveReadRepository;
            _packetReadRepository = packetReadRepository;
            _agenceReadRepository = agenceReadRepository;
        }

        public  async Task<IEnumerable<DocumentsEnAttenteModel>> Handle(ListDocEtatAgenceQuery request, CancellationToken cancellationToken)
        {
          IEnumerable<AgenceModel> agences = await _agenceReadRepository.GetAllAgences(cancellationToken);
            // return await _archiveReadRepository.ListArchiveByEtatAgence(2, agences, cancellationToken);

            return await _docBruteReadRepository.ListDocsByEtatAgence(0, agences, cancellationToken);

        }
    }
}
