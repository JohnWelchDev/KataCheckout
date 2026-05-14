using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Offers
{
    /// <summary>
    /// The unit that makes up part of a special offer condition.
    /// </summary>
    public class ProductConditionUnit
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
    }
}
