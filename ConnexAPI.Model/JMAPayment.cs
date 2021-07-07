using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnexForQuickBooks.Model
{
    [Serializable]
    public class JMAPayment
    {
        public JMAPayment()
        {
            Order = new JMAOrder();
        }

        public int PaymentMethodId { get; set; }
        public string CreditCardName { get; set; }
        public string DepositAccount { get; set; }
        public int Id { get; set; }
        public bool AutoApply { get; set; }
        public int OrderId { get; set; }
        public JMAOrder Order { get; set; }
        public string PaymentType { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string QuickBooksInvoiceTxnId { get; set; }
        public string ReferenceNumber { get; set; }
        public bool SkipPayment { get; set; }
        public string PaymentStatus { get; set; }

        public string OrderNumber
        {
            get
            {
                return Order.OrderNumber;
            }
        }

        public DateTime ModifiedDate { get; set; }
        public string OriginalOrderId { get; set; }
        public string Note { get; set; }
        public decimal PaymentAmountInCustomerCurrency { get; set; }
    }
}
