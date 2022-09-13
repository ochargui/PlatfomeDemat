using DEMAT.Models;
using DEMAT.Models.Sendgrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.Sendgrid
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Email email);
    }
}
