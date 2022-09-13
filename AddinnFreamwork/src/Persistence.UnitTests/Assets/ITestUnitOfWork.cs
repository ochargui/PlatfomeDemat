using Addinn.Framework.Core;

namespace Persistence.UnitTests.Assets
{
    public interface ITestUnitOfWork : IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
    }
}
