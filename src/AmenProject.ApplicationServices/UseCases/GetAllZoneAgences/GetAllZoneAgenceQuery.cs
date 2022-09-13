using DEMAT.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMAT.ApplicationServices.UseCases.GetAllZoneAgences
{
    public class GetAllZoneAgenceQuery : IRequest<IEnumerable<ZoneAgenceModel>>
    {
    }
}
