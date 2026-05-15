using KataCheckout.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Offers
{
    /// <summary>
    /// The offer condition.
    /// </summary>
    public class OfferCondition : ISkuExtractable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfferCondition" /> class.
        /// </summary>
        public OfferCondition()
        {
            this.UnitConditions = new List<ProductConditionUnit>();
            this.ChildOfferConditions = new List<OfferCondition>();
            this.RelationCode = RelationCodes.AND;
        }

        /// <summary>
        /// Gets or sets the units.
        /// </summary>
        public List<ProductConditionUnit> UnitConditions { get; init; }

        /// <summary>
        /// Gets or sets the offer condition.
        /// </summary>
        public List<OfferCondition> ChildOfferConditions { get; init; }

        /// <summary>
        /// Gets or sets the code defining relationship between the units
        /// that forms the evaluation (&& = all unit conditions must be met, || = 1 condition must be met).
        /// </summary>
        public string RelationCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to invert the evaluation result (basically a "NOT" statement).
        /// </summary>
        public bool InvertEvaluation { get; set; }

        /// <summary>
        /// Extracts product skus.
        /// </summary>
        /// <returns>The product skus.</returns>
        public IEnumerable<string> ExtractSkus()
        {
            HashSet<string> result = new HashSet<string>();

            // check unit conditions populated.
            if (this.UnitConditions != null && this.UnitConditions.Count > 0)
            {
                // extract skus from each unit condition.
                this.UnitConditions.ForEach(x =>
                {
                    // extract skus from condition.
                    IEnumerable<string> skus = x.ExtractSkus();

                    // chekc skus populated.
                    if (skus != null)
                    {
                        // apparently IEnumerable doesn't have a foreach linq extension.
                        foreach (string sku in skus)
                        {
                            // add to results.
                            result.Add(sku);
                        }
                    }
                });
            }

            // check the child offer conditions populated..
            if (this.ChildOfferConditions != null && this.ChildOfferConditions.Count > 0)
            {
                // extract skus from each unit condition.
                this.ChildOfferConditions.ForEach(x =>
                {
                    // extract skus from condition.
                    IEnumerable<string> skus = x.ExtractSkus();

                    // chekc skus populated.
                    if (skus != null)
                    {
                        // apparently IEnumerable doesn't have a foreach linq extension.
                        foreach (string sku in skus)
                        {
                            // add to results.
                            result.Add(sku);
                        }
                    }
                });
            }

            return result;
        }
    }
}
