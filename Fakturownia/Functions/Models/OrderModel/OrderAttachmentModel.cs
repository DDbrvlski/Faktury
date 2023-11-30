using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakturownia.Functions.Models.OrderModel
{
    public class OrderAttachmentModel
    {
        public int order_id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int type { get; set; }
    }
}
