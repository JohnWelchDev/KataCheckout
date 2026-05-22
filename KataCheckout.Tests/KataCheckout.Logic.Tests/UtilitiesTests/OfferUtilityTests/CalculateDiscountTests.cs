using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData.CalculateDiscountTestData;
using KataCheckout.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests
{
    [TestFixture]
    public class CalculateDiscountTests
    {
        [TestCaseSource(typeof(CalculateDiscountTestData))]
        public void CalculatesDiscountCorrectly(Dictionary<string, CartLineItem> cartLineItems, SpecialOffer offer, decimal expectedResult, string? expectedLog)
        {
            this.ExecuteTest(cartLineItems, offer, expectedResult, expectedLog);
        }

        /// <summary>
        /// Executes test.
        /// </summary>
        /// <param name="cartLineItems">The cart line items.</param>
        /// <param name="offer">The offer.</param>
        /// <param name="expectedResult">The expected result.</param>
        /// <param name="expectedLog">The expected log.</param>
        public void ExecuteTest(Dictionary<string, CartLineItem> cartLineItems, SpecialOffer offer, decimal expectedResult, string? expectedLog)
        {
            // calculate discount.
            decimal discountAmount = OfferUtility.CalculateDiscount(cartLineItems, offer, out string? log);

            // check discount amount.
            Assert.That(discountAmount, Is.EqualTo(expectedResult), "Unexpected discount amount returned");

            /*
            // check if a log is expected.
            if (expectedLog == null)
            {
                Assert.That(log, Is.Null, "log was expected to be null");
            }
            else
            {
                Assert.That(log, Is.EqualTo(expectedLog), "Unexpected log value returned");
            }
            */
        }
    }
}
