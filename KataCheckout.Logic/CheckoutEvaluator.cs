using KataCheckout.Entities;
using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic
{
    /// <summary>
    /// The checkout evaluator.
    /// </summary>
    public class CheckoutEvaluator
    {
        /// <summary>
        /// The special offers.
        /// </summary>
        private List<SpecialOffer> specialOffers;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutEvaluator" /> class.
        /// </summary>
        /// <param name="specialOffers">The special offers.</param>
        public CheckoutEvaluator(IEnumerable<SpecialOffer> specialOffers)
        {
            this.specialOffers = new List<SpecialOffer>();

            // check object passed in.
            if (specialOffers != null)
            {
                // fill thje special offers collection for evaluator.
                this.specialOffers.AddRange(specialOffers);
            }
        }

        public CartCalculationResponse CalculateTotal(IEnumerable<CartLineItem> lineItems)
        {
            CartCalculationResponse response = new CartCalculationResponse();

            // check line itewms passed in.
            // note: could have added check for collection length that line items in
            // collection had units set, but chose to evaluate reference being set and
            // allow empty list to be a valid input that evaluates to 0.
            if (lineItems != null)
            {
                // create dictionary to track unique skus and their quantities.
                Dictionary<string, CartLineItem> dicLineItems = this.GetUniqueLineItems(lineItems, out string? uniqueLineItemErrorMessage);

                // check the unique items returned successfully.
                if (string.IsNullOrWhiteSpace(uniqueLineItemErrorMessage))
                {

                }
                else
                {
                    // set response error message.
                    response.ErrorMessage = uniqueLineItemErrorMessage;
                }
            }
            else
            {
                // no line items passed in.
                response.ErrorMessage = "No line items detected";
            }
        }

        private Dictionary<string, CartLineItem> GetUniqueLineItems(IEnumerable<CartLineItem> lineItems, out string? errorMessage)
        {
            errorMessage = null;
            Dictionary<string, CartLineItem> dicLineItems = new Dictionary<string, CartLineItem>();

            // process line items into dictionary. setup is intended to be one sku per line,
            // however can't control how it is passed in.
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
