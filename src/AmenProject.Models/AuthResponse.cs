using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.Models
{
    public class AuthResponse
    {
        public string Email { get; set; }

        public string DiplayName { get; set; }

        public string Token { get; set; }
        public string Message { get; set; }
    }
}
