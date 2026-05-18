using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData.OfferAppliesTestData;
using KataCheckout.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests
{
    [TestFixture]
    public class OfferAppliesTests
    {
        [TestCaseSource(typeof(EvaluateOfferAppliesSingleLevelConditionTestData))]
        [TestCaseSource(typeof(EvaluateOfferAppliesSingleLevelMultiConditionUnitTestData))]
        [TestCaseSource(typeof(EvaluateOfferAppliesMultiLevelConditionTestData))]
        [TestCaseSource(typeof(EvaluateOfferAppliesMultiLevelConditionInvertTestData))]
        [TestCaseSource(typeof(EvaluateOfferAppliesConditionInvertTestData))]
        public void EvaluatesOfferAppliesCorrectly(Dictionary<string, CartLineItem> cartLineItems, SpecialOffer offer, bool expectedResult)
        {
            this.ExecuteTest(cartLineItems, offer, expectedResult);
        }

        /// <summary>
        /// Executes test.
        /// </summary>
        /// <param name="cartLineItems">The cart line items.</param>
        /// <param name="offer">The offer.</param>
        /// <param name="expectedResult">The expected result.</param>
        public void ExecuteTest(Dictionary<string, CartLineItem> cartLineItems, SpecialOffer offer, bool expectedResult)
        {
            bool result = OfferUtility.OfferApplies(cartLineItems, offer);

            Assert.That(result, Is.EqualTo(expectedResult), "Unexpected result returned");
        }
    }
}
