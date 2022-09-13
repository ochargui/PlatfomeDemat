using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.CreateOperation
{
    public class CreateOperationCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int Pd { get; set; }
        internal Operation ToEntity()
        {
            return new Operation
            {
                OperationName = Name,
                CodeOperation = Code,
                Ponderation = Pd
            };
        }
    }
}
