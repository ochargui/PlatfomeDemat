using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.RapportFacturation
{
    public class RapportFacturationQuery : IRequest<IEnumerable<RapportFacturationModel>>
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public RapportFacturationQuery(DateTimeOffset Datedebut, DateTimeOffset DateFin)
        {
            StartDate = Datedebut;
            EndDate = DateFin;
        }
    }
}
