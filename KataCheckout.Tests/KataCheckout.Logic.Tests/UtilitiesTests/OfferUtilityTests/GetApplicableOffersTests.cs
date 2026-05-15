using KataCheckout.Entities.Offers;
using KataCheckout.Entities.Products;
using KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData;
using KataCheckout.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests
{
    /// <summary>
    /// The get applicable offer tests.
    /// </summary>
    [TestFixture]
    public class GetApplicableOffersTests
    {
        [TestCaseSource(typeof(GetApplicableSingleLevelOffersTestData))]
        public void ReturnsCorrectSingleLevelOffers(IEnumerable<BaseProduct> products, IEnumerable<SpecialOffer> specialOffers, IEnumerable<SpecialOffer> expectedResults)
        {
            this.ExecuteTest(products, specialOffers, expectedResults);
        }

        [TestCaseSource(typeof(GetApplicableMultiLevelOffersTestData))]
        public void ReturnsCorrectMultiLevelOffers(IEnumerable<BaseProduct> products, IEnumerable<SpecialOffer> specialOffers, IEnumerable<SpecialOffer> expectedResults)
        {
            this.ExecuteTest(products, specialOffers, expectedResults);
        }

        public void ExecuteTest(IEnumerable<BaseProduct> products, IEnumerable<SpecialOffer> specialOffers, IEnumerable<SpecialOffer> expectedResults)
        {
            List<SpecialOffer> results = OfferUtility.GetApplicableOffers(specialOffers, products);

            // check right number of results returned.
            Assert.That(results.Count(), Is.EqualTo(expectedResults.Count()), "Incorrect number of results returned");

            Dictionary<int, SpecialOffer> dicResults = new Dictionary<int, SpecialOffer>();

            // read results into dictionary.
            foreach(SpecialOffer offer in results)
            {
                dicResults.Add(offer.SpecialOfferID, offer);
            }

            // loop through expected results.
            foreach (SpecialOffer expectedResult in expectedResults)
            {
                Assert.That(dicResults.ContainsKey(expectedResult.SpecialOfferID), Is.True, "Expected results not returned");
            }
        }
    }
}
