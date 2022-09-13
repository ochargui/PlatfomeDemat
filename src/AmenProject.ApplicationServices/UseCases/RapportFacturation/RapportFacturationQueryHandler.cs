using DEMAT.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.RapportFacturation
{
    public class RapportFacturationQueryHandler : IRequestHandler<RapportFacturationQuery, IEnumerable<RapportFacturationModel>>
    {
        private readonly IReportingReadRepository _reportingReadRepository;

        public RapportFacturationQueryHandler(IReportingReadRepository reportingReadRepository)
        {
            _reportingReadRepository = reportingReadRepository;
        }
        public async Task<IEnumerable<RapportFacturationModel>> Handle(RapportFacturationQuery request, CancellationToken cancellationToken)
        {
            return await _reportingReadRepository.DataRapportFacturation(request.StartDate, request.EndDate, cancellationToken);
        }
    }
}
