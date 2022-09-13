using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEMAT.Infrastructure.Identity.Models;

namespace DEMAT.ApplicationServices.Jwt
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);

        string ToSHA256(string textToHash);

        string GetRandomHexNumber(int digits);

        string DateTimeToTimestamp(DateTime date);
    }
}
