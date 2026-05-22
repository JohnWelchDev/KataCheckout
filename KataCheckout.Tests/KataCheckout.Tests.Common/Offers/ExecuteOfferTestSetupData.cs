using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace KataCheckout.Tests.Common.Offers
{
    /// <summary>
    /// The setup object for execute offer tests.
    /// </summary>
    public class ExecuteOfferTestSetupData
    {
        public ExecuteOfferTestSetupData()
        {
            this.ExpectedAffectedLineItems = new List<(string, decimal, int)>();
            this.ExpectedUnaffectedLineItems = new List<(string, decimal, int)>();
        }

        /// <summary>
        /// Gets or sets the specia offer identifier.
        /// </summary>
        public int SpecialOfferID { get; set; }

        /// <summary>
        /// Gets or sets the cart line data.
        /// </summary>
        public List<(string, decimal, int)>? CartLineData { get; set; }

        /// <summary>
        /// Gets or sets the affected products.
        /// </summary>
        public List<(string, int?)>? AffectedProducts { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        public OfferExecutionMode Mode { get; set; }

        /// <summary>
        /// Gets or sets the discount value.
        /// </summary>
        public decimal DiscountValue { get; set; }

        /// <summary>
        /// Gets or sets the condition nest.
        /// </summary>
        public ConditionNest? ConditionNest { get; set; }

        /// <summary>
        /// Gets or sets the number of times per checkout the offer can be applied.
        /// </summary>
        public int? LimitPerCheckout { get; set; }

        /// <summary>
        /// Gets or sets the expected discount amount.
        /// </summary>
        public decimal ExpectedDiscountAmount { get; set; }

        /// <summary>
        /// Gets or sets the expected affected line items.
        /// </summary>
        public List<(string, decimal, int)> ExpectedAffectedLineItems { get; set; }

        /// <summary>
        /// Gets or sets the expected affected line items.
        /// </summary>
        public List<(string, decimal, int)> ExpectedUnaffectedLineItems { get; set; }

        /// <summary>
        /// Gets or sets the expected log.
        /// </summary>
        public string? ExpectedLog { get; set; }

        /// <summary>
        /// Gets or set the test name.
        /// </summary>
        public string? TestName { get; set; }

        public static ExecuteOfferTestSetupData SetupData(
            int offerID = 0,
            List<(string, decimal, int)>? cartLineData = null,
            List<(string, int?)>? affectedProducts = null,
            OfferExecutionMode mode = OfferExecutionMode.FlatTotalDiscount,
            decimal discountValue = 0,
            ConditionNest? conditionNest = null,
            int? limitPerCheckout = null,
            decimal expectedDiscountAmount = 0,
            string? expectedLog = null,
            List<(string, decimal, int)>? expectedAffectedLineItems = null,
            List<(string, decimal, int)>? expectedUnaffectedItems = null,
            string? testName = null
            )
        {
            ExecuteOfferTestSetupData data = new ExecuteOfferTestSetupData();
            data.SpecialOfferID = offerID;
            data.CartLineData = cartLineData;
            data.AffectedProducts = affectedProducts;
            data.Mode = mode;
            data.DiscountValue = discountValue;
            data.ConditionNest = conditionNest;
            data.LimitPerCheckout = limitPerCheckout;
            data.ExpectedDiscountAmount = expectedDiscountAmount;
            data.ExpectedLog = expectedLog;
            data.ExpectedAffectedLineItems = expectedAffectedLineItems ?? new List<(string, decimal, int)>();
            data.ExpectedUnaffectedLineItems = expectedUnaffectedItems ?? new List<(string, decimal, int)>();
            data.TestName = testName;

            return data;
        }
    }
}
