using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData.OfferAppliesTestData
{
    /// <summary>
    /// The helper for converting evaluate condition tests to offer applies tests.
    /// </summary>
    internal class OfferAppliesTestConverter
    {
        /// <summary>
        /// Converts tests.
        /// </summary>
        /// <param name="sourceTests">The source tests.</param>
        /// <returns>The converted tests.</returns>
        internal static IEnumerator Convert(IEnumerator sourceTests)
        {
            // loop through enumerator.
            while (sourceTests.MoveNext())
            {
                TestCaseData? data = sourceTests.Current as TestCaseData;

                // check test case data parsed correctly.
                if (data != null && data.Arguments != null && data.Arguments.Count() > 0)
                {
                    OfferCondition? condition = null;
                    Dictionary<string, CartLineItem>? cartLineItems = null;
                    bool? expectedResult = null;

                    // loop through arguments.
                    foreach (object? arg in data.Arguments)
                    {
                        OfferCondition? attemptCondition = arg as OfferCondition;

                        // check parsed successfully.
                        if (attemptCondition != null)
                        {
                            condition = attemptCondition;
                        }
                        else
                        {
                            Dictionary<string, CartLineItem>? attemptLineItems = arg as Dictionary<string, CartLineItem>;

                            // check parsed to line item dictionary.
                            if (attemptLineItems != null)
                            {
                                cartLineItems = attemptLineItems;
                            }
                            else
                            {
                                bool? attemptResult = arg as bool?;

                                if (attemptResult != null)
                                {
                                    expectedResult = attemptResult;
                                }
                            }
                        }
                    }

                    if (condition != null && cartLineItems != null && expectedResult != null)
                    {
                        SpecialOffer offer = new SpecialOffer();
                        offer.SpecialOfferID = 1;
                        offer.LimitPerCheckout = null;

                        // fill conditions.
                        offer.Condition.UnitConditions.AddRange(condition.UnitConditions);
                        offer.Condition.ChildOfferConditions.AddRange(condition.ChildOfferConditions);
                        offer.Condition.InvertEvaluation = condition.InvertEvaluation;

                        yield return new TestCaseData(cartLineItems, offer, expectedResult).SetName($"Offer - {data.TestName}");
                    }
                }
            }
        }
    }
}
