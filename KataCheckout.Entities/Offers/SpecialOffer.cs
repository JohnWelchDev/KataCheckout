using KataCheckout.Entities.Products;
using KataCheckout.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KataCheckout.Entities.Offers
{
    /// <summary>
    /// The special offer.
    /// </summary>
    public class SpecialOffer : ISkuExtractable
    {
        /// <summary>
        /// Gets or sets the execution rules.
        /// </summary>
        private List<OfferExecutionRule> executionRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialOffer" /> class.
        /// </summary>
        public SpecialOffer()
        {
            //this.Conditions = new List<OfferCondition>();
            this.Condition = new OfferCondition();
            this.executionRules = new List<OfferExecutionRule>();
        }

        /// <summary>
        /// Gets or sets the special offer identifier.
        /// </summary>
        public int SpecialOfferID { get; set; }

        /// <summary>
        /// Gets or sets the offer mode.
        /// </summary>
        public OfferExecutionMode OfferMode { get; set; }

        /// <summary>
        /// Gets or sets the condition to be met for the offer to apply.
        /// </summary>
        public OfferCondition Condition { get; init; }

        /// <summary>
        /// Gets the execution rules.
        /// </summary>
        public IEnumerable<OfferExecutionRule> ExecutionRules { get { return this.executionRules; } private set { this.executionRules = new List<OfferExecutionRule>(value); } }

        /// <summary>
        /// Gets or sets the number of times an offer can apply within a single checkout (null for unlimited).
        /// </summary>
        public int? LimitPerCheckout { get; set; }

        /// <summary>
        /// Gets or sets the discount amount.
        /// </summary>
        public decimal DiscountAmount { get; set; }

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

        /// <summary>
        /// Add execution rule to offer.
        /// </summary>
        /// <param name="sku">The sku.</param>
        /// <param name="numUnitLimit">The number of units limit.</param>
        public void AddExecutionRule(string sku, int numUnitLimit)
        {
            OfferExecutionRule rule = new OfferExecutionRule();
            rule.SKU = sku;
            rule.NumUnitLimit = numUnitLimit;

            this.AddExecutionRule(rule);
        }

        /// <summary>
        /// Adds execution rule to offer.
        /// </summary>
        /// <param name="rule">The rule.</param>
        public void AddExecutionRule(OfferExecutionRule rule)
        {
            if (rule != null)
            {
                this.executionRules.Add(rule);
            }
        }
    }
}
