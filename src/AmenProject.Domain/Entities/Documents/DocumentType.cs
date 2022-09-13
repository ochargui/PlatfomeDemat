using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMAT.Domain.Entities.Documents
{
    public enum DocumentType
    {
        NotRecognized,
        Check,
        Withdrawal,
        CashPayment,
        CheckRemittanceSlip,
        PaymentOrder,
        Various
    }
}
