using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Entities.Products;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace KataCheckout.Logic.Utilities
{
    /// <summary>
    /// The offer utility.
    /// </summary>
    public static class OfferUtility
    {
        /// <summary>
        /// Extacts offers that could apply to the list of specified products.
        /// </summary>
        /// <param name="offers">The offers.</param>
        /// <param name="products">The products.</param>
        /// <returns>The applicable offers based on products.</returns>
        public static List<SpecialOffer> GetApplicableOffers(IEnumerable<SpecialOffer> offers, IEnumerable<BaseProduct> products)
        {
            List<SpecialOffer> applicableOffers = new List<SpecialOffer>();

            // check the offers and products passed in.
            if (offers != null && products != null)
            {
                // loop through offers.
                foreach (SpecialOffer offer in offers)
                {
                    // get the skus from the offer.
                    IEnumerable<string> skus = offer.ExtractSkus();

                    // loop through products.
                    foreach (BaseProduct product in products)
                    {
                        // track whether product has been found in the offer.
                        bool found = false;

                        // check sku is set.
                        if (!string.IsNullOrEmpty(product.SKU))
                        {
                            // loop through sku list.
                            foreach(string sku in skus)
                            {
                                // check the sku match.
                                if (product.SKU.Equals(sku, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    // sku matched, offer can be applicable.
                                    found = true;

                                    break;
                                }
                            }

                            // check if product was found.
                            if (found)
                            {
                                // add offer to applicable list.
                                applicableOffers.Add(offer);

                                // stop searching products, one is enough to make the
                                // offer potentially a match for the whole checkout.
                                break;
                            }
                        }
                    }
                }
            }

            return applicableOffers;
        }

        public static bool OfferApplies(Dictionary<string, CartLineItem> dicUniqueCartItems, SpecialOffer offer)
        {
            bool applies = false;

            // check offer passed in.
            if (dicUniqueCartItems != null && offer != null)
            {
                // check the condition is set.
                if (offer.Condition != null)
                {
                    applies = EvaluateCondition(offer.Condition, dicUniqueCartItems);
                }
                else
                {
                    // if no condition, always applies.
                    applies = true;
                }
            }

            return applies;
        }

        public static bool EvaluateCondition(OfferCondition condition, Dictionary<string, CartLineItem> cartLines)
        {
            bool match = false;

            // check unit conditions set.
            if (condition.UnitConditions != null && condition.UnitConditions.Count > 0)
            {
                // check if all units must evaluate true in order for condition to pass.
                bool allRequired = condition.RelationCode == "&&";

                // loop through the condition units.
                foreach (ProductConditionUnit unit in condition.UnitConditions)
                {
                    // check the sku is set.
                    if (!string.IsNullOrEmpty(unit.SKU))
                    {
                        // check cart line exists for product.
                        if (cartLines.ContainsKey(unit.SKU))
                        {
                            // get the cart line item.
                            CartLineItem cartLineItem = cartLines[unit.SKU];
                        }
                        else if (allRequired)
                        {
                            // no cart line found for product.
                            break;
                        }
                    }
                    else if (allRequired)
                    {
                        // unable to match condition as can't identify product.
                        // all required to pass so evaluate condition fails.
                        break;
                    }
                }
            }

            return match;
        }
    }
}
