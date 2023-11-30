using Fakturownia.Functions.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Fakturownia.Functions.AddInvoiceToOrderFunction
{
    public class AddInvoiceToOrder
    {
        private readonly IOrderService _orderService;
        public AddInvoiceToOrder(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [FunctionName("AddInvoiceToOrder")]
        public void Run([TimerTrigger("0 */30 * * * *")] TimerInfo myTimer, ILogger log)
        {
            _orderService.GetOrdersAsync();
        }
    }
}