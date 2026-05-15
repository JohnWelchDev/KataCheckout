using KataCheckout.Entities.Offers;
using KataCheckout.Entities.Products;
using KataCheckout.Tests.Common.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData
{
    /// <summary>
    /// The get applicable offers test data base class.
    /// </summary>
    internal abstract class BaseGetApplicableOffersTestData
    {
        /// <summary>
        /// Generates test.
        /// </summary>
        /// <param name="productSkus">The product skus.</param>
        /// <param name="allOfferSkus">The skus for all offers.</param>
        /// <param name="expectedIndices">The indices of offers expected to be in the results.</param>
        /// <param name="testName">The test name.</param>
        /// <returns>The test case data.</returns>
        protected TestCaseData GenerateTest(IEnumerable<string> productSkus, IEnumerable<SkuNest> allOfferSkus, IEnumerable<int> expectedIndices, string testName)
        {
            IEnumerable<BaseProduct> products = this.GetProducts(productSkus);
            List<SpecialOffer> allOffers = new List<SpecialOffer>();

            int id = 1;
            foreach (SkuNest skuNest in allOfferSkus)
            {
                SpecialOffer offer = this.GetSpecialOffer(skuNest, id);

                allOffers.Add(offer);

                id++;
            }

            List<SpecialOffer> expectedResults = new List<SpecialOffer>();

            // loop through indices for expected results.
            foreach (int targetIndex in expectedIndices)
            {
                // check index is valid.
                if (targetIndex < allOfferSkus.Count())
                {
                    // add offer to expected results.
                    expectedResults.Add(allOffers[targetIndex]);
                }
            }

            return new TestCaseData(products, allOffers, expectedResults).SetName(testName);
        }

        /// <summary>
        /// Gets products.
        /// </summary>
        /// <param name="skus">The skus.</param>
        /// <returns>The products.</returns>
        protected IEnumerable<BaseProduct> GetProducts(IEnumerable<string> skus)
        {
            // loop through skus.
            foreach (string sku in skus)
            {
                yield return new Product { SKU = sku, UnitPrice = 5.73M };
            }
        }

        /// <summary>
        /// Gets special offer.
        /// </summary>
        /// <param name="skuNest">The nest of skus.</param>
        /// <param name="specialOfferID">The special offer identifier.</param>
        /// <returns>The special offer.</returns>
        protected SpecialOffer GetSpecialOffer(SkuNest skuNest, int specialOfferID)
        {
            SpecialOffer specialOffer = new SpecialOffer();
            specialOffer.SpecialOfferID = specialOfferID;

            // fill condition.
            skuNest.FillCondition(specialOffer.Condition);

            return specialOffer;
        }
    }
}
