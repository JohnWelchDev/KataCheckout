using KataCheckout.Entities.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Utilities
{
    /// <summary>
    /// The cart utility.
    /// </summary>
    public class CartUtility
    {
        /// <summary>
        /// Gets unique collection of line items grouped by product.
        /// </summary>
        /// <param name="lineItems">The line items.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The unique product line items.</returns>
        public static Dictionary<string, CartLineItem> GetUniqueLineItems(IEnumerable<CartLineItem> lineItems, out string? errorMessage)
        {
            errorMessage = null;
            Dictionary<string, CartLineItem> dicLineItems = new Dictionary<string, CartLineItem>();

            // loop through items.
            foreach (CartLineItem item in lineItems)
            {
                // check the item has a product and a valid sku.
                if (item.Product == null || string.IsNullOrWhiteSpace(item.Product.SKU))
                {
                    // unable to identify product.
                    errorMessage = "Invalid line item detected";

                    // stop processing.
                    break;
                }

                CartLineItem uniqueItem;

                // check if the product already has an item in collection.
                if (dicLineItems.ContainsKey(item.Product.SKU))
                {
                    // get existing entry.
                    uniqueItem = dicLineItems[item.Product.SKU];

                    // merge line item into existing record.
                    uniqueItem.Merge(item);
                }
                else
                {
                    // create new entry.
                    uniqueItem = item.Clone();

                    // add to dictionary.
                    dicLineItems.Add(item.Product.SKU, uniqueItem);
                }
            }

            return dicLineItems;
        }
    }
}
