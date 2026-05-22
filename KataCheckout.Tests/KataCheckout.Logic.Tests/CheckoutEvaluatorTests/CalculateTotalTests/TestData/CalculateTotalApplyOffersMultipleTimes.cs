using KataCheckout.Entities.Offers;
using KataCheckout.Tests.Common.Offers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.CheckoutEvaluatorTests.CalculateTotalTests.TestData
{
    /// <summary>
    /// The calculate total apply offers multiple times test data.
    /// </summary>
    public class CalculateTotalApplyOffersMultipleTimes : BaseCalculateTotalTestData, IEnumerable
    {
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            IEnumerable<CalculateTotalTestSetupData> testData = this.GenerateTestData();

            return this.GenerateTests(testData).GetEnumerator();
        }

        public IEnumerable<CalculateTotalTestSetupData> GenerateTestData()
        {
            List<CalculateTotalTestSetupData> setupData =
                [
                    CalculateTotalTestSetupData.Setup(
                        cartLines: [("A", 15.00M, 6)],
                        offerTestData:
                        [
                            ExecuteOfferTestSetupData.SetupData (
                                offerID: 1,
                                affectedProducts: [("A", 3)],
                                mode: OfferExecutionMode.CostOverrideForProductsTotal,
                                discountValue: 40.00M,
                                conditionNest: new ConditionNest { Units = [("A", ">=", 1)], RelationCode = RelationCodes.OR, Invert = false },
                                limitPerCheckout: 2
                            )
                        ],
                        expectedTotalExcludingDiscount: 90.00M,
                        expectedTotalIncludingDiscount: 80.00M,
                        testName: "Multi Apply - Single product runs twice - no left over"
                    ),
                    CalculateTotalTestSetupData.Setup(
                        cartLines: [("A", 15.00M, 8)],
                        offerTestData:
                        [
                            ExecuteOfferTestSetupData.SetupData (
                                offerID: 1,
                                affectedProducts: [("A", 3)],
                                mode: OfferExecutionMode.CostOverrideForProductsTotal,
                                discountValue: 40.00M,
                                conditionNest: new ConditionNest { Units = [("A", ">=", 1)], RelationCode = RelationCodes.OR, Invert = false },
                                limitPerCheckout: 2
                            )
                        ],
                        expectedTotalExcludingDiscount: 120.00M,
                        expectedTotalIncludingDiscount: 110.00M,
                        testName: "Multi Apply - Single product runs twice - products left over"
                    ),
                    CalculateTotalTestSetupData.Setup(
                        cartLines: [("A", 15.00M, 8), ("B", 4.00M, 2)],
                        offerTestData:
                        [
                            ExecuteOfferTestSetupData.SetupData (
                                offerID: 1,
                                affectedProducts: [("A", 4), ("B", 1)],
                                mode: OfferExecutionMode.CheapestProductPercentageDiscount,
                                discountValue: 50.00M,
                                conditionNest: new ConditionNest { Units = [("A", ">=", 1)], RelationCode = RelationCodes.OR, Invert = false },
                                limitPerCheckout: 2
                            )
                        ],
                        expectedTotalExcludingDiscount: 128.00M,
                        expectedTotalIncludingDiscount: 124.00M,
                        testName: "Multi Apply - multiple product runs twice - no products left over"
                    ),
                    CalculateTotalTestSetupData.Setup(
                        cartLines: [("A", 15.00M, 9), ("B", 4.00M, 2)],
                        offerTestData:
                        [
                            ExecuteOfferTestSetupData.SetupData (
                                offerID: 1,
                                affectedProducts: [("A", 4), ("B", 1)],
                                mode: OfferExecutionMode.CheapestProductPercentageDiscount,
                                discountValue: 50.00M,
                                conditionNest: new ConditionNest { Units = [("A", ">=", 1)], RelationCode = RelationCodes.OR, Invert = false },
                                limitPerCheckout: 2
                            )
                        ],
                        expectedTotalExcludingDiscount: 143.00M,
                        expectedTotalIncludingDiscount: 139.00M,
                        testName: "Multi Apply - multiple product runs twice - products left over"
                    ),
                    CalculateTotalTestSetupData.Setup(
                        cartLines: [("A", 15.00M, 12), ("B", 4.00M, 3)],
                        offerTestData:
                        [
                            ExecuteOfferTestSetupData.SetupData (
                                offerID: 1,
                                affectedProducts: [("A", 4), ("B", 1)],
                                mode: OfferExecutionMode.CheapestProductPercentageDiscount,
                                discountValue: 50.00M,
                                conditionNest: new ConditionNest { Units = [("A", ">=", 1)], RelationCode = RelationCodes.OR, Invert = false },
                                limitPerCheckout: 2
                            )
                        ],
                        expectedTotalExcludingDiscount: 192.00M,
                        expectedTotalIncludingDiscount: 188.00M,
                        testName: "Multi Apply - multiple product runs twice - enough products to run again without limit"
                    )
                ];

            return setupData;
        }
    }
}
