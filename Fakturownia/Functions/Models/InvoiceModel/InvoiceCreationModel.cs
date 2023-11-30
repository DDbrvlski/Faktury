using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakturownia.Functions.Models.InvoiceModel
{
    public class InvoiceCreationModel
    {
        public string api_token { get; set; }
        public InvoiceModel invoice { get; set; }
    }
}
