using DEMAT.Domain.Entities;
using DEMAT.Models;
using DEMAT.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DEMAT.Infrastructure.Identity.Models;

namespace DEMAT.Domain.Interfaces
{
    public interface IUserReadRepository
    {
        public Task<AppUser> GetUserById(Guid Id, CancellationToken cancellationToken);
      


    }
}
