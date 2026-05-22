using KataCheckout.Entities.Offers;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Tests.Common.Products
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

        /// <summary>
        /// Fill condition data.
        /// </summary>
        /// <param name="condition">The condition.</param>
        public void FillCondition(OfferCondition condition)
        {
            this.SKUs.ForEach(x =>
            {
                condition.UnitConditions.Add(new ProductConditionUnit { SKU = x, Operator = OperatorCodes.EqualTo, NumUnits = 2 });
            });

            // check child levels present.
            if (this.ChildLevels != null && this.ChildLevels.Count > 0)
            {
                foreach (SkuNest childLevel in this.ChildLevels)
                {
                    OfferCondition childCondition = new OfferCondition();

                    // populate the child condition.
                    childLevel.FillCondition(childCondition);

                    // add child condition to parent.
                    condition.ChildOfferConditions.Add(childCondition);
                }
            }
        }
    }
}
