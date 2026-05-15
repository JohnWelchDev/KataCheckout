using KataCheckout.Entities.Offers;
using KataCheckout.Entities.Tests.OfferTests.TestData;

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace KataCheckout.Entities.Tests.OfferTests
{
    /// <summary>
    /// The special offer tests.
    /// </summary>
    [TestFixture]
    public class SpecialOfferTests
    {
        [TestCaseSource(typeof(SpecialOfferExtractSkusTestData))]
        public void ExtractsSkusCorrectly(SpecialOffer specialOffer, IEnumerable<string> expectedResults)
        {
            this.ExecuteTest(specialOffer, expectedResults);
        }

        [Test]
        public void ExtractsEmptyOfferCorrectly()
        {
            SpecialOffer specialOffer = new SpecialOffer();
            string[] expectedResults= new string[0];

            this.ExecuteTest(specialOffer, expectedResults);
        }

        private void ExecuteTest(SpecialOffer specialOffer, IEnumerable<string> expectedResults)
        {
            // extract skus from offer.
            IEnumerable<string> extractedSkus = specialOffer.ExtractSkus();

            Assert.That(extractedSkus.Count(), Is.EqualTo(expectedResults.Count()), "Number of results does not match expected amount");

            Dictionary<string, string> dicResults = new Dictionary<string, string>();

            // loop through the results checking for duplicates.
            foreach(string result in extractedSkus)
            {
                Assert.That(dicResults.ContainsKey(result), Is.Not.True, "Sku returned multiple times in resultset");

                // check should be made moot by previous assert, but future proofing in case
                // it moves/changes in the future - the vulnerability would then be easy to miss.
                if (!dicResults.ContainsKey(result))
                {
                    // add to dictionary.
                    dicResults.Add(result, result);
                }
            }

            foreach (string expectedResult in expectedResults)
            {
                Assert.That(dicResults.ContainsKey(expectedResult), Is.True, "expected sku was not returned in resultset");
            }
        }
    }
}
