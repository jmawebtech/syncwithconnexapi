using ConnexForQuickBooks.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace ConnexAPI.Web
{
    public class ConnexController : Controller
    {
        public IActionResult AuthenticateToWebService()
        {
            return Json("OK");
        }

        /// <summary>
        /// Syncs orders from QuickBooks to your website. Our tool downloads orders, holds them in a queue, and this is how they are returned.
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateOrders()
        {
            JMAUser user = MakeOrder();

            return Json(user);
        }

        /// <summary>
        /// Used to pull orders by order number
        /// </summary>
        /// <returns></returns>
        public IActionResult Orders(string orderNumber)
        {
            JMAUser user = MakeOrder();

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            return Json(user);
        }


        /// <summary>
        /// Syncs orders from your website to QuickBooks by date. Our tool downloads orders, holds them in a queue, and this is how they are returned.
        /// Cancelled orders are refunds.
        /// </summary>
        /// <param name="date_modified_min">Begin date for date modified. Used to pull sales by date modified and order status.</param>
        /// <param name="date_modified_max">End date for date modified. Used to pull sales by date modified and order status.</param>
        /// <param name="date_created_min">Begin date for date created. Used to pull the most recent sales.</param>
        /// <param name="date_created_max">End date for date created. Used to pull the most recent sales.</param>
        /// <param name="status">Shipped, Completed, etc.</param>
        /// <param name="storeName">Amazon, eBay, another filter used to sync orders.</param>
        /// <returns></returns>
        public IActionResult Orders(DateTime date_modified_min, DateTime date_modified_max, DateTime date_created_min, DateTime date_created_max, string orderStatus, string storeName)
        {
            JMAUser user = new JMAUser();

            JMAOrder order = GetOrder();

            user.Orders.Add(order);
            user.CancelledOrders.Add(order);

            return Json(user);
        }


        private JMAUser MakeOrder()
        {
            JMAUser user = new JMAUser();

            JMAOrder order = GetOrder();

            user.Orders.Add(order);
            return user;
        }

        private JMAOrder GetOrder()
        {
            JMAOrder order = new JMAOrder();

            order.BillingAddress = new JMAAddress()
            {
                Address1 = "123 Main St",
                Address2 = "STE 164",
                City = "Watertown",
                RegionName = "MA",
                PostalCode = "02472",
                Email = "test@example.com",
                PhoneNumber = "7813300737",
                FullName = "John Example",
                Company = "Example",
                FirstName = "John",
                LastName = "Example"
            };

            order.ShippingAddress = order.BillingAddress;

            order.OrderNumber = "1";
            order.CreationDate = new DateTime(2021, 1, 1);

            order.CreditCardName = "Mastercard";

            order.OrderDetails.Add(new JMAOrderDetail()
            {
                Quantity = 1,
                Sku = "Test",
                Name = "TestName",
                PriceExclTax = 100M,
                PriceInclTax = 106.25M
            });

            order.Total = 106.25M;
            order.TotalExclTax = 100M;
            order.TotalInclTax = 106.25M;
            return order;
        }
    }
}
