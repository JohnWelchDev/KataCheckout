using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Entities.Products;
using KataCheckout.Tests.Common.Offers;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData
{
    /// <summary>
    /// The base evaluate condition test data.
    /// </summary>
    public abstract class BaseEvaluateConditionTestData
    {
        protected Dictionary<string, CartLineItem> GetCartLines(List<(string, int)> lines)
        {
            Dictionary<string, CartLineItem> dicLineItems = new Dictionary<string, CartLineItem>();

            // loop through line item data.
            foreach ((string, int) line in lines)
            {
                // create line item.
                CartLineItem lineItem = new CartLineItem(new Product { SKU = line.Item1, UnitPrice = 5.99M, }, line.Item2);

                // add to unique line items.
                dicLineItems.Add(line.Item1, lineItem);
            }

            return dicLineItems;
        }

        /// <summary>
        /// Gets condition.
        /// </summary>
        /// <param name="conditionNest">The condition nest.</param>
        /// <returns>The condition.</returns>
        protected OfferCondition GetCondition(ConditionNest conditionNest)
        {
            OfferCondition condition = new OfferCondition();

            // populate condition units.
            foreach ((string, string, int) item in conditionNest.Units)
            {
                condition.UnitConditions.Add(new ProductConditionUnit { SKU = item.Item1, Operator = item.Item2, NumUnits = item.Item3 });
            }

            condition.InvertEvaluation = conditionNest.Invert;

            // check if child nests populated.
            if (conditionNest.ChildNests != null && conditionNest.ChildNests.Count > 0)
            {
                // loop through child nests.
                foreach (ConditionNest childNest in conditionNest.ChildNests)
                {
                    condition.ChildOfferConditions.Add(this.GetCondition(childNest));
                }
            }

            return condition;
        }
    }
}
