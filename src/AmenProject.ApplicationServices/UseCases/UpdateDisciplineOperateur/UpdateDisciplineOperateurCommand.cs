using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.UpdateDisciplineOperateur
{
    public class UpdateDisciplineOperateurCommand : IRequest<string>
    {
        public Guid IdOperateur { get; set; }
        public string Discipline { get; set; }

        public UpdateDisciplineOperateurCommand(Guid idOperateur, string discipline)
        {
            IdOperateur = idOperateur;
            Discipline = discipline;
        }
    }
}
