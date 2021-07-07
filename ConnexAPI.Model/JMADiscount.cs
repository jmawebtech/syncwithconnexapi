using System;

namespace ConnexForQuickBooks.Model
{
    [Serializable]
    public class JMADiscount
    {
        /// <summary>
        /// If all coupons are mapped to a single discount code, the name field shows the code prior to mapping.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Amount of discount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Coupon code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Whether there is tax on the discount. If the order has no tax or if the order is paid by gift card, then the discount is non-taxable.
        /// </summary>
        public bool NonTaxable { get; set; }

        /// <summary>
        /// If the customer paid in USD and the store uses EUR, then show the discount in USD.
        /// Allows Connex to change the currency of the sale without using an exchange rate.
        /// </summary>
        public decimal AmountInCustomerCurrency { get; set; }

        /// <summary>
        /// The tax code in QuickBooks, which is usually TAX or NON.
        /// </summary>
        public string TaxCode { get; set; }
    }
}
