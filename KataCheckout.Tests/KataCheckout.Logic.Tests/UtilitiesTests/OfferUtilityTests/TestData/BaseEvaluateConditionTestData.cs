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
            return OfferTestUtility.GetCartLines(lines);
        }

        /// <summary>
        /// Gets condition.
        /// </summary>
        /// <param name="conditionNest">The condition nest.</param>
        /// <returns>The condition.</returns>
        protected OfferCondition GetCondition(ConditionNest conditionNest)
        {
            return OfferTestUtility.GetCondition(conditionNest);
        }
    }
}
