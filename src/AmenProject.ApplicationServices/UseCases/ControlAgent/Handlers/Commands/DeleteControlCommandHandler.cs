using DEMAT.ApplicationServices.UseCases.ControlAgent.Requests;
using DEMAT.Domain.Entities;
using DEMAT.Domain.Interfaces;
using DEMAT.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ControlAgent.Handlers.Commands
{
    public class DeleteControlCommandHandler : IRequestHandler<DeleteControlCommand, Control>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;
        private readonly IControlReadRepository _controlReadRepository;

        public DeleteControlCommandHandler(IAmenUnitOfWork amenUnitOfWork,
            IControlReadRepository controlReadRepository)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _controlReadRepository = controlReadRepository;
        }
        public async Task<Control> Handle(DeleteControlCommand request, CancellationToken cancellationToken)
        {
            var control = await _controlReadRepository.GetControlById(request.Id, cancellationToken);
            if (control == null)
            {
                throw new Exception("There is no control with such an Id");
            }
             _amenUnitOfWork.ControlRepository.Remove(control);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            return control;
        }
    }
}
