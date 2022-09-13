using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetAllAgences
{
    public class GetAllAgencesQuery : IRequest<IEnumerable<AgenceModel>>
    {
    }
}
