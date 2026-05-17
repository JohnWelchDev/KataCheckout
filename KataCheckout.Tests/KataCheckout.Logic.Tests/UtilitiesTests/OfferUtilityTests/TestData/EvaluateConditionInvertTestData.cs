using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Tests.Common.Offers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData
{
    /// <summary>
    /// The evaluate condition utilising invert test data.
    /// </summary>
    public class EvaluateConditionInvertTestData : BaseEvaluateConditionTestData, IEnumerable
    {
        /// <summary>
        /// Gets or sets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            OfferCondition oneCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2)], Invert = true });
            Dictionary<string, CartLineItem> oneLineItems = this.GetCartLines([("A", 2)]);
            bool oneResult = false;
            string oneName = "Invert - One item - equal to pass";

            OfferCondition twoCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2)], Invert = true });
            Dictionary<string, CartLineItem> twoLineItems = this.GetCartLines([("A", 1)]);
            bool twoResult = false;
            string twoName = "Invert - One item - equal to fail";

            OfferCondition threeCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 1)], RelationCode = RelationCodes.AND, Invert = true });
            Dictionary<string, CartLineItem> threeLineItems = this.GetCartLines([("A", 2), ("B", 1)]);
            bool threeResult = false;
            string threeName = "Invert - Two items - both pass";

            OfferCondition fourCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 1)], RelationCode = RelationCodes.AND, Invert = true });
            Dictionary<string, CartLineItem> fourLineItems = this.GetCartLines([("A", 2), ("B", 2)]);
            bool fourResult = true;
            string fourName = "Invert - Two items - AND - one passes";

            OfferCondition fiveCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 1)], RelationCode = RelationCodes.AND, Invert = true });
            Dictionary<string, CartLineItem> fiveLineItems = this.GetCartLines([("A", 3), ("B", 2)]);
            bool fiveResult = true;
            string fiveName = "Invert - Two items - AND - both fail";

            OfferCondition sixCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 1)], RelationCode = RelationCodes.OR, Invert = true });
            Dictionary<string, CartLineItem> sixLineItems = this.GetCartLines([("A", 2), ("B", 2)]);
            bool sixResult = false;
            string sixName = "Invert - Two items - OR - one passes";

            OfferCondition sevenCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 1)], RelationCode = RelationCodes.OR, Invert = true });
            Dictionary<string, CartLineItem> sevenLineItems = this.GetCartLines([("A", 3), ("B", 2)]);
            bool sevenResult = true;
            string sevenName = "Invert - Two items - OR - both fail";

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
