using DEMAT.Domain.Interfaces;
using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.SinIn
{
    public class SinInCommandHandler : IRequestHandler<SinInCommand, OperateurModel>
    {
        private readonly IOperateurReadRepository _operateurReadRepository;
        private readonly IAmenUnitOfWork _amenUnitOfWork;

        public SinInCommandHandler(IOperateurReadRepository operateurReadRepository, IAmenUnitOfWork amenUnitOfWork)
        {
            _operateurReadRepository = operateurReadRepository;
            _amenUnitOfWork = amenUnitOfWork;
        }

        public async Task<OperateurModel> Handle(SinInCommand request, CancellationToken cancellationToken)
        {
            return await _operateurReadRepository.SinIn(request.Login, request.Pwd, cancellationToken);
        }
    }
}
