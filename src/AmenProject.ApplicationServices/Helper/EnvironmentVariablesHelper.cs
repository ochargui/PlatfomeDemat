using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.Helper
{
    public static class EnvironmentVariablesHelper
    {
        public static string GetTokenKey()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                return Environment.GetEnvironmentVariable("TOKEN_KEY");
            else
                return "Demat secret key";
        }

        public static string GetTokenIssuer()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                return Environment.GetEnvironmentVariable("TOKEN_ISSUER");
            else
                return "https://localhost:5001";
        }
    }
}
