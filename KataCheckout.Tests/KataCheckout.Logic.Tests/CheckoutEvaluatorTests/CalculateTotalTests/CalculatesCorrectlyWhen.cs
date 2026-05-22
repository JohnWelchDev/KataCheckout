using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Logic.Logging;
using KataCheckout.Logic.Tests.CheckoutEvaluatorTests.CalculateTotalTests.TestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.CheckoutEvaluatorTests.CalculateTotalTests
{
    [TestFixture]
    public class CalculatesCorrectlyWhen
    {
        [TestCaseSource(typeof(CalculateTotalNoOffersTestData))]
        public void NoSpecialOffersPresent(IEnumerable<CartLineItem> cartLineItems, IEnumerable<SpecialOffer> specialOffers,
            decimal expectedTotalNoDiscount, decimal expectedTotalIncludingDiscount)
        {
            this.ExecuteTest(cartLineItems, specialOffers, expectedTotalNoDiscount, expectedTotalIncludingDiscount);
        }

        [TestCaseSource(typeof(CalculateTotalWithApplicableOffersTestData))]
        public void SpecialOffersPresent(IEnumerable<CartLineItem> cartLineItems, IEnumerable<SpecialOffer> specialOffers,
            decimal expectedTotalNoDiscount, decimal expectedTotalIncludingDiscount)
        {
            this.ExecuteTest(cartLineItems, specialOffers, expectedTotalNoDiscount, expectedTotalIncludingDiscount);
        }

        [TestCaseSource(typeof(CalculateTotalApplyOffersMultipleTimes))]
        public void SpecialOffersRunMultipleTimes(IEnumerable<CartLineItem> cartLineItems, IEnumerable<SpecialOffer> specialOffers,
            decimal expectedTotalNoDiscount, decimal expectedTotalIncludingDiscount)
        {
            this.ExecuteTest(cartLineItems, specialOffers, expectedTotalNoDiscount, expectedTotalIncludingDiscount);
        }

        public void ExecuteTest(IEnumerable<CartLineItem> cartLineItems, IEnumerable<SpecialOffer> specialOffers, decimal expectedTotalNoDiscount, decimal expectedTotalWithDiscount)
        {
            SpecialOfferLogger logger = new SpecialOfferLogger();
            CheckoutEvaluator checkoutEvaluator = new CheckoutEvaluator(specialOffers, logger);

            // calculate the total.
            CartCalculationResponse response = checkoutEvaluator.CalculateTotal(cartLineItems);

            Assert.That(response, Is.Not.Null, "Resonse returned null");
            Assert.That(response.Success, Is.True, "Response returned unsuccessful");

            Assert.That(response.TotalExcludingOffers, Is.EqualTo(expectedTotalNoDiscount), "Unexpected total excluding discount");
            Assert.That(response.TotalIncludingOffers, Is.EqualTo(expectedTotalWithDiscount), "Unexpected total including discount");
        }
    }
}
