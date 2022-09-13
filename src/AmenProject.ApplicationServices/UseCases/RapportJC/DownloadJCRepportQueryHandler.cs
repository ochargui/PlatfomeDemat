using DEMAT.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.DownloadJCRepport
{
    public class JCRepportQueryHandler : IRequestHandler<JCRepportQuery, IEnumerable<RapportJCModel>>
    { 
        private readonly IReportingReadRepository _reportingReadRepository;

        public JCRepportQueryHandler(IReportingReadRepository reportingReadRepository)
        {
            _reportingReadRepository = reportingReadRepository;
        }

        public async  Task<IEnumerable<RapportJCModel>> Handle(JCRepportQuery request, CancellationToken cancellationToken)
        {
           return await _reportingReadRepository.SelectJourneeByDateComptable(request.StartDate , request.EndDate, cancellationToken);
          //return await _reportingReadRepository.JCRepport(request.StartDate , request.EndDate, cancellationToken);
        }
    }
}
