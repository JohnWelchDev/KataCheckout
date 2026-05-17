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
        
        [TestCaseSource(typeof(EvaluateSingleConditionTestData))]
        public void EvaluatesSingleConditionCorrectly(OfferCondition condition, Dictionary<string, CartLineItem> cartLines, bool expectedResult)
        {
            this.ExecuteTest(condition, cartLines, expectedResult);
        }

        [TestCaseSource(typeof(EvaluateMultiConditionTestData))]
        public void EvaluateMultiConditionsCorrectly(OfferCondition condition, Dictionary<string, CartLineItem> cartLines, bool expectedResult)
        {
            this.ExecuteTest(condition, cartLines, expectedResult);
        }

        [TestCaseSource(typeof(EvaluateMultiLevelConditionTestData))]
        public void EvaluateMultiLevelConditionsCorrectly(OfferCondition condition, Dictionary<string, CartLineItem> cartLines, bool expectedResult)
        {
            this.ExecuteTest(condition, cartLines, expectedResult);
        }

        [TestCaseSource(typeof(EvaluateConditionInvertTestData))]
        public void EvaluatesSingleLevelInvertsCorrectly(OfferCondition condition, Dictionary<string, CartLineItem> cartLines, bool expectedResult)
        {
            this.ExecuteTest(condition, cartLines, expectedResult);
        }

        [TestCaseSource(typeof(EvaluateMultiLevelConditionInvertTestData))]
        public void EvaluatesMultiLevelInvertsCorrectly(OfferCondition condition, Dictionary<string, CartLineItem> cartLines, bool expectedResult)
        {
            this.ExecuteTest(condition, cartLines, expectedResult);
        }

        /// <summary>
        /// Executes test.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="cartLines">The cart lines.</param>
        /// <param name="expectedResult">The expected result.</param>
        public void ExecuteTest(OfferCondition condition, Dictionary<string, CartLineItem> cartLines, bool expectedResult)
        {
            bool pass = OfferUtility.EvaluateCondition(condition, cartLines);

            Assert.That(pass, Is.EqualTo(expectedResult), "Unexpected result");
        }
    }
}
