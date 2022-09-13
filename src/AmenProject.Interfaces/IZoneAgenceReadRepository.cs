using DEMAT.Domain.Entities;
using DEMAT.Models;
using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.Domain.Interfaces
{
    public interface IZoneAgenceReadRepository
    {
        Task<ZoneAgenceModel> GetZoneAgenceById(Guid zoneAgenceId, CancellationToken cancellationToken);

        Task<IEnumerable<ZoneAgenceModel>> GetAllZoneAgences(CancellationToken cancellationToken);
    }
}
