using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Entities.Products;
using KataCheckout.Tests.Common.Offers;
using KataCheckout.Tests.Common.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData
{
    /// <summary>
    /// The evaluate condition test data.
    /// </summary>
    public class EvaluateConditionTestData : BaseEvaluateConditionTestData, IEnumerable
    {
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            OfferCondition oneCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2)], Invert = false });
            Dictionary<string, CartLineItem> oneLineItems = this.GetCartLines([("A", 2)]);
            bool oneResult = true;
            string oneName = "One item - equal to pass";

            OfferCondition twoCondition = this.GetCondition(new ConditionNest { Units = [("B", "=", 2)], Invert = false });
            Dictionary<string, CartLineItem> twoLineItems = this.GetCartLines([("A", 2)]);
            bool twoResult = false;
            string twoName = "One item - equal to fail on sku";

            OfferCondition threeCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 3)], Invert = false });
            Dictionary<string, CartLineItem> threeLineItems = this.GetCartLines([("A", 2)]);
            bool threeResult = false;
            string threeName = "One item - equal to fail on num units";

            OfferCondition fourCondition = this.GetCondition(new ConditionNest { Units = [("A", ">", 2)], Invert = false });
            Dictionary<string, CartLineItem> fourLineItems = this.GetCartLines([("A", 3)]);
            bool fourResult = true;
            string fourName = "One item - greater than pass";

            OfferCondition fiveCondition = this.GetCondition(new ConditionNest { Units = [("A", ">", 2)], Invert = false });
            Dictionary<string, CartLineItem> fiveLineItems = this.GetCartLines([("A", 2)]);
            bool fiveResult = false;
            string fiveName = "One item - greater than fail";

            yield return new TestCaseData(oneCondition, oneLineItems, oneResult).SetName(oneName);
            yield return new TestCaseData(twoCondition, twoLineItems, twoResult).SetName(twoName);
            yield return new TestCaseData(threeCondition, threeLineItems, threeResult).SetName(threeName);
            yield return new TestCaseData(fourCondition, fourLineItems, fourResult).SetName(fourName);
            yield return new TestCaseData(fiveCondition, fiveLineItems, fiveResult).SetName(fiveName);
        }
    }
}