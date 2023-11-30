using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakturownia.Functions.Models.OrderModel
{
    public class OrderModel
    {
        public int id { get; set; }
        public DateTime date {  get; set; }
        public int payment_mode_id { get; set; }
        public int payment_status { get; set; } // 0 - not paid 1 - paid
        public List<OrderCustomerModel> customer {  get; set; }
        public List<OrderProductModel> products { get; set; }
        public List<OrderAttachmentModel> attachments { get; set; }
    }
}
