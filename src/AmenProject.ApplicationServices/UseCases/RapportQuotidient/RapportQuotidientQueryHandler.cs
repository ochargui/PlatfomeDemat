using DEMAT.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.RapportQuotidient
{
    public class RapportQuotidientQueryHandler : IRequestHandler<RapportQuotidientQuery, IEnumerable<RapportQuotidienModel>>
    {
        private readonly IReportingReadRepository _reportingReadRepository;

        public RapportQuotidientQueryHandler(IReportingReadRepository reportingReadRepository)
        {
            _reportingReadRepository = reportingReadRepository;
        }

        public  async Task<IEnumerable<RapportQuotidienModel>> Handle(RapportQuotidientQuery request, CancellationToken cancellationToken)
        {
            return await _reportingReadRepository.DataRapportQuotidien(request.DateTraitement, request.Journee, cancellationToken);
        }
    }
}
