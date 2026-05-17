using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Tests.Common.Offers;
using System;
using System.Collections;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData
{
    public class EvaluateMultiConditionTestData : BaseEvaluateConditionTestData, IEnumerable
    {
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            OfferCondition oneCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.AND, Invert = false });
            Dictionary<string, CartLineItem> oneLineItems = this.GetCartLines([("B", 3), ("A", 2)]);
            bool oneResult = true;
            string oneName = "Two conditions - AND - both pass";

            OfferCondition twoCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.AND, Invert = false });
            Dictionary<string, CartLineItem> twoLineItems = this.GetCartLines([("B", 4), ("A", 2)]);
            bool twoResult = false;
            string twoName = "Two conditions - AND - 1 pass 1 fail";

            OfferCondition threeCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.AND, Invert = false });
            Dictionary<string, CartLineItem> threeLineItems = this.GetCartLines([("B", 4), ("A", 2)]);
            bool threeResult = false;
            string threeName = "Two conditions - AND - both fail";

            OfferCondition fourCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.OR, Invert = false });
            Dictionary<string, CartLineItem> fourLineItems = this.GetCartLines([("B", 3), ("A", 2)]);
            bool fourResult = true;
            string fourName = "Two conditions - OR - both pass";

            OfferCondition fiveCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.OR, Invert = false });
            Dictionary<string, CartLineItem> fiveLineItems = this.GetCartLines([("B", 4), ("A", 2)]);
            bool fiveResult = true;
            string fiveName = "Two conditions - OR - 1 pass 1 fail";

            OfferCondition sixCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.OR, Invert = false });
            Dictionary<string, CartLineItem> sixLineItems = this.GetCartLines([("B", 4), ("A", 2)]);
            bool sixResult = false;
            string sixName = "Two conditions - OR - both fail";

            yield return new TestCaseData(oneCondition, oneLineItems, oneResult).SetName(oneName);
            yield return new TestCaseData(twoCondition, twoLineItems, twoResult).SetName(twoName);
            yield return new TestCaseData(threeCondition, threeLineItems, threeResult).SetName(threeName);
            yield return new TestCaseData(fourCondition, fourLineItems, fourResult).SetName(fourName);
            yield return new TestCaseData(fiveCondition, fiveLineItems, fiveResult).SetName(fiveName);
            yield return new TestCaseData(sixCondition, sixLineItems, sixResult).SetName(sixName);
        }
    }
}
