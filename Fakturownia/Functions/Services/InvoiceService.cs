using Fakturownia.Functions.Enums;
using Fakturownia.Functions.Models;
using Fakturownia.Functions.Models.InvoiceModel;
using Fakturownia.Functions.Models.OrderModel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fakturownia.Functions.Services
{
    public interface IInvoiceService
    {
        public Task<int> CreateInvoice(InvoiceModel invoiceModel);
        public InvoiceModel ConvertOrderToInvoice(OrderModel orderModel, string currency, string lang);
        public string GetInvoiceDownloadURL(int invoiceId);
    }
    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceServiceSettings _invoiceServiceSettings;
        private readonly SellerSettings _sellerSettings;
        public InvoiceService(InvoiceServiceSettings invoiceServiceSettings, SellerSettings sellerSettings)
        {
            _invoiceServiceSettings = invoiceServiceSettings;
            _sellerSettings = sellerSettings;
        }
        public async Task<int> CreateInvoice(InvoiceModel invoiceModel) 
        {
            if (invoiceModel != null)
            {
                InvoiceCreationModel invoice = new InvoiceCreationModel();
                invoice.invoice = invoiceModel;
                invoice.api_token = _invoiceServiceSettings.API_TOKEN;

                using (var client = new RestClient(_invoiceServiceSettings.URL + ".json"))
                {
                    var request = new RestRequest()
                        .AddHeader("Accept", "application/json")
                        .AddHeader("Content-Type", "application/json")
                        .AddJsonBody(invoice);
                    
                    try
                    {
                        var response = await client.PostAsync(request);
                        var content = response.Content;

                        var responseObject = new { Id = 0 };

                        try
                        {
                            responseObject = JsonConvert.DeserializeAnonymousType(content, responseObject);

                            return responseObject.Id;
                        }
                        catch (JsonSerializationException ex)
                        {
                            Console.Out.WriteLine(ex.Message);
                            return -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Out.WriteLine(ex.Message);
                        return -1;
                    }
                }
            }
            return -1;
        }

        public InvoiceModel ConvertOrderToInvoice(OrderModel orderModel, string currency, string lang)
        {
            if (orderModel.attachments != null)
            {
                var isInvoiceExists = orderModel.attachments.FirstOrDefault(x => x.type == 1);
                if (isInvoiceExists != null)
                {
                    return null;
                }
            }

            var buyer = orderModel.customer.First();

            if (buyer == null)
            {
                return null;
            }

            InvoiceModel invoiceModel = new InvoiceModel()
            {
                kind = "vat",
                sell_date = orderModel.date.ToShortDateString(),
                issue_date = orderModel.date.ToShortDateString(),

                seller_name = _sellerSettings.Name,
                seller_tax_no = _sellerSettings.Tax_Identify,
                seller_street = _sellerSettings.Street,
                seller_post_code = _sellerSettings.Postal_Code,
                seller_city = _sellerSettings.City,
                seller_country = _sellerSettings.Country,
                seller_email = _sellerSettings.Email,
                seller_phone = _sellerSettings.Phone,
                seller_bank = _sellerSettings.Bank,
                seller_bank_account = _sellerSettings.Bank_Account,

                buyer_name = buyer.name,
                buyer_person = buyer.company,
                buyer_street = buyer.billing_street,
                buyer_post_code = buyer.billing_postal_code,
                buyer_city = buyer.billing_city,
                buyer_country = buyer.billing_country,
                buyer_email = buyer.email,

                currency = currency,
                lang = lang,
                payment_type = PaymentMethodHelper.GetPaymentMethodName(orderModel.payment_mode_id),
                status = InvoiceStatusHelper.GetInvoiceStatusName(orderModel.payment_status),

                positions = ConvertOrderProductsToInvoice(orderModel.products),
            };

            return invoiceModel;
        }

        private List<InvoiceProductModel> ConvertOrderProductsToInvoice(List<OrderProductModel> orderProducts)
        {
            List<InvoiceProductModel> invoiceProducts = new List<InvoiceProductModel>();

            foreach (var orderProduct in orderProducts)
            {
                invoiceProducts.Add(new InvoiceProductModel()
                {
                    name = orderProduct.product_id.ToString(),
                    quantity = orderProduct.quantity,
                    price_net = orderProduct.sale_price,
                    total_price_gross = CalculateBruttoPrice(orderProduct.sale_price, 23)
                });
            }

            return invoiceProducts;
        }

        private decimal CalculateBruttoPrice(decimal priceNetto, int vat)
        {
            return priceNetto * (1 + ((decimal)vat / 100));
        }

        public string GetInvoiceDownloadURL(int invoiceId)
        {
            return $"{_invoiceServiceSettings.URL}/{invoiceId}.pdf?api_token={_invoiceServiceSettings.API_TOKEN}";
        }
    }
}
