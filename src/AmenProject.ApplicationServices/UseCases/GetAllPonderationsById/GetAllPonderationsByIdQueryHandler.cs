using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetAllPonderationsById
{
    public class GetAllPonderationsByIdQueryHandler : IRequestHandler<GetAllPonderationsByIdQuery, AllPonderationModel>
    {
        private readonly IAllPonderationReadRepository _allPonderationReadRepository;

        public GetAllPonderationsByIdQueryHandler(IAllPonderationReadRepository allPonderationReadRepository)
        {
            _allPonderationReadRepository = allPonderationReadRepository;
        }

        public async Task<AllPonderationModel> Handle(GetAllPonderationsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _allPonderationReadRepository.GetAllPonderationById(request.AllPonderationId, cancellationToken);
        }
    }
}
