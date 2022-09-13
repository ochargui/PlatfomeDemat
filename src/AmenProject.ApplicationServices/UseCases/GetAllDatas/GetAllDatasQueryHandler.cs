using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetAllDatas
{
    public class GetAllDatasQueryHandler : IRequestHandler<GetAllDatasQuery, IEnumerable<DataModel>>
    {
        private readonly IDataReadRepository _dataReadRepository;

        public GetAllDatasQueryHandler(IDataReadRepository dataReadRepository)
        {
            _dataReadRepository = dataReadRepository;
        }

        public async Task<IEnumerable<DataModel>> Handle(GetAllDatasQuery request, CancellationToken cancellationToken)
        {
            return await _dataReadRepository.GetAllDatas(cancellationToken);
        }
    }
}
