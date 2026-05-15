using KataCheckout.Entities.Offers;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Tests.OfferTests.TestData
{
    /// <summary>
    /// The special offer multi level extract skus test data.
    /// </summary>
    public class SpecialOfferMultiLevelExtractSkusTestData : IEnumerable
    {
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            SkuNest twoLevelUnique = new SkuNest { SKUs = ["A", "B"], ChildLevels = [new SkuNest { SKUs = ["C"] }, new SkuNest { SKUs = ["D", "E"] }] };
            SkuNest twoLevelDupesOne = new SkuNest { SKUs = ["A", "A"], ChildLevels = [new SkuNest { SKUs = ["C"] }, new SkuNest { SKUs = ["D", "E"] }] };
            SkuNest twoLevelDupesTwo = new SkuNest { SKUs = ["A", "B"], ChildLevels = [new SkuNest { SKUs = ["C"] }, new SkuNest { SKUs = ["B", "E"] }] };
            SkuNest twoLevelDupesThree = new SkuNest { SKUs = ["A", "A"], ChildLevels = [new SkuNest { SKUs = ["A"] }, new SkuNest { SKUs = ["A", "A"] }] };

            yield return this.GenerateMultiLevelOfferTest(twoLevelUnique, ["A", "B", "C", "D", "E"], "Two levels, unique skus");
            yield return this.GenerateMultiLevelOfferTest(twoLevelDupesOne, ["A", "C", "D", "E"], "Two levels, duplicate sku same level");
            yield return this.GenerateMultiLevelOfferTest(twoLevelDupesTwo, ["A", "B", "C", "E"], "Two levels, duplicate sku across levels");
            yield return this.GenerateMultiLevelOfferTest(twoLevelDupesThree, ["A"], "Two levels, all duplicate skus");
        }

        /// <summary>
        /// Generates multi level offer test.
        /// </summary>
        /// <param name="skuNest">The sku nest.</param>
        /// <param name="expectedResults">The expected results.</param>
        /// <param name="testName">The test name.</param>
        /// <returns>The test case data.</returns>
        public TestCaseData GenerateMultiLevelOfferTest(SkuNest skuNest, IEnumerable<string> expectedResults, string testName)
        {
            SpecialOffer specialOffer = this.GenerateMultiLevelOffer(skuNest);

            TestCaseData testCase = new TestCaseData(specialOffer, expectedResults).SetName(testName);

            return testCase;
        }

        /// <summary>
        /// Generates multi level offer.
        /// </summary>
        /// <param name="skuNest">The sku nest.</param>
        /// <returns>The offer.</returns>
        private SpecialOffer GenerateMultiLevelOffer(SkuNest skuNest)
        {
            SpecialOffer specialOffer = new SpecialOffer();

            // addd skus to condition.
            this.FillCondition(specialOffer.Condition, skuNest);

            return specialOffer;
        }

        /// <summary>
        /// Fill condition data.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="skuNest">The sku tree.</param>
        private void FillCondition(OfferCondition condition, SkuNest skuNest)
        {
            skuNest.SKUs.ForEach(x =>
            {
                condition.UnitConditions.Add(new ProductConditionUnit { SKU = x, Operator = OperatorCodes.Equals, NumUnits = 2 });
            });

            // check child levels present.
            if (skuNest.ChildLevels != null && skuNest.ChildLevels.Count > 0)
            {
                foreach (SkuNest childLevel in skuNest.ChildLevels)
                {
                    OfferCondition childCondition = new OfferCondition();
                    
                    // populate the child condition.
                    this.FillCondition(childCondition, childLevel);

                    // add child condition to parent.
                    condition.ChildOfferConditions.Add(childCondition);
                }
            }
        }
    }
}
