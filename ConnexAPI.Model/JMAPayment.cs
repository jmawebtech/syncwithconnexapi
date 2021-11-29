using System;

namespace ConnexForQuickBooks.Model
{
    [Serializable]
    public class JMAPayment
    {
        public JMAPayment()
        {
        }

        /// <summary>
        /// Payment method, like VISA.
        /// </summary>
        public string CreditCardName { get; set; }

        /// <summary>
        /// Deposit account, like undeposited funds.
        /// </summary>
        public string DepositAccount { get; set; }

        /// <summary>
        /// Amount of payment.
        /// </summary>
        public decimal PaymentAmount { get; set; }

        /// <summary>
        /// Date order was paid.
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Used to link payments and invoices in QuickBooks.
        /// </summary>
        public string QuickBooksInvoiceTxnId { get; set; }

        /// <summary>
        /// Alternate number used to identify payments.
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Decides whether to sync payment to QuickBooks.
        /// </summary>
        public bool SkipPayment { get; set; }

        /// <summary>
        /// Ties payment to order in QuickBooks.
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// If the order has multiple currencies, this is the total paid by the customer.
        /// Used to convert an order from one currency to another.
        /// </summary>
        public decimal PaymentAmountInCustomerCurrency { get; set; }
    }
}
