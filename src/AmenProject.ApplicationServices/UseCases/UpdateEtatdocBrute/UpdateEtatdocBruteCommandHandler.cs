using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.UpdateEtatdocBrute
{
    public class UpdateEtatdocBruteCommandHandler : IRequestHandler<UpdateEtatdocBruteCommand, string>
    {
        private readonly IDocBruteReadRepository _docBruteReadRepository;
        private readonly IAmenUnitOfWork _amenUnitOfWork;
        public UpdateEtatdocBruteCommandHandler(IDocBruteReadRepository docBruteReadRepository, IAmenUnitOfWork amenUnitOfWork)
        {
            _docBruteReadRepository = docBruteReadRepository;
            _amenUnitOfWork = amenUnitOfWork;
        }
        public async  Task<string> Handle(UpdateEtatdocBruteCommand request, CancellationToken cancellationToken)
        {
            var result = await _docBruteReadRepository.UpdateEtatDocBrute(request.DocBruteId,request.Etat,cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);
            return result;
        }

    }
}
