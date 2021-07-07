using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ConnexForQuickBooks.Model
{
    /// <summary>
    /// associates orders, payments with and without orders, and settings with a particular user
    /// </summary>
    [Serializable]
    public class JMAUser
    {
        public JMAUser()
        {
            CancelledOrders = new List<JMAOrder>();
            Orders = new List<JMAOrder>();
            Payments = new List<JMAPayment>();
            ProductsToQB = new List<JMAProduct>();
            ProductsFromQBToUpdate = new List<JMAProduct>();
            Purchases = new List<JMAOrder>();
        }

        public List<JMAOrder> CancelledOrders { get; set; }
        public List<JMAAddress> Customers { get; set; }
        public List<JMAProduct> InventoryAdjustments { get; set; }
        public List<JMAOrder> Orders { get; set; }
        public List<JMAPayment> Payments { get; set; }
        public List<JMAProduct> ProductsFromQBToUpdate { get; set; }
        public List<JMAProduct> ProductsToQB { get; set; }
        public List<JMAOrder> Purchases { get; set; }
    }
}
