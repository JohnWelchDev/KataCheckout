using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Entities.Products;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace KataCheckout.Tests.Common.Offers
{
    /// <summary>
    /// The offer test utility.
    /// </summary>
    public class OfferTestUtility
    {
        /// <summary>
        /// Gets cart lines.
        /// </summary>
        /// <param name="lines">The line data.</param>
        /// <returns>The cart lines.</returns>
        public static Dictionary<string, CartLineItem> GetCartLines(List<(string, int)> lines)
        {
            List<(string, decimal, int)> updatedLines = new List<(string, decimal, int)>();

            // loop through lines.
            foreach ((string, int) line in lines)
            {
                // insert previously used hard coded unit price into updated line.
                (string, decimal, int) update = (line.Item1, 5.99M, line.Item2);

                // add to updated lines.
                updatedLines.Add(update);
            }

            // get the cart lines.
            return GetCartLines(updatedLines);
        }

        /// <summary>
        /// Gets cart lines.
        /// </summary>
        /// <param name="lines">The line data.</param>
        /// <returns>The cart lines.</returns>
        public static Dictionary<string, CartLineItem> GetCartLines(List<(string, decimal, int)> lines)
        {
            Dictionary<string, CartLineItem> dicLineItems = new Dictionary<string, CartLineItem>();

            // loop through line item data.
            foreach ((string, decimal, int) line in lines)
            {
                // create line item.
                CartLineItem lineItem = new CartLineItem(new Product { SKU = line.Item1, UnitPrice = line.Item2, }, line.Item3);

                // add to unique line items.
                dicLineItems.Add(line.Item1, lineItem);
            }

            return dicLineItems;
        }

        /// <summary>
        /// Gets special offer.
        /// </summary>
        /// <param name="id">The offer identifier.</param>
        /// <param name="skusAndLimits">The skus and limits.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="amount">The discount amount.</param>
        /// <param name="limitPerCheckout">The limit per checkout.</param>
        /// <param name="conditionNest">The condition data.</param>
        /// <returns>The offer.</returns>
        public static SpecialOffer GetSpecialOffer(int id, List<(string, int?)> skusAndLimits, OfferExecutionMode mode, decimal amount, int? limitPerCheckout, ConditionNest? conditionNest)
        {
            SpecialOffer offer = new SpecialOffer();
            offer.SpecialOfferID = id;
            offer.OfferMode = mode;
            offer.DiscountAmount = amount;
            offer.LimitPerCheckout = limitPerCheckout;

            // generate and fill offer condition.
            if (conditionNest != null)
            {
                OfferCondition condition = GetCondition(conditionNest);
                FillCondition(offer.Condition, condition);
            }

            // loop through creating the rules to execute offer on
            // (different to the conditions).
            foreach ((string, int?) data in skusAndLimits)
            {
                // create rule.
                OfferExecutionRule rule = new OfferExecutionRule { SKU = data.Item1, NumUnitLimit = data.Item2 };

                // add to offer rule collection.
                offer.AddExecutionRule(rule);
            }

            return offer;
        }

        /// <summary>
        /// Gets condition.
        /// </summary>
        /// <param name="conditionNest">The condition nest.</param>
        /// <returns>The condition.</returns>
        public static OfferCondition GetCondition(ConditionNest conditionNest)
        {
            OfferCondition condition = new OfferCondition();
            condition.RelationCode = conditionNest.RelationCode ?? RelationCodes.AND;

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
                    condition.ChildOfferConditions.Add(GetCondition(childNest));
                }
            }

            return condition;
        }

        public static void FillCondition(OfferCondition targetCondition, OfferCondition sourceCondition)
        {
            targetCondition.UnitConditions.AddRange(sourceCondition.UnitConditions);
            targetCondition.ChildOfferConditions.AddRange(sourceCondition.ChildOfferConditions);
            targetCondition.RelationCode = sourceCondition.RelationCode;
            targetCondition.InvertEvaluation = sourceCondition.InvertEvaluation;
        }
    }
}
