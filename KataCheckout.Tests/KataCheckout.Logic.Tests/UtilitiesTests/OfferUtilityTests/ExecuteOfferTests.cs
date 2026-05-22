using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData.ExecuteOfferTestData;
using KataCheckout.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests
{
    /// <summary>
    /// The execute offer tests.
    /// </summary>
    public class ExecuteOfferTests
    {
        [TestCaseSource(typeof(ExecuteOfferSingleProductTestData))]
        public void OfferExecutesCorrectlyForSingleProductType(Dictionary<string, CartLineItem> cartLines, SpecialOffer offer, decimal expectedAmount, string expectedLog,
            Dictionary<string, CartLineItem> expectedAffectedProducts, Dictionary<string, CartLineItem> expectedUnaffectedProducts)
        {
            this.ExecuteTest(cartLines, offer, expectedAmount, expectedLog, expectedAffectedProducts, expectedUnaffectedProducts);
        }

        [TestCaseSource(typeof(ExecuteOfferMultiProductTestData))]
        public void OfferExecutesCorrectlyForMultipleProductTypes(Dictionary<string, CartLineItem> cartLines, SpecialOffer offer, decimal expectedAmount, string expectedLog,
            Dictionary<string, CartLineItem> expectedAffectedProducts, Dictionary<string, CartLineItem> expectedUnaffectedProducts)
        {
            this.ExecuteTest(cartLines, offer, expectedAmount, expectedLog, expectedAffectedProducts, expectedUnaffectedProducts);
        }

        public void ExecuteTest(Dictionary<string, CartLineItem> cartLines, SpecialOffer offer, decimal expectedAmount, string expectedLog,
            Dictionary<string, CartLineItem> expectedAffectedProducts, Dictionary<string, CartLineItem> expectedUnaffectedProducts)
        {
            ExecuteOfferResult result = OfferUtility.ExecuteOffer(cartLines, offer);

            Assert.That(result, Is.Not.Null, "No result returned");

            Assert.That(result.DiscountAmountApplied, Is.EqualTo(expectedAmount), "Unexpected amount returned");

            //Assert.That(result.DiscountLog, Is.EqualTo(expectedLog), "Unexpected log value returned");

            // check affected items.
            Assert.That(result.AffectedLineItems, Is.Not.Null, "No affected lines returned");
            Assert.That(result.AffectedLineItems.Count, Is.EqualTo(expectedAffectedProducts.Count), "Unexpected number of affected items");

            // loop through affected line items.
            foreach (string key in expectedAffectedProducts.Keys)
            {
                Assert.That(result.AffectedLineItems.ContainsKey(key), Is.True, $"key \"{key}\" not found in result affected line items");
                Assert.That(result.AffectedLineItems[key].NumUnits, Is.EqualTo(expectedAffectedProducts[key].NumUnits), $"unexpected number of units for sku \"{key}\"");
            }

            // check unaffected items.
            Assert.That(result.UnaffectedLineItems, Is.Not.Null, "No unaffected lines returned");
            Assert.That(result.UnaffectedLineItems.Count, Is.EqualTo(expectedUnaffectedProducts.Count), "Unexpected number of unaffected items");

            // loop through affected line items.
            foreach (string key in expectedUnaffectedProducts.Keys)
            {
                Assert.That(result.UnaffectedLineItems.ContainsKey(key), Is.True, $"key \"{key}\" not found in result unaffected line items");
                Assert.That(result.UnaffectedLineItems[key].NumUnits, Is.EqualTo(expectedUnaffectedProducts[key].NumUnits), $"unexpected number of units for sku \"{key}\"");
            }
        }
    }
}
