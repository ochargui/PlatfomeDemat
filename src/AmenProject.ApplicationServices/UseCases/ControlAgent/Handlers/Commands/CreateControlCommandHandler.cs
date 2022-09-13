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
    public class CreateControlCommandHandler : IRequestHandler<CreateControlCommand, Control>
    {

        private readonly IAmenUnitOfWork _amenUnitOfWork;
        private readonly IControlReadRepository _controlReadRepository;

        public CreateControlCommandHandler(IAmenUnitOfWork amenUnitOfWork, IControlReadRepository controlReadRepository)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _controlReadRepository = controlReadRepository;
        }
        public async Task<Control> Handle(CreateControlCommand request, CancellationToken cancellationToken)
        {
            //    var control = new Control
            //    {

            //        FieldNumberScore = request.FieldNumberScore,
            //        ClientSignatureScore = request.ClientSignatureScore,
            //        BankStampScore = request.BankStampScore,
            //        ScoreLimit=request.ScoreLimit ,
            //        Name= request.Name
            //    };
            var control = await _controlReadRepository.GetControlByName(request.Name, cancellationToken);
            if(control == null)
            {
                 control = new Control
               {

                    FieldNumberScore = request.FieldNumberScore,
                    ClientSignatureScore = request.ClientSignatureScore,
                    BankStampScore = request.BankStampScore,
                    ScoreLimit = request.ScoreLimit,
                    Name = request.Name
                };

            }
            else
            {
                control.FieldNumberScore = request.FieldNumberScore;
                control.ClientSignatureScore = request.ClientSignatureScore;
                control.BankStampScore = request.BankStampScore;
                control.ScoreLimit = request.ScoreLimit;
            }

            await _amenUnitOfWork.ControlRepository.AddOrUpdateAsync(control, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            return control;
        }
    }
}
