using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakturownia.Functions.Enums
{
    public enum PaymentMethod
    {
        cash_on_delivery = 1,
        transfer = 2,
        card = 3
    }
    public static class PaymentMethodHelper
    {
        public static string GetPaymentMethodName(int payment_type_id)
        {
            switch ((PaymentMethod)payment_type_id)
            {
                case PaymentMethod.cash_on_delivery:
                    return "cash_on_delivery";
                case PaymentMethod.transfer:
                    return "transfer";
                case PaymentMethod.card:
                    return "card";
                default:
                    return "";
            }
        }
    }
}
