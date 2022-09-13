using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetStatOperationJourne
{
    public class GetStatOperationJourneQueryHandler : IRequestHandler<GetStatOperationJourneQuery, IEnumerable<JourneOperationModel>>
    {
         private readonly IArchiveReadRepository _archiveReadRepository;
         private readonly IDocBruteReadRepository _docBruteReadRepository ;

        public GetStatOperationJourneQueryHandler(IDocBruteReadRepository docBruteReadRepository,IArchiveReadRepository archiveReadRepository)
        {
            _archiveReadRepository = archiveReadRepository;
            _docBruteReadRepository = docBruteReadRepository;
        }

        public async  Task<IEnumerable<JourneOperationModel>> Handle(GetStatOperationJourneQuery request, CancellationToken cancellationToken)
        {
            //return await _archiveReadRepository.ListArchiveJourne(cancellationToken);
            return await _docBruteReadRepository.ListDOcsJourne(cancellationToken);
        }
    }
}
