using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Tests.Common.Offers;
using System.Collections;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData
{
    /// <summary>
    /// The evaluate multi level condition utilising invert test data.
    /// </summary>
    public class EvaluateMultiLevelConditionInvertTestData : BaseEvaluateConditionTestData, IEnumerable
    {
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            OfferCondition oneCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.AND, Invert = true });
            OfferCondition oneChildOne = this.GetCondition(new ConditionNest { Units = [("C", ">", 1), ("D", "<=", 3)], RelationCode = RelationCodes.OR, Invert = false });
            oneCondition.ChildOfferConditions.Add(oneChildOne);
            Dictionary<string, CartLineItem> oneLineItems = this.GetCartLines([("B", 3), ("A", 2), ("C", 2), ("D", 3)]);
            bool oneResult = false;
            string oneName = "Invert parent - Two conditions one child group - AND - both pass";

            OfferCondition twoCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.AND, Invert = true });
            OfferCondition twoChildOne = this.GetCondition(new ConditionNest { Units = [("C", ">", 1), ("D", "<=", 3)], RelationCode = RelationCodes.OR, Invert = false });
            twoCondition.ChildOfferConditions.Add(twoChildOne);
            Dictionary<string, CartLineItem> twoLineItems = this.GetCartLines([("B", 2), ("A", 3), ("C", 2), ("D", 3)]);
            bool twoResult = true;
            string twoName = "Invert parent - Two conditions one child group - AND - units fail child passes";

            OfferCondition threeCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.OR, Invert = true });
            OfferCondition threeChildOne = this.GetCondition(new ConditionNest { Units = [("C", ">", 1), ("D", "<=", 3)], RelationCode = RelationCodes.OR, Invert = false });
            threeCondition.ChildOfferConditions.Add(threeChildOne);
            Dictionary<string, CartLineItem> threeLineItems = this.GetCartLines([("B", 2), ("A", 3), ("C", 2), ("D", 3)]);
            bool threeResult = false;
            string threeName = "Invert parent - Two conditions one child group - OR - units fail child passes";

            OfferCondition fourCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.AND, Invert = false });
            OfferCondition fourChildOne = this.GetCondition(new ConditionNest { Units = [("C", ">", 1), ("D", "<=", 3)], RelationCode = RelationCodes.OR, Invert = true });
            fourCondition.ChildOfferConditions.Add(fourChildOne);
            Dictionary<string, CartLineItem> fourLineItems = this.GetCartLines([("B", 3), ("A", 2), ("C", 2), ("D", 3)]);
            bool fourResult = false;
            string fourName = "Invert child - Two conditions one child group - AND - both pass";

            OfferCondition fiveCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.AND, Invert = false });
            OfferCondition fiveChildOne = this.GetCondition(new ConditionNest { Units = [("C", ">", 1), ("D", "<=", 3)], RelationCode = RelationCodes.OR, Invert = true });
            fiveCondition.ChildOfferConditions.Add(fiveChildOne);
            Dictionary<string, CartLineItem> fiveLineItems = this.GetCartLines([("B", 2), ("A", 3), ("C", 2), ("D", 3)]);
            bool fiveResult = true;
            string fiveName = "Invert child - Two conditions one child group - AND - units fail child passes";

            OfferCondition sixCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.OR, Invert = false });
            OfferCondition sixChildOne = this.GetCondition(new ConditionNest { Units = [("C", ">", 1), ("D", "<=", 3)], RelationCode = RelationCodes.OR, Invert = true });
            sixCondition.ChildOfferConditions.Add(sixChildOne);
            Dictionary<string, CartLineItem> sixLineItems = this.GetCartLines([("B", 2), ("A", 3), ("C", 2), ("D", 3)]);
            bool sixResult = false;
            string sixName = "Invert child - Two conditions one child group - OR - units fail child passes";

            yield return new TestCaseData(oneChildOne, oneLineItems, oneResult).SetName(oneName);
            yield return new TestCaseData(twoChildOne, twoLineItems, twoResult).SetName(twoName);
            yield return new TestCaseData(threeChildOne, threeLineItems, threeResult).SetName(threeName);
            yield return new TestCaseData(fourChildOne, fourLineItems, fourResult).SetName(fourName);
            yield return new TestCaseData(fiveChildOne, fiveLineItems, fiveResult).SetName(fiveName);
            yield return new TestCaseData(sixChildOne, sixLineItems, sixResult).SetName(sixName);
        }
    }
}
