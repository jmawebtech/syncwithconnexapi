using ConnexForQuickBooks.Model.Enums;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ConnexForQuickBooks.Model
{
    [Serializable]
    public class JMAOrder
    {
        public JMAOrder()
        {
            JMACustomFields = new List<JMACustomField>();
            OrderDetails = new List<JMAOrderDetail>();
            Discounts = new List<JMADiscount>();
            CustomFields = new List<JMACustomField>();
            Customer = new JMACustomer();
            BillingAddress = new JMAAddress();
            ShippingAddress = new JMAAddress();
            PaymentGatewayNames = new List<string>();
            Tags = new List<string>();
        }

        public string AffiliateName { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BalanceDue { get; set; }
        public JMAAddress BillingAddress { get; set; }
        public string BillToAccount { get; set; }
        public string CardType { get; set; }
        public string Class { get; set; }
        public string CheckNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreditCardName { get; set; }
        public string CustomerTaxCode { get; set; }
        public string CurrencyCode { get; set; }
        [XmlIgnore]
        public List<JMACustomField> CustomFields { get; set; }
        public JMACustomer Customer { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        public bool DidExport { get; set; }
        public decimal DiscountExclTax { get; set; }
        public decimal DiscountInclTax { get; set; }
        public bool DoNotImport { get; set; }
        public bool DoNotExport { get; set; }
        public List<JMADiscount> Discounts = new List<JMADiscount>();
        public DateTime? DueDate { get; set; }
        public decimal ExchangeRate { get; set; }
        public string FOB { get; set; }
        public string FulFillmentChannel { get; set; }
        public string GiftMessage { get; set; }
        public int Id { get; set; }
        public string CustomerNote { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceTemplate { get; set; }
        public bool IsTaxInclusive { get; set; }
        public string LinkToTxnID { get; set; }
        public List<JMACustomField> JMACustomFields { get; set; }
        public string MerchantFeeItemName { get; set; }
        public double MerchantPercent { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Note { get; set; }
        public string OrderAccountsReceivableAccount { get; set; }
        public string OrderDefaultExpenseAccount { get; set; }
        public string OrderDiscountClassRef { get; set; }
        public string OrderDepositAccount { get; set; }
        public List<JMAOrderDetail> OrderDetails { get; set; }
        public Guid OrderGuid { get; set; }
        public string OrderNumber { get; set; }
        public string OrderShippingMethodClassRef { get; set; }
        public string OrderStatus { get; set; }
        public string Other { get; set; }
        public DateTime? PaidDate { get; set; }
        public List<string> PaymentGatewayNames { get; set; }

        /// <summary>
        /// If the order has multiple refunds, this is the order number of the original sale.
        /// This number is used to find the original sale in QuickBooks.
        /// </summary>
        public string ParentOrderNumber { get; set; }

        public decimal PartialRefund { get; set; }
        public decimal PaymentMethodAdditionalFeeExclTax { get; set; }
        public int PaymentTerms { get; set; }
        public string PriceLevel { get; set; }
        public string Project { get; set; }
        public string PONumber { get; set; }
        public string PrivateNote { get; set; }
        public string QuickBooksCustomerFullName { get; set; }
        public string QuickBooksId { get; set; }
        public string QuickBooksPaymentIds { get; set; }
        public string ReferenceNumber { get; set; }
        public string RegisterName { get; set; }
        public string SalesOrderNumber { get; set; }
        public string SalesTerms { get; set; }
        public string SalesRep { get; set; }
        public string SalesTaxName { get; set; }
        public JMAAddress ShippingAddress { get; set; }
        public DateTime? ShippingDate { get; set; }
        public decimal ShippingExclTax { get; set; }
        public string ShippingMethod { get; set; }
        public string ShippingMethodDescription { get; set; }
        public string ShipmentBatchNumber { get; set; }
        public string ShipVia { get; set; }
        public decimal ShippingInclTax { get; set; }
        /// <summary>
        /// Actual name of the store, like Joe's Amazon Store
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// Type of store, like Amazon or eBay
        /// </summary>
        public string StoreType { get; set; }
        public string Source { get; set; }

        /// <summary>
        /// If the order is split because it is a split shipment, this is the order number of the parent sale.
        /// </summary>
        public string SplitShipmentParentOrderNumber { get; set; }

        public List<string> Tags { get; set; }
        public decimal TaxRate { get; set; }
        public int TaxRateID { get; set; }
        public bool? ToBePrinted { get; set; }
        public decimal Total { get; set; }
        public decimal TotalExclTax { get; set; }
        public decimal TotalInclTax { get; set; }
        public decimal TotalShippingCosts { get; set; }
        public decimal TotalTax { get; set; }
        public string TrackingNumber { get; set; }
        public string Vendor { get; set; }
        public string WarehouseName { get; set; }

        public decimal GetBalanceDue()
        {
            if (BalanceDue == 0)
                return Total - AmountPaid;
            else
                return BalanceDue;
        }

        [XmlIgnore]
        public string GetCustomFieldsForHtml
        {
            get
            {
                string fields = string.Empty;

                foreach (JMACustomField field in JMACustomFields)
                {
                    fields += String.Format("Field: {0} Value: {1} {2}", field.Name, field.Value, Environment.NewLine);
                }

                return fields;
            }
        }

        [XmlIgnore]
        public string GetTagsForHtml
        {
            get
            {
                return string.Join(",", Tags);
            }
        }


        public string Cashier { get; set; }

        public string PackageCode { get; set; }

        public string QuickBooksTxnId { get; set; }
        public decimal TotalMerchantFees { get; set; }
        public decimal MerchantFee { get; set; }
        public bool RemoveCountry { get; set; }
        public string CustomerCurrencyCode { get; set; }
        public decimal TotalCustomerCurrency { get; set; }
        public decimal TotalRefund { get; set; }
        public string ShippingTaxCode { get; set; }
        public decimal ShippingExclTaxCustomerCurrency { get; set; }
        public decimal ShippingInclTaxCustomerCurrency { get; set; }
        public string BuyerUserId { get; set; }
        public bool DoNotImportInvoiceFromSalesOrder { get; set; }
        public bool IsExpense { get; set; }
        public string PurchaseAssetAccount { get; set; }
        public decimal AmountPaidInCustomerCurrency { get; set; }
        public decimal TaxAmountRemitted { get; set; }

        /// <summary>
        /// Using rules engine, you can decide whether this syncs as a PO, bill, or both types.
        /// </summary>
        public JMAPurchaseType PurchaseType { get; set; }
        public DateTime QuickBooksTimeCreated { get; set; }
        public DateTime QuickBooksTimeModified { get; set; }
        /// <summary>
        /// The user can map an order to a sales receipt or invoice, for example, based on some criteria. Many users want unpaid invoices and sales receipts.
        /// When syncing from QuickBooks, this enum determine what kind of order came from QuickBooks.
        /// </summary>
        public InvoiceModeEnum InvoiceMode { get; set; }
        public decimal CustomerExchangeRate { get; set; }
        public string SalesRepId { get; set; }
        public bool IsDraft { get; set; }
        public string PayoutId { get; set; }
    }
}
