using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.UpdateEtatdocBrute
{
    public class UpdateEtatdocBruteCommand :IRequest<string>
    {
        public Guid DocBruteId { get; set; }
        public int Etat { get; set; }

        public UpdateEtatdocBruteCommand(Guid IdDocBrute,int etat )
        {
            DocBruteId = IdDocBrute;
            Etat = etat;
        }
    }
}
