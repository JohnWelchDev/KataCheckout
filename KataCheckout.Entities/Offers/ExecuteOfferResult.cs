using KataCheckout.Entities.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Offers
{
    /// <summary>
    /// The execute offer result.
    /// </summary>
    public class ExecuteOfferResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteOfferResult" /> class.
        /// </summary>
        public ExecuteOfferResult()
        {
            this.AffectedLineItems = new Dictionary<string, CartLineItem>();
            this.UnaffectedLineItems = new Dictionary<string, CartLineItem>();
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the discount amount applied.
        /// </summary>
        public decimal DiscountAmountApplied { get; set; }

        /// <summary>
        /// Gets or sets the number of times applied.
        /// </summary>
        public int NumTimesApplied { get; set; }

        /// <summary>
        /// Gets or sets the discount log.
        /// </summary>
        public string? DiscountLog { get; set; }

        /// <summary>
        /// Gets or sets the affected line items.
        /// </summary>
        public Dictionary<string, CartLineItem> AffectedLineItems { get; init; }

        /// <summary>
        /// Gets or sets the unaffected line items.
        /// </summary>
        public Dictionary<string, CartLineItem> UnaffectedLineItems { get; init; }

        /// <summary>
        /// Fills the affected items.
        /// </summary>
        /// <param name="items">The items.</param>
        public void FillAffectedItems(IEnumerable<CartLineItem> items)
        {
            // check items passed in.
            if (items != null)
            {
                // clear any existing items.
                this.AffectedLineItems.Clear();

                // loop through items.
                foreach (CartLineItem item in items)
                {
                    // check the product populated.
                    if (item.Product != null && !string.IsNullOrEmpty(item.Product.SKU))
                    {
                        // check the sku not already in affected items.
                        if (this.AffectedLineItems.ContainsKey(item.Product.SKU))
                        {
                            // get existing entry.
                            CartLineItem existing = this.AffectedLineItems[item.Product.SKU];

                            // merge the unit numbers.
                            existing.NumUnits += item.NumUnits;
                        }
                        else
                        {
                            // create new entry.
                            CartLineItem entry = item.Clone();

                            // add item to dictionary.
                            this.AffectedLineItems.Add(item.Product.SKU, entry);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Fills the unaffected items.
        /// </summary>
        /// <param name="items">The items.</param>
        public void FillUnaffectedItems(IEnumerable<CartLineItem> items)
        {
            // check items passed in.
            if (items != null)
            {
                // clear any existing items.
                this.UnaffectedLineItems.Clear();

                // loop through items.
                foreach (CartLineItem item in items)
                {
                    // check the product populated.
                    if (item.Product != null && !string.IsNullOrEmpty(item.Product.SKU))
                    {
                        // check the sku not already in affected items.
                        if (this.UnaffectedLineItems.ContainsKey(item.Product.SKU))
                        {
                            // get existing entry.
                            CartLineItem existing = this.UnaffectedLineItems[item.Product.SKU];

                            // merge the unit numbers.
                            existing.NumUnits += item.NumUnits;
                        }
                        else
                        {
                            // create new entry.
                            CartLineItem entry = item.Clone();

                            // add item to dictionary.
                            this.UnaffectedLineItems.Add(item.Product.SKU, entry);
                        }
                    }
                }
            }
        }
    }
}
