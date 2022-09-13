using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.CreateLotArchive
{
    public class CreateLotArchiveCommand : IRequest<Guid>
    {
      
        public int Code { get; set; }
        public string Name { get; set; }
        

        internal LotArchive ToEntity()
        {
            return new LotArchive
            {
                CodeLotArchive = Code,
                LotArchiveName = Name,
                CreatedDate = DateTimeOffset.Now
            };
        }

    }
}
