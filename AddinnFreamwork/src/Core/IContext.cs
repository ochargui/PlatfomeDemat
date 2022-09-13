using System;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.Core
{
    /// <summary>
    /// Interface de base du context de persistence. Utile pour les tests unitaires
    /// </summary>
    public interface IContext : IDisposable
    {
        /// <summary>
        /// Sauvegarde les changements courants du context
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
