using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Offers
{
    /// <summary>
    /// Rule defining how an offer is applied.
    /// </summary>
    public class OfferExecutionRule
    {
        /// <summary>
        /// Gets or sets the product sku.
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// Gets or sets the number of units to ringfence for the offer.
        /// null if apply to all products matching sku in list.
        /// </summary>
        public int? NumUnitLimit { get; set; }
    }
}
