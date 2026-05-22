using KataCheckout.Entities.Offers;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Tests.OfferTests.TestData
{
    /// <summary>
    /// The special offer sku extraction test data.
    /// </summary>
    public class SpecialOfferExtractSkusTestData : IEnumerable
    {
        /// <summary>
        /// Gets enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            yield return this.GenerateSingleLevelOfferTest(["A"], "Single SKU");
            yield return this.GenerateSingleLevelOfferTest(["A", "B"], "Two SKUs");
            yield return this.GenerateSingleLevelOfferTest(["A", "B", "C", "D"], "Multiple SKUs");
            yield return this.GenerateSingleLevelOfferTest(["A", "A", "A", "A"], "Multiple Same SKUs");
            yield return this.GenerateSingleLevelOfferTest(["A", "B", "A", "C", "C", "D"], "Multiple with Repeated SKUs");
        }

        /// <summary>
        /// Generates test case based on single level data.
        /// </summary>
        /// <param name="skus">The skus.</param>
        /// <param name="testName">The test name.</param>
        /// <returns></returns>
        public TestCaseData GenerateSingleLevelOfferTest(IEnumerable<string> skus, string testName)
        {
            SpecialOffer offer = this.GenerateSingleLevelOffer(skus);

            // work out unique skus for expected results.
            HashSet<string> uniqueSkus = new HashSet<string>();

            foreach (string sku in skus)
            {
                uniqueSkus.Add(sku);
            }

            TestCaseData testData = new TestCaseData(offer, uniqueSkus).SetName(testName);

            return testData;
        }

        /// <summary>
        /// Generates single level offer.
        /// </summary>
        /// <param name="skus">The skus.</param>
        /// <returns>The offer.</returns>
        private SpecialOffer GenerateSingleLevelOffer(IEnumerable<string> skus)
        {
            SpecialOffer offer = new SpecialOffer();

            OfferCondition condition = new OfferCondition();

            // loop through skus creating condition for each.
            foreach (string sku in skus)
            {
                ProductConditionUnit conditionUnit = new ProductConditionUnit();
                conditionUnit.SKU = sku;
                conditionUnit.Operator = OperatorCodes.EqualTo;
                conditionUnit.NumUnits = 2;

                offer.Condition.UnitConditions.Add(conditionUnit);
            }

            // add to offer.
            //offer.Conditions.Add(condition);

            return offer;
        }
    }
}
