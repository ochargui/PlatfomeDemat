using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetDataById
{
    public class GetDataByIdQueryHandler : IRequestHandler<GetDataByIdQuery, DataModel>
    {
        private readonly IDataReadRepository _dataReadRepository;

        public GetDataByIdQueryHandler(IDataReadRepository dataReadRepository)
        {
            _dataReadRepository = dataReadRepository;
        }

        public async Task<DataModel> Handle(GetDataByIdQuery request, CancellationToken cancellationToken)
        {
            return await _dataReadRepository.GetDataById(request.DataId, cancellationToken);
        }
    }
}
