using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakturownia.Functions.Models
{
    public class SellerSettings
    {
        public string Name { get; set; }
        public string Tax_Identify { get; set; }
        public string Street { get; set; }
        public string Postal_Code { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Bank_Account { get; set; }
        public string Bank { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
