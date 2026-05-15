using KataCheckout.Entities.Cart;
using KataCheckout.Logic.Tests.UtilitiesTests.CartUtilityTests.TestData;
using KataCheckout.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.CartUtilityTests
{
    /// <summary>
    /// The cart utility tests.
    /// </summary>
    [TestFixture]
    public class GetUniqueLineItemsTests
    {
        [TestCaseSource(typeof(UniqueLineItemTestData))]
        public void CondensesLineItemsCorrectly(IEnumerable<CartLineItem> lineItems, Dictionary<string, CartLineItem> expectedResults)
        {
            this.ExecuteTest(lineItems, expectedResults);
        }

        private void ExecuteTest(IEnumerable<CartLineItem> lineItems, Dictionary<string, CartLineItem> expectedResults)
        {
            Dictionary<string, CartLineItem> results = CartUtility.GetUniqueLineItems(lineItems, out string? errorMessage);

            // check nothing went wrong.
            Assert.That(string.IsNullOrEmpty(errorMessage), Is.True, $"Error message returned: {errorMessage}");

            // check same number of results.
            Assert.That(results.Count, Is.EqualTo(expectedResults.Count), "Unexpected number of results returned");

            foreach (string sku in results.Keys)
            {
                CartLineItem result = results[sku];

                // check can find the result.
                Assert.That(expectedResults.ContainsKey(sku), Is.True, $"Unexpected sku returned: {sku}");

                CartLineItem expectedResult = expectedResults[sku];

                // check results match.
                Assert.That(result.Product.SKU, Is.EqualTo(expectedResult.Product.SKU), "Different sku returned");
                Assert.That(result.NumUnits, Is.EqualTo(expectedResult.NumUnits), "Different number of units returned");
            }
        }
    }
}
