using Fakturownia.Functions.Models;
using Fakturownia.Functions.Models.OrderModel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakturownia.Functions.Services
{
    public interface IOrderService
    {
        public Task GetOrdersAsync();
    }
    public class OrderService : IOrderService
    {
        private readonly IAuthService _authService;
        private readonly IInvoiceService _invoiceService;
        private readonly Dictionary<string, ServicesSettings> _servicesSettings;

        public OrderService(IAuthService authService, Dictionary<string, ServicesSettings> servicesSettings, IInvoiceService invoiceService)
        {
            _authService = authService;
            _servicesSettings = servicesSettings;
            _invoiceService = invoiceService;
        }

        public async Task GetOrdersAsync()
        {
            foreach (var (serviceName, serviceSettings) in _servicesSettings)
            {
                using (var client = _authService.SetEMAGRestClient(serviceSettings.URL_API))
                {
                    var request = new RestRequest("/order/read", Method.Get);
                    request.AddQueryParameter("status", 3);

                    try
                    {
                        var result = client.ExecuteAsync(request).Result;

                        if (result != null)
                        {
                            var orders = JsonConvert.DeserializeObject<List<OrderModel>>(result.Content);

                            if (orders != null)
                            {
                                foreach (var order in orders)
                                {
                                    int responseId = await _invoiceService.CreateInvoice(_invoiceService.ConvertOrderToInvoice(order, serviceSettings.Currency, serviceSettings.Lang));
                                    if (responseId != -1)
                                    {
                                        await Task.Delay(3000);
                                        await AddInvoiceToOrder(responseId, order.id, serviceSettings);
                                    }
                                }
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        Console.Out.WriteLine(ex.Message);
                    }
                }
                await Task.Delay(3000);
            }
        }

        private async Task AddInvoiceToOrder(int invoiceId, int orderId, ServicesSettings serviceSettings)
        {
            using (var client = _authService.SetEMAGRestClient(serviceSettings.URL_API))
            {
                var request = new RestRequest("/order/attachments/save", Method.Post);

                OrderAttachmentModel model = new OrderAttachmentModel()
                {
                    order_id = orderId,
                    name = "Invoice",
                    url = _invoiceService.GetInvoiceDownloadURL(invoiceId),
                    type = 1
                };

                request.AddJsonBody(model);

                try
                {
                    var response = await client.ExecuteAsync(request);
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine(ex.Message);
                }

            }

        }
    }
}
