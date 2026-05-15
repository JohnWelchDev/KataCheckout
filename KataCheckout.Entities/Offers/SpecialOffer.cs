using KataCheckout.Entities.Products;
using KataCheckout.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Offers
{
    /// <summary>
    /// The special offer.
    /// </summary>
    public class SpecialOffer : ISkuExtractable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialOffer" /> class.
        /// </summary>
        public SpecialOffer()
        {
            //this.Conditions = new List<OfferCondition>();
            this.Condition = new OfferCondition();
        }

        /// <summary>
        /// Gets or sets the special offer identifier.
        /// </summary>
        public int SpecialOfferID { get; set; }

        /// <summary>
        /// Gets or sets the condition to be met for the offer to apply.
        /// </summary>
        public OfferCondition Condition { get; init; }

        /// <summary>
        /// Extracts product skus.
        /// </summary>
        /// <returns>The product skus.</returns>
        public IEnumerable<string> ExtractSkus()
        {
            HashSet<string> results = new HashSet<string>();

            // get skus from condition.
            IEnumerable<string> conditionSkus = this.Condition.ExtractSkus();

            // check skus returned.
            if (conditionSkus != null)
            {
                // loop through returned skus.
                foreach (string sku in conditionSkus)
                {
                    results.Add(sku);
                }
            }

            return results;
        }
    }
}
