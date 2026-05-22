using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Cart
{
    /// <summary>
    /// The cart calculation discount line.
    /// </summary>
    public class CartCalculationDiscountLine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartCalculationDiscountLine" /> class.
        /// </summary>
        public CartCalculationDiscountLine()
        {
            this.LineItemsAffected = new List<CartLineItem>();
        }

        /// <summary>
        /// Gets or sets the special offer identifier.
        /// </summary>
        public int SpecialOfferID { get; set; }

        /// <summary>
        /// Gets or sets the discount amount.
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// Gets the line items affected by discount.
        /// </summary>
        public List<CartLineItem> LineItemsAffected { get; init; }
    }
}
