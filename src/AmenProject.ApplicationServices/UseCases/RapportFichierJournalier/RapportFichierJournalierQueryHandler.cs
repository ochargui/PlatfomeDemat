using DEMAT.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.RapportFichierJournalier
{
    class RapportFichierJournalierQueryHandler : IRequestHandler<RapportFichierJournalierQuery, IEnumerable<RapportJournalierModel>>
    {
        private readonly IReportingReadRepository _reportingReadRepository;

        public RapportFichierJournalierQueryHandler(IReportingReadRepository reportingReadRepository)
        {
            _reportingReadRepository = reportingReadRepository;
        }

        public async Task<IEnumerable<RapportJournalierModel>> Handle(RapportFichierJournalierQuery request, CancellationToken cancellationToken)
        {
            return await _reportingReadRepository.DataFichierJournalier(request.StartDate, request.EndDate, cancellationToken);
        }
    }
}
