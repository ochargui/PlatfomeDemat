using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace DEMAT.Core.Security
{
    /// <summary>
    /// User service implementation
    /// </summary>
    /// <seealso cref="DEMAT.Core.Security.IUserService" />
    public class UserService : IUserService
    {
        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public IPrincipal Current { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            if (!(Current is ClaimsPrincipal ci))
            {
                return string.Empty;
            }

            return ci.Claims?.FirstOrDefault(c => c.Type == AddinnClaimTypes.Name || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value ??
                $"{ci.Claims?.FirstOrDefault(c => c.Type == AddinnClaimTypes.GivenName)?.Value} {ci.Claims?.FirstOrDefault(c => c.Type == AddinnClaimTypes.FamilyName)?.Value}".Trim();
        }

        /// <summary>
        /// Gets the subject identifier => Email
        /// </summary>
        /// <returns></returns>
        public string GetSubjectId()
        {
            return GetEmail(); // email to be used as the user identifier

            //if (!(Current is ClaimsPrincipal ci))
            //{
            //    return string.Empty;
            //}

            //return ci.Claims?.FirstOrDefault(c => c.Type == AddinnClaimTypes.Subject)?.Value;
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <returns></returns>
        public string GetEmail()
        {
            if (!(Current is ClaimsPrincipal ci))
            {
                return string.Empty;
            }

            return ci.Claims?.FirstOrDefault(c => c.Type == AddinnClaimTypes.Email || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/email")?.Value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class. With current principal set to ClaimsPrincipal.Current
        /// </summary>
        public UserService()
        {
            Current = ClaimsPrincipal.Current;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public UserService(Func<IPrincipal> user)
        {
            Current = user.Invoke();
        }
    }
}
