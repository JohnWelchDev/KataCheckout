using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Tests.Common.Offers;
using System.Collections;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData.EvaluateConditionTestData
{
    /// <summary>
    /// The evaluate condition test data.
    /// </summary>
    public class EvaluateSingleLevelConditionTestData : BaseEvaluateConditionTestData, IEnumerable
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

            OfferCondition sixCondition = this.GetCondition(new ConditionNest { Units = [("A", ">=", 2)], Invert = false });
            Dictionary<string, CartLineItem> sixLineItems = this.GetCartLines([("A", 3)]);
            bool sixResult = true;
            string sixName = "One item - greater than or equal pass";

            OfferCondition sevenCondition = this.GetCondition(new ConditionNest { Units = [("A", ">=", 2)], Invert = false });
            Dictionary<string, CartLineItem> sevenLineItems = this.GetCartLines([("A", 1)]);
            bool sevenResult = false;
            string sevenName = "One item - greater than or equal fail";

            yield return new TestCaseData(oneCondition, oneLineItems, oneResult).SetName(oneName);
            yield return new TestCaseData(twoCondition, twoLineItems, twoResult).SetName(twoName);
            yield return new TestCaseData(threeCondition, threeLineItems, threeResult).SetName(threeName);
            yield return new TestCaseData(fourCondition, fourLineItems, fourResult).SetName(fourName);
            yield return new TestCaseData(fiveCondition, fiveLineItems, fiveResult).SetName(fiveName);
            yield return new TestCaseData(sixCondition, sixLineItems, sixResult).SetName(sixName);
            yield return new TestCaseData(sevenCondition, sevenLineItems, sevenResult).SetName(sevenName);
        }
    }
}