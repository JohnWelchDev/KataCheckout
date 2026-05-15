using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData;
using KataCheckout.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests
{
    /// <summary>
    /// The evaluate condition tests.
    /// </summary>
    [TestFixture]
    public class EvaluateConditionTests
    {
        [TestCaseSource(typeof(EvaluateConditionTestData))]
        public void EvaluatesCorrectly(OfferCondition condition, Dictionary<string, CartLineItem> cartLines, bool expectedResult)
        {
            this.ExecuteTest(condition, cartLines, expectedResult);
        }

        public void ExecuteTest(OfferCondition condition, Dictionary<string, CartLineItem> cartLines, bool expectedResult)
        {
            bool pass = OfferUtility.EvaluateCondition(condition, cartLines);

            Assert.That(pass, Is.EqualTo(expectedResult), "Unexpected result");
        }
    }
}
