using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Tests.Common.Offers;
using System;
using System.Collections;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData.EvaluateConditionTestData
{
    /// <summary>
    /// The evaluate multi level condition test data.
    /// </summary>
    public class EvaluateMultiLevelConditionTestData : BaseEvaluateConditionTestData, IEnumerable
    {
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            OfferCondition oneCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.AND, Invert = false });
            OfferCondition oneChildOne = this.GetCondition(new ConditionNest { Units = [("C", ">", 1), ("D", "<=", 3)], RelationCode = RelationCodes.OR, Invert = false });
            oneCondition.ChildOfferConditions.Add(oneChildOne);
            Dictionary<string, CartLineItem> oneLineItems = this.GetCartLines([("B", 3), ("A", 2), ("C", 2), ("D", 3)]);
            bool oneResult = true;
            string oneName = "Two conditions one child group - both pass";

            OfferCondition twoCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.AND, Invert = false });
            OfferCondition twoChildOne = this.GetCondition(new ConditionNest { Units = [("C", ">", 1), ("D", "<=", 3)], RelationCode = RelationCodes.OR, Invert = false });
            twoCondition.ChildOfferConditions.Add(twoChildOne);
            Dictionary<string, CartLineItem> twoLineItems = this.GetCartLines([("B", 3), ("A", 1), ("C", 2), ("D", 2)]);
            bool twoResult = false;
            string twoName = "Two conditions one child group - units pass child group fails";

            OfferCondition threeCondition = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.AND, Invert = false });
            OfferCondition threeChildOne = this.GetCondition(new ConditionNest { Units = [("C", ">", 1), ("D", "<=", 3)], RelationCode = RelationCodes.OR, Invert = false });
            threeCondition.ChildOfferConditions.Add(threeChildOne);
            Dictionary<string, CartLineItem> threeLineItems = this.GetCartLines([("B", 4), ("A", 2), ("C", 2), ("D", 3)]);
            bool threeResult = false;
            string threeName = "Two conditions one child group - units and child group fails";

            OfferCondition fourCondition = this.GetCondition(new ConditionNest {RelationCode = RelationCodes.AND, Invert = false });
            OfferCondition fourChildOne = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.AND, Invert = false });
            OfferCondition fourChildTwo = this.GetCondition(new ConditionNest { Units = [("C", ">", 1), ("D", "<=", 3)], RelationCode = RelationCodes.OR, Invert = false });
            fourCondition.ChildOfferConditions.AddRange([fourChildOne, fourChildTwo]);
            Dictionary<string, CartLineItem> fourLineItems = this.GetCartLines([("B", 3), ("A", 2), ("C", 2), ("D", 3)]);
            bool fourResult = true;
            string fourName = "No conditions two child groups - AND - both groups pass";

            OfferCondition fiveCondition = this.GetCondition(new ConditionNest { RelationCode = RelationCodes.AND, Invert = false });
            OfferCondition fiveChildOne = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.AND, Invert = false });
            OfferCondition fiveChildTwo = this.GetCondition(new ConditionNest { Units = [("C", ">", 1), ("D", "<=", 3)], RelationCode = RelationCodes.OR, Invert = false });
            fiveCondition.ChildOfferConditions.AddRange([fiveChildOne, fiveChildTwo]);
            Dictionary<string, CartLineItem> fiveLineItems = this.GetCartLines([("B", 3), ("A", 2), ("C", 1), ("D", 4)]);
            bool fiveResult = false;
            string fiveName = "No conditions two child groups - AND - one group pass one group fail";

            OfferCondition sixCondition = this.GetCondition(new ConditionNest { RelationCode = RelationCodes.AND, Invert = false });
            OfferCondition sixChildOne = this.GetCondition(new ConditionNest { Units = [("A", "=", 2), ("B", "=", 3)], RelationCode = RelationCodes.AND, Invert = false });
            OfferCondition sixChildTwo = this.GetCondition(new ConditionNest { Units = [("C", ">", 1), ("D", "<=", 3)], RelationCode = RelationCodes.OR, Invert = false });
            sixCondition.ChildOfferConditions.AddRange([sixChildOne, sixChildTwo]);
            Dictionary<string, CartLineItem> sixLineItems = this.GetCartLines([("B", 3), ("A", 3), ("C", 2), ("D", 4)]);
            bool sixResult = false;
            string sixName = "No conditions two child groups - AND - both groups fail";

            yield return new TestCaseData(oneCondition, oneLineItems, oneResult).SetName(oneName);
            yield return new TestCaseData(twoCondition, twoLineItems, twoResult).SetName(twoName);
            yield return new TestCaseData(threeCondition, threeLineItems, threeResult).SetName(threeName);
            yield return new TestCaseData(fourCondition, fourLineItems, fourResult).SetName(fourName);
            yield return new TestCaseData(fiveCondition, fiveLineItems, fiveResult).SetName(fiveName);
            yield return new TestCaseData(sixCondition, sixLineItems, sixResult).SetName(sixName);
        }
    }
}
