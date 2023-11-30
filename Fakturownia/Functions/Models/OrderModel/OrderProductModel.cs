using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakturownia.Functions.Models.OrderModel
{
    public class OrderProductModel
    {
        public int product_id { get; set; }
        //public string product_name {  get; set; }
        public decimal sale_price { get; set; }
        public decimal tax_value { get; set; }
        public int quantity { get; set; }
    }
}
