using KataCheckout.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Offers
{
    /// <summary>
    /// The unit that makes up part of a special offer condition.
    /// </summary>
    public class ProductConditionUnit : ISkuExtractable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductConditionUnit" /> class.
        /// </summary>
        public ProductConditionUnit()
        {
            // set defaults.
            this.SKU = string.Empty;
            this.Operator = "=";
        }

        /// <summary>
        /// Gets or sets the product sku.
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Gets or sets the number of units the operator must satisfy.
        /// </summary>
        public int NumUnits { get; set; }

        /// <summary>
        /// Extracts product skus.
        /// </summary>
        /// <returns>The product skus.</returns>
        public IEnumerable<string> ExtractSkus()
        {
            if (!string.IsNullOrWhiteSpace(this.SKU))
            {
                return [this.SKU];
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }
    }
}
