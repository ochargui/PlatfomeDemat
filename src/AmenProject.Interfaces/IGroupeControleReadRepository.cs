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
    public interface IGroupeControleReadRepository
    {

        Task<GroupeControleModel> GetGroupeControleById(Guid groupeControleId, CancellationToken cancellationToken);

        Task<IEnumerable<GroupeControleModel>> GetAllGroupeControles(CancellationToken cancellationToken);

    }
}
