using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData.EvaluateConditionTestData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData.OfferAppliesTestData
{
    public class EvaluateOfferAppliesSingleLevelConditionTestData : EvaluateSingleLevelConditionTestData, IEnumerable
    {
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public new IEnumerator GetEnumerator()
        {
            // get the enumerator from the base class.
            IEnumerator baseEnumerator = base.GetEnumerator();

            return OfferAppliesTestConverter.Convert(baseEnumerator);
        }
    }
}
