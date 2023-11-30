using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakturownia.Functions.Models.OrderModel
{
    public class OrderCustomerModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string company { get; set; }
        public string phone_1 { get; set; }
        public string billing_country { get; set; }
        public string billing_city { get; set; }
        public string billing_street { get; set; }
        public string billing_postal_code { get; set; }
        public string bank { get; set; }
        public string iban { get; set; }
    }
}
