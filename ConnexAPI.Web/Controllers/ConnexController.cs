using ConnexForQuickBooks.Model;
using ConnexForQuickBooks.Model.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace ConnexAPI.Web
{
    public class ConnexController : Controller
    {
        /// <summary>
        /// We send the user name and password, using basic auth.
        /// For more details, visit https://jasonwatmore.com/post/2019/10/21/aspnet-core-3-basic-authentication-tutorial-with-example-api
        /// </summary>
        /// <returns></returns>
        public IActionResult AuthenticateToWebService()
        {
            return Json("OK");
        }

        /// <summary>
        /// Syncs orders from QuickBooks to your website. 
        /// Our tool downloads orders, holds them in a queue, and sends them to your website when the QuickBooks Web Connector is run.
        /// This is how orders will look, when our tool sends them to QuickBooks.
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
        /// <param name="orderStatus">Shipped, Completed, etc.</param>
        /// <param name="storeName">Amazon, eBay, another filter used to sync orders.</param>
        /// <returns></returns>
        public IActionResult Orders(DateTime date_modified_min, DateTime date_modified_max, DateTime date_created_min, DateTime date_created_max, string orderStatus, string storeName)
        {
            JMAUser user = new JMAUser();

            JMAOrder order = GetOrder();

            AddSalesOrder(user);

            user.Orders.Add(order);
            user.CancelledOrders.Add(order);

            return Json(user);
        }

        /// <summary>
        /// Creates a sales order in QuickBooks. This setting allows the developer to choose the type of transaction to create in QuickBooks.
        /// If your order status was unshipped, then sales order would be acceptable.
        /// </summary>
        /// <param name="user"></param>
        private void AddSalesOrder(JMAUser user)
        {
            JMAOrder order2 = GetOrder();
            order2.InvoiceMode = InvoiceModeEnum.SalesOrders;
            user.Orders.Add(order2);
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
