using KataCheckout.Entities.Offers;
using KataCheckout.Entities.Products;
using System;
using System.Collections.Generic;
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
                        // check sku is set.
                        if (!string.IsNullOrEmpty(product.SKU))
                        {
                            bool found = false;

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
                            }
                        }
                    }
                }
            }

            return applicableOffers;
        }
    }
}
