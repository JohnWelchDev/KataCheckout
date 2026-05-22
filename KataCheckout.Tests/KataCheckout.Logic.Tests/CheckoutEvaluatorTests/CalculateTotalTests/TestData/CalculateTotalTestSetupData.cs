using KataCheckout.Tests.Common.Offers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace KataCheckout.Logic.Tests.CheckoutEvaluatorTests.CalculateTotalTests.TestData
{
    /// <summary>
    /// The calculate test setup data.
    /// </summary>
    public class CalculateTotalTestSetupData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalculateTotalTestSetupData" /> class.
        /// </summary>
        private CalculateTotalTestSetupData()
        {
            this.OfferSetupData = new List<ExecuteOfferTestSetupData>();
        }

        /// <summary>
        /// Gets or sets the cart lines.
        /// </summary>
        public List<(string, decimal, int)>? CartLines { get; set; }

        /// <summary>
        /// Gets or sets the offer setup data.
        /// </summary>
        public List<ExecuteOfferTestSetupData>? OfferSetupData { get; set; }

        /// <summary>
        /// Gets or sets the expected total excluding discount.
        /// </summary>
        public decimal ExpectedTotalExcludingDiscount { get; set; }

        /// <summary>
        /// Gets or sets the expected total including discount.
        /// </summary>
        public decimal ExpectedTotalIncludingDiscount { get; set; }

        /// <summary>
        /// Gets or sets the test name.
        /// </summary>
        public string? TestName { get; set; }

        public static CalculateTotalTestSetupData Setup(List<(string, decimal, int)>? cartLines = null, IEnumerable<ExecuteOfferTestSetupData>? offerTestData = null,
            decimal expectedTotalExcludingDiscount = 0, decimal expectedTotalIncludingDiscount = 0, string? testName = null)
        {
            CalculateTotalTestSetupData data = new CalculateTotalTestSetupData();

            data.CartLines = cartLines;

            if (offerTestData != null)
            {
                data.OfferSetupData?.AddRange(offerTestData);
            }
            
            data.ExpectedTotalExcludingDiscount = expectedTotalExcludingDiscount;
            data.ExpectedTotalIncludingDiscount = expectedTotalIncludingDiscount;
            data.TestName = testName;

            return data;
        }
    }
}
