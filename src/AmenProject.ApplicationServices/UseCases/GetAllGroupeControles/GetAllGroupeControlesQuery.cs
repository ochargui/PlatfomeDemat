using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetAllGroupeControles
{
    public class GetAllGroupeControlesQuery : IRequest<IEnumerable<GroupeControleModel>>
    {
    
    }
}
