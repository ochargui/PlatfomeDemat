using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.CreateData
{
    public class CreateDataCommand : IRequest<Guid>
    {
        public String ControleValue { get; set; }
        public Guid? IdArchive { get; set; }
        public Guid? IdControle { get; set; }

        internal Data ToEntity()
        {
            return new Data
            {
                ControleValue = ControleValue,
                ArchiveId = IdArchive,
                ControleId = IdControle
            };
        }


    }
}
