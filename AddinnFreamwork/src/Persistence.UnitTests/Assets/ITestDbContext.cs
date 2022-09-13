using Microsoft.EntityFrameworkCore;
using Addinn.Framework.Core;

namespace Persistence.UnitTests.Assets
{
    public interface ITestDbContext : IContext
    {
        DbSet<Customer> Customers { get; set; }
    }
}
