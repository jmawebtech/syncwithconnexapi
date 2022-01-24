using ConnexAPI.Model;
using ConnexForQuickBooks.Model;
using ConnexForQuickBooks.Model.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        /// Set purchase type to PO to create purchase orders in QuickBooks.
        /// Bill is preferred because it decrements inventory.
        /// Example URL is 
        /// </summary>
        /// <returns></returns>
        public IActionResult PurchaseOrders(DateTime date_modified_min, DateTime date_modified_max, DateTime date_created_min, DateTime date_created_max, string orderStatus, string storeName)
        {
            JMAUser user = MakeBill();
            return Json(user);
        }

        private static JMAUser MakeBill()
        {
            JMAUser user = new JMAUser();
            user.Purchases.Add(new JMAOrder()
            {
                OrderNumber = "123",
                Vendor = "Test Vendor",
                BillingAddress = new JMAAddress()
                {
                    Company = "Test Vendor",
                    FirstName = "Test",
                    LastName = "User",
                    Address1 = "123 Main St.",
                    City = "Watertown",
                    RegionName = "MA",
                    PostalCode = "02472",
                    TwoLetterIsoCode = "US"
                },
                ShippingAddress = new JMAAddress()
                {
                    Company = "Test Vendor",
                    FirstName = "Test",
                    LastName = "User2",
                    Address1 = "1 Main St.",
                    City = "Watertown",
                    RegionName = "MA",
                    PostalCode = "02472",
                    TwoLetterIsoCode = "US"
                },
                PurchaseType = JMAPurchaseType.Bill,
                CreationDate = new DateTime(2022, 1, 1),
                OrderDetails = new List<JMAOrderDetail>()
                {
                    new JMAOrderDetail()
                    {
                        Sku = "Test",
                        Quantity = 1,
                        PriceExclTax = 50,
                        PriceInclTax = 50
                    },
                    new JMAOrderDetail()
                    {
                        Sku = "Test2",
                        Quantity = 1,
                        PriceExclTax = 50,
                        PriceInclTax = 50
                    },
                },
                Total = 100,
                TotalExclTax = 100,
                TotalInclTax = 100
            });
            return user;
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

            //We link order.OrderNumber with payment.OrderNumber. Allows our tool to sync payments with QuickBooks.
            JMAPayment payment = new JMAPayment()
            {
                ReferenceNumber = order.OrderNumber,
                OrderNumber = order.OrderNumber,
                CreditCardName = order.CreditCardName,
                PaymentDate = order.CreationDate,
                PaymentAmount = order.Total
            };

            user.Payments.Add(payment);

            return user;
        }

        /// <summary>
        /// JMAOrder.ReferenceNumber is used for deposit matching. In this case, we mapped the charge ID from Stripe.
        /// </summary>
        /// <returns></returns>
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

            order.ReferenceNumber = "ch_3JQ3NqBozibG58Lk2KbwpEV3";
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

        /// <summary>
        /// Our tool sends a list of products to your website.
        /// Product SKU is QuickBooks item name.
        /// Product StockQuantity is the stock level from QuickBooks.
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Inventory([FromBody] List<JMAProduct> products)
        {
            //Perform some updates on your side with List<JMAProduct>

            JMAUpdateInventoryResponse response = new JMAUpdateInventoryResponse();
            response.ProductSyncSuccess.AddRange(products);
            response.ProductSkusErrors.Add("TestProduct", "No matching SKU.");

            return Json(response);
        }

        /// <summary>
        /// --CODE Inside of Connex that sends data to UpdateProducts. We send a post request 
        /// Send stock and pricing updates from your custom store to QuickBooks.
        /// </summary>
        //public override JMAInventoryResponse UpdateProducts(List<JMAProduct> products)
        //{
        //    JMAInventoryResponse inventoryResponse = new JMAInventoryResponse();

        //    string json = JsonConvert.SerializeObject(products);
        //    string inventoryUrl = String.Format("{0}/inventory", QBUrl);
        //    string result = PostPutJson(inventoryUrl, json, "PUT");
        //    JMAUpdateInventoryResponse response = JsonConvert.DeserializeObject<JMAUpdateInventoryResponse>(result);

        //    foreach (JMAProduct product in response.ProductSyncSuccess)
        //        inventoryResponse.ProductsThatSyncedSuccessfully.Add(product.Sku);

        //    List<JMAErrorMessage> messages = new List<JMAErrorMessage>();
        //    foreach (KeyValuePair<string, string> pair in response.ProductSkusErrors)
        //    {
        //        messages.Add(new JMAErrorMessage()
        //        {
        //            Severity = MessageSeverity.Error,
        //            Message = pair.Value
        //        });
        //    }

        //    inventoryResponse.Messages.AddRange(messages);

        //    return inventoryResponse;
        //}


    }
}
