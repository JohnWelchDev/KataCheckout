using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Tests.OfferTests.TestData
{
    /// <summary>
    /// The sku nest.
    /// </summary>
    public class SkuNest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SkuNest" /> class.
        /// </summary>
        public SkuNest()
        {
            this.SKUs = new List<string>();
            this.ChildLevels = new List<SkuNest>();
        }

        /// <summary>
        /// Gets or sets the skus.
        /// </summary>
        public List<string> SKUs { get; init; }

        /// <summary>
        /// Gets or sets the child levels.
        /// </summary>
        public List<SkuNest> ChildLevels { get; init; }
    }
}
