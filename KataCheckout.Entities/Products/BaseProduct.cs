using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Products
{
    /// <summary>
    /// The product.
    /// </summary>
    public abstract class BaseProduct
    {
        /// <summary>
        /// Gets or sets the sku.
        /// </summary>
        public string? SKU { get; set; }

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}
