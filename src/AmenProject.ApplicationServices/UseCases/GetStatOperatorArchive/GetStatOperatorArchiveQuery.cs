using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetStatOperatorArchive
{
    public class GetStatOperatorArchiveQuery : IRequest<IEnumerable<OperateurArchiveSatModel>>
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Equipe { get; set; }

        public GetStatOperatorArchiveQuery(DateTimeOffset Datedebut, DateTimeOffset DateFin, string equipe)
        {
            StartDate = Datedebut;
            EndDate = DateFin;
            Equipe = equipe;
        }
    }
}
