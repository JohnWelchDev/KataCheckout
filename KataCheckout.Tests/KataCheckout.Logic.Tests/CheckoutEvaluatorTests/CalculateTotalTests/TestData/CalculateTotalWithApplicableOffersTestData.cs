using KataCheckout.Entities.Offers;
using KataCheckout.Tests.Common.Offers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.CheckoutEvaluatorTests.CalculateTotalTests.TestData
{
    public class CalculateTotalWithApplicableOffersTestData : BaseCalculateTotalTestData, IEnumerable
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
                        cartLines: [("A", 15.00M, 1)],
                        offerTestData:
                        [
                            ExecuteOfferTestSetupData.SetupData (
                                offerID: 1,
                                affectedProducts: [("A", 1)],
                                mode: OfferExecutionMode.CostOverrideForProductsTotal,
                                discountValue: 10.00M,
                                conditionNest: new ConditionNest { Units = [("A", ">=", 1)], RelationCode = RelationCodes.OR, Invert = false },
                                limitPerCheckout: null
                            )
                        ],
                        expectedTotalExcludingDiscount: 15.00M,
                        expectedTotalIncludingDiscount: 10.00M,
                        testName: "Only matching offer - exact execution rule match"
                    ),
                    CalculateTotalTestSetupData.Setup(
                        cartLines: [("A", 15.00M, 4)],
                        offerTestData:
                        [
                            ExecuteOfferTestSetupData.SetupData (
                                offerID: 1,
                                affectedProducts: [("A", 3)],
                                mode: OfferExecutionMode.CostOverrideForProductsTotal,
                                discountValue: 30.00M,
                                conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                                limitPerCheckout: null
                            )
                        ],
                        expectedTotalExcludingDiscount: 60.00M,
                        expectedTotalIncludingDiscount: 45.00M,
                        testName: "Only matching offer - more than execution rule limit"
                    ),
                    CalculateTotalTestSetupData.Setup(
                        cartLines: [("A", 15.00M, 2)],
                        offerTestData:
                        [
                            ExecuteOfferTestSetupData.SetupData (
                                offerID: 1,
                                affectedProducts: [("A", 3)],
                                mode: OfferExecutionMode.CostOverrideForProductsTotal,
                                discountValue: 25.00M,
                                conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                                limitPerCheckout: null
                            )
                        ],
                        expectedTotalExcludingDiscount: 30.00M,
                        expectedTotalIncludingDiscount: 25.00M,
                        testName: "Only matching offer - less than execution rule limit"
                    ),
                    CalculateTotalTestSetupData.Setup(
                        cartLines: [("A", 15.00M, 2), ("B", 8.99M, 1)],
                        offerTestData:
                        [
                            ExecuteOfferTestSetupData.SetupData (
                                offerID: 1,
                                affectedProducts: [("A", 2), ("B", 1)],
                                mode: OfferExecutionMode.CostOverrideForProductsTotal,
                                discountValue: 30.00M,
                                conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                                limitPerCheckout: null
                            )
                        ],
                        expectedTotalExcludingDiscount: 38.99M,
                        expectedTotalIncludingDiscount: 30.00M,
                        testName: "Only matching offer - multi product execution rule - on limit for all products"
                    ),
                    CalculateTotalTestSetupData.Setup(
                        cartLines: [("A", 15.00M, 1), ("B", 8.99M, 1)],
                        offerTestData:
                        [
                            ExecuteOfferTestSetupData.SetupData (
                                offerID: 1,
                                affectedProducts: [("A", 2), ("B", 2)],
                                mode: OfferExecutionMode.CostOverrideForProductsTotal,
                                discountValue: 20.00M,
                                conditionNest: new ConditionNest { Units = [("A", ">=", 1)], RelationCode = RelationCodes.OR, Invert = false },
                                limitPerCheckout: null
                            )
                        ],
                        expectedTotalExcludingDiscount: 23.99M,
                        expectedTotalIncludingDiscount: 20.00M,
                        testName: "Only matching offer - multi product execution rule - products under limits"
                    ),
                    CalculateTotalTestSetupData.Setup(
                        cartLines: [("A", 15.00M, 4), ("B", 8.50M, 3)],
                        offerTestData:
                        [
                            ExecuteOfferTestSetupData.SetupData (
                                offerID: 1,
                                affectedProducts: [("A", 2), ("B", 2)],
                                mode: OfferExecutionMode.CostOverrideForProductsTotal,
                                discountValue: 32.00M,
                                conditionNest: new ConditionNest { Units = [("A", ">=", 1)], RelationCode = RelationCodes.OR, Invert = false },
                                limitPerCheckout: null
                            )
                        ],
                        expectedTotalExcludingDiscount: 85.50M,
                        expectedTotalIncludingDiscount: 70.50M,
                        testName: "Only matching offer - multi product execution rule - products over limits"
                    ),
                    CalculateTotalTestSetupData.Setup(
                        cartLines: [("A", 15.00M, 1), ("B", 8.50M, 3)],
                        offerTestData:
                        [
                            ExecuteOfferTestSetupData.SetupData (
                                offerID: 1,
                                affectedProducts: [("A", 2), ("B", 2)],
                                mode: OfferExecutionMode.CostOverrideForProductsTotal,
                                discountValue: 28.50M,
                                conditionNest: new ConditionNest { Units = [("A", ">=", 1)], RelationCode = RelationCodes.OR, Invert = false },
                                limitPerCheckout: null
                            )
                        ],
                        expectedTotalExcludingDiscount: 40.50M,
                        expectedTotalIncludingDiscount: 37.00M,
                        testName: "Only matching offer - multi product execution rule - products over and under limits"
                    ),
                    CalculateTotalTestSetupData.Setup(
                        cartLines: [("A", 15.00M, 3), ("C", 8.50M, 3)],
                        offerTestData:
                        [
                            ExecuteOfferTestSetupData.SetupData (
                                offerID: 1,
                                affectedProducts: [("A", 2), ("B", 2)],
                                mode: OfferExecutionMode.CostOverrideForProductsTotal,
                                discountValue: 25.00M,
                                conditionNest: new ConditionNest { Units = [("A", ">=", 1)], RelationCode = RelationCodes.OR, Invert = false },
                                limitPerCheckout: null
                            )
                        ],
                        expectedTotalExcludingDiscount: 70.50M,
                        expectedTotalIncludingDiscount: 65.50M,
                        testName: "Only matching offer - mixed matching product execution rule - products over limits"
                    )
                ];

            return setupData;
        }
    }
}
