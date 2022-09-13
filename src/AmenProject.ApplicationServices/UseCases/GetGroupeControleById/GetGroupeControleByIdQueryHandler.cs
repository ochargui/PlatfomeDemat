using DEMAT.ApplicationServices.UseCases.GetPacketById;
using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.GetGroupeControleById
{
    public class GetGroupeControleByIdQueryHandler : IRequestHandler<GetGroupeControleByIdQuery, GroupeControleModel>
    {
        private readonly IGroupeControleReadRepository _groupeControleRepository;

        public GetGroupeControleByIdQueryHandler(IGroupeControleReadRepository groupeControleRepository)
        {
            _groupeControleRepository = groupeControleRepository;
        }
       
        public async Task<GroupeControleModel> Handle(GetGroupeControleByIdQuery request, CancellationToken cancellationToken)
        {
          return await _groupeControleRepository.GetGroupeControleById(request.GroupeControleID, cancellationToken);
        }
    }

    
}
