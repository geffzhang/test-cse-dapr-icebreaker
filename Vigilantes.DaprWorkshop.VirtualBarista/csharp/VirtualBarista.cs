using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Vigilantes.DaprWorkshop.VirtualBarista.Models;

namespace Vigilantes.DaprWorkshop.VirtualBarista
{
    public class VirtualBarista : BackgroundService
    {
        public static readonly string StoreId = Environment.GetEnvironmentVariable("STORE_ID") ?? "Redmond";
        public static readonly int MinSecondsToMakeDrink = int.Parse(Environment.GetEnvironmentVariable("MIN_SECONDS_TO_MAKE_DRINK") ?? "5");
        public static readonly int MaxSecondsToMakeDrink = int.Parse(Environment.GetEnvironmentVariable("MAX_SECONDS_TO_MAKE_DRINK") ?? "15");
        private readonly HttpClient _httpClient;
        private readonly ILogger<VirtualBarista> _logger;
        private Task _drinkTask = Task.CompletedTask;

        public VirtualBarista(IHttpClientFactory httpClientFactory, ILogger<VirtualBarista> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"The VirtualBarista ({StoreId}) is ready to make drinks!");

            stoppingToken.Register(() =>
            {
                _logger.LogInformation($"Finish making that drink and go home, VirtualBarista.");

                _drinkTask.Wait();

                _logger.LogInformation($"The VirtualBarista is heading home for the day. No drinks for you!");
            });

            List<OrderSummary> orders = new List<OrderSummary>();
            Random random = new Random();
            await Task.Delay(3000);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"The VirtualBarista is checking orders on the make line...");

                    orders.AddRange(await GetOrders(stoppingToken));

                    _logger.LogInformation($"There {(orders.Count == 1 ? "is" : "are")} {orders.Count} {(orders.Count == 1 ? "order" : "orders")} waiting to be made.");

                    if (orders.Count > 0)
                    {
                        while (orders.Count > 0 && !stoppingToken.IsCancellationRequested)
                        {
                            var order = orders.First();

                            _logger.LogInformation($"The VirtualBarista is making an order for {order.CustomerName}...");

                            foreach (var orderItem in order.OrderItems)
                            {
                                if (!stoppingToken.IsCancellationRequested)
                                {
                                    _logger.LogInformation($"The VirtualBarista is making {orderItem.Quantity} {orderItem.MenuItemName}.");

                                    _drinkTask = Task.Delay(random.Next(MinSecondsToMakeDrink * 1000, MaxSecondsToMakeDrink * 1000));
                                    await _drinkTask;

                                    _logger.LogInformation($"The VirtualBarista completed {orderItem.Quantity} {orderItem.MenuItemName}.");
                                }
                            }

                            if (!stoppingToken.IsCancellationRequested)
                            {
                                await CompleteOrder(order);

                                orders.RemoveAt(0);

                                _logger.LogInformation($"{order.CustomerName}, your drinks are ready!");
                            }
                        }
                    }
                    else
                    {
                        _logger.LogInformation($"The make line is empty! Time to drum up some customers!");

                        await Task.Delay(5000, stoppingToken);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, null);
                }
            }
        }

        private async Task CompleteOrder(OrderSummary order)
        {
            // TODO: Challenge 4 - Call the Makeline service via Dapr to complete the order
            await Task.Delay(5000);
        }

        private async Task<IEnumerable<OrderSummary>> GetOrders(CancellationToken stoppingToken)
        {
            // TODO: Challenge 4 - Call the Makeline service via Dapr to get orders
            await Task.Delay(5000);
            return Enumerable.Empty<OrderSummary>();
        }
    }
}