using KataCheckout.Entities.Offers;
using KataCheckout.Entities.Products;
using KataCheckout.Tests.Common.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData
{
    /// <summary>
    /// Gets applicable offers data for single level offers.
    /// </summary>
    internal class GetApplicableSingleLevelOffersTestData : BaseGetApplicableOffersTestData, IEnumerable
    {
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            yield return this.GenerateTest(["A", "B", "C", "D"],
                [new SkuNest { SKUs = ["A"] }, new SkuNest { SKUs = ["B"] }, new SkuNest { SKUs = ["C"] }, new SkuNest { SKUs = ["D"] }], [0, 1, 2, 3],
                "Single Product Offers - all match");

            yield return this.GenerateTest(["A", "B", "C", "D"],
                [new SkuNest { SKUs = ["A"] }, new SkuNest { SKUs = ["B"] }, new SkuNest { SKUs = ["D"] }], [0, 1, 2, 3],
                "Single Product Offers - all offers, excess products");

            yield return this.GenerateTest(["A", "B", "D"],
                [new SkuNest { SKUs = ["A"] }, new SkuNest { SKUs = ["B"] }, new SkuNest { SKUs = ["C"] }, new SkuNest { SKUs = ["D"] }], [0, 1, 3],
                "Single Product Offers - not all offers");

            yield return this.GenerateTest(["A", "B", "C", "D"],
                [new SkuNest { SKUs = ["E"] }, new SkuNest { SKUs = ["F"] }, new SkuNest { SKUs = ["G"] }, new SkuNest { SKUs = ["H"] }], [],
                "Single Product Offers - no matches");
        }
    }
}
