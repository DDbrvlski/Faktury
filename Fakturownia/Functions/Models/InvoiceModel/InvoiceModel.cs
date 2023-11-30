using Fakturownia.Functions.AddInvoiceToOrderFunction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakturownia.Functions.Models.InvoiceModel
{
    public class InvoiceModel
    {
        public string kind { get; set; }
        public string number { get; set; }
        public string sell_date { get; set; }
        public string issue_date { get; set; }

        public string seller_name { get; set; }
        public string seller_tax_no { get; set; }
        public string seller_street { get; set; }
        public string seller_post_code { get; set; }
        public string seller_city { get; set; }
        public string seller_country { get; set; }
        public string seller_email { get; set; }
        public string seller_www { get; set; }
        public string seller_phone { get; set; }
        public string seller_bank { get; set; }
        public string seller_bank_account { get; set; }

        public string buyer_name { get; set; }
        public string buyer_person { get; set; }
        public string buyer_tax_no { get; set; }
        public string buyer_street { get; set; }
        public string buyer_post_code { get; set; }
        public string buyer_city { get; set; }
        public string buyer_country { get; set; }
        public string buyer_email { get; set; }

        public string currency { get; set; }
        public string lang { get; set; }
        public string payment_type { get; set; }
        public string status { get; set; }
        public List<InvoiceProductModel> positions { get; set; } = new List<InvoiceProductModel>();
    }
}
