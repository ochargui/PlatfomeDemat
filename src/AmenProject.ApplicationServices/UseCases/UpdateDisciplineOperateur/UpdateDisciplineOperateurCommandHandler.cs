using DEMAT.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.UpdateDisciplineOperateur
{
   public  class UpdateDisciplineOperateurCommandHandler : IRequestHandler<UpdateDisciplineOperateurCommand, string>
    {
        private readonly IOperateurReadRepository _operateurReadRepository;
       private readonly IAmenUnitOfWork _amenUnitOfWork;

        public UpdateDisciplineOperateurCommandHandler(IOperateurReadRepository operateurReadRepository, IAmenUnitOfWork amenUnitOfWork)
        {
            _operateurReadRepository = operateurReadRepository;
            _amenUnitOfWork = amenUnitOfWork;
        }

        public async Task<string> Handle(UpdateDisciplineOperateurCommand request, CancellationToken cancellationToken)
        {
            var result = await _operateurReadRepository.UpdateDiscipline(request.IdOperateur, request.Discipline, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);
            return result;
        }
    }
}
