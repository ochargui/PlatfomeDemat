using DEMAT.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.ControlAgent.Requests
{
    public class DeleteControlCommand : IRequest<Control>
    {
        public Guid Id { get; set; }
        public DeleteControlCommand(Guid id)
        {
            Id = id;

        }
    }
}
