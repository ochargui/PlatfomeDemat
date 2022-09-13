using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetAllGroupeControles
{
    public class GetAllGroupeControlesQueryHandler : IRequestHandler<GetAllGroupeControlesQuery, IEnumerable<GroupeControleModel>>
    {
        private readonly IGroupeControleReadRepository _groupeControleReadRepository;

        public GetAllGroupeControlesQueryHandler(IGroupeControleReadRepository groupeControleReadRepository)
        {
            _groupeControleReadRepository = groupeControleReadRepository;
        }

        public async Task<IEnumerable<GroupeControleModel>> Handle(GetAllGroupeControlesQuery request, CancellationToken cancellationToken)
        {
            return await _groupeControleReadRepository.GetAllGroupeControles(cancellationToken);
        }
    }
}
