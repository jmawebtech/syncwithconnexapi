using System.ComponentModel;

namespace ConnexForQuickBooks.Model.Enums
{
    public enum InvoiceModeEnum
    {
        [Description("None")]
        None,
        [Description("Do Not Sync Sales")]
        DoNotSyncSales,
        [Description("Sales Receipts")]
        SalesReceipts,
        [Description("Invoices and Payments If Paid")]
        Invoices,
        [Description("Invoices Only")]
        InvoicesNoPayments,
        [Description("Estimates")]
        Estimates,
        [Description("Summary Sales Receipt")]
        SummarySalesReceipt,
        [Description("Summary Invoice")]
        SummaryInvoice,
        [Description("Purchase Orders")]
        PurchaseOrders,
        [Description("Expense")]
        Expense,
        [Description("Credit Memo")]
        CreditMemo,
        [Description("Payments")]
        Payments,
        [Description("Bill")]
        Bill,
        [Description("Deposit")]
        Deposit,
        [Description("Sales Orders")]
        SalesOrders,
        [Description("Sales Orders No Invoice Payments Only")]
        SalesOrdersNoInvoicePaymentsOnly,
        [Description("Sales Orders, Invoices, and Payments If Paid")]
        SalesOrdersInvoices,
        [Description("Sales Orders Invoices PO")]
        SalesOrdersInvoicesPO,
        [Description("Summary Sales Order")]
        SummarySalesOrder,
        [Description("Purchase Orders Bills")]
        PurchaseOrdersBills,
        [Description("Work Orders")]
        WorkOrders,
        [Description("Journal Entries")]
        JournalEntries
    }
}
