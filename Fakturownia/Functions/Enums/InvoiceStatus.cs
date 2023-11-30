using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakturownia.Functions.Enums
{
    public enum InvoiceStatus
    {
        issued = 0,
        paid = 1
    }
    public static class InvoiceStatusHelper
    {
        public static string GetInvoiceStatusName(int payment_status_id)
        {
            switch ((InvoiceStatus)payment_status_id)
            {
                case InvoiceStatus.issued:
                    return "issued";
                case InvoiceStatus.paid:
                    return "paid";
                default:
                    return "";
            }
        }
    }
}
