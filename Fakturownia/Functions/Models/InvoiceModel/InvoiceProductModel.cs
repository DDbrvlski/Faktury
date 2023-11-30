using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakturownia.Functions.Models.InvoiceModel
{
    public class InvoiceProductModel
    {
        public string name { get; set; }
        public int tax { get; set; } = 23;
        public decimal price_net { get; set; }
        public decimal total_price_gross { get; set; }
        public int quantity { get; set; }
        public string quantity_unit { get; set; } = "szt";
    }
}
