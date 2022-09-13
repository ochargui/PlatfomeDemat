using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.UpdatePssword
{
    public class UpdatePasswordCommande : IRequest<string>
    {


        public string id { get; set; }
        public string currentPWD { get; set; }
        public string newPWD { get; set; }
        public string confirmdPWD { get; set; }




        public UpdatePasswordCommande(string Id, string CurrentPWD, string NewPWD, string ConfirmedPWD)
        {
            id = Id;
            currentPWD = CurrentPWD;
            newPWD = NewPWD;
            confirmdPWD = ConfirmedPWD;




        }
    }
}
