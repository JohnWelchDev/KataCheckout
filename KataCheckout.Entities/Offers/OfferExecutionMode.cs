using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Offers
{
    /// <summary>
    /// The offer execution modes.
    /// </summary>
    public enum OfferExecutionMode : byte
    {
        /// <summary>
        /// Discounts a flat amount from the cart total.
        /// </summary>
        FlatTotalDiscount = 0,

        /// <summary>
        /// Discounts a percentage of the cart total.
        /// </summary>
        FlatPercentageDiscount = 1,

        /// <summary>
        /// Overrides the cost for the group of products covered by the offer.
        /// </summary>
        CostOverrideForProductsTotal = 2,

        /// <summary>
        /// Applies flat discount to cheapest product covered by offer.
        /// </summary>
        CheapestProductFlatDiscount = 3,

        /// <summary>
        /// Applies percentage discount to cheapest product covered by offer.
        /// </summary>
        CheapestProductPercentageDiscount = 4,

        /// <summary>
        /// Applies flat discount to most expensive product covered by offer.
        /// </summary>
        ExpensiveProductFlatDiscount = 5,

        /// <summary>
        /// Applies percentage to most expensive product covered by offer.
        /// </summary>
        ExpensiveProductPercentageDiscount = 6
    }
}
