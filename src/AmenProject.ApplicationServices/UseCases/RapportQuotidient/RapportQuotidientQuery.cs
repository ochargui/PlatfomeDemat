using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.RapportQuotidient
{
    public class RapportQuotidientQuery :IRequest<IEnumerable<RapportQuotidienModel>>
    {
        public DateTimeOffset DateTraitement { get; set; }
        public Boolean Journee { get; set; }

        public RapportQuotidientQuery(DateTimeOffset dateTraitement, bool journee)
        {
            DateTraitement = dateTraitement;
            Journee = journee;
        }
    }
}
