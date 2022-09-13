using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.Models.Dtos
{
    //This is a Dto for the user to return 
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
    }
}
