using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Tests.Common.Offers;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.CheckoutEvaluatorTests.CalculateTotalTests.TestData
{
    /// <summary>
    /// The base calculate total test data.
    /// </summary>
    public class BaseCalculateTotalTestData
    {
        public IEnumerable<TestCaseData> GenerateTests(IEnumerable<CalculateTotalTestSetupData> setupData)
        {
            // loop through setup data.
            foreach (CalculateTotalTestSetupData data in setupData)
            {
                Dictionary<string,CartLineItem> cartLineItems = OfferTestUtility.GetCartLines(data.CartLines ?? new List<(string, decimal, int)>());

                List<SpecialOffer> offers = new List<SpecialOffer>();

                // check the offer setup data is set.
                if (data.OfferSetupData != null)
                {
                    // loop through offer setup data.
                    foreach (ExecuteOfferTestSetupData offerData in data.OfferSetupData)
                    {
                        SpecialOffer offer = OfferTestUtility.GetSpecialOffer(offerData.SpecialOfferID, offerData.AffectedProducts ?? [], offerData.Mode,
                            offerData.DiscountValue, offerData.LimitPerCheckout, offerData.ConditionNest);

                        offers.Add(offer);
                    }

                    yield return new TestCaseData(cartLineItems.Values, offers, data.ExpectedTotalExcludingDiscount, data.ExpectedTotalIncludingDiscount).SetName(data.TestName);
                }
            }
        }
    }
}
