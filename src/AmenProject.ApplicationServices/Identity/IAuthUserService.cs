using DEMAT.Infrastructure.Identity.Models;
using DEMAT.Models;
using DEMAT.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.Identity
{
    public interface IAuthUserService
    {   
        Task<AuthResponse> Login(AuthRequest request);

        Task<UserDto> GetCurrentUser(string email, string role);

        Task<bool> SendResetEmail(string email);
        public Task<(bool isSucceed, string Id)> CreateUserAsync(string DisplayName,string userName, string password, string email, string FirstName, string LastName, List<string> roles);

        Task<IdentityResult> ResetPassword(string email , string newPassword , string identityToken , string secondSecurityLayer );
        
        public Task<string> UpdatePassword(string id, string currentPWD, string newPWD, string confirmdPWD, CancellationToken cancellationToken);

        public  Task<string> GetUserIdAsync(string userName, CancellationToken cancellationToken);
        public Task<List<(string Id, string DisplayName,string FirstName, string LastName, string UserName, string Email , string PhoneNumber)>> GetAllUsersAsync();
        public Task<bool> DeleteUserAsync(string userId);

        Task<bool> UpdateUserProfile(string id, string LastName,string FirstName, string email, IList<string> roles);
    }
}
