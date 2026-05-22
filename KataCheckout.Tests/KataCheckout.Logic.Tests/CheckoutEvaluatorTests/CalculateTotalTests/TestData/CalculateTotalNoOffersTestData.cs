using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Tests.Common.Offers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.CheckoutEvaluatorTests.CalculateTotalTests.TestData
{
    public class CalculateTotalNoOffersTestData : BaseCalculateTotalTestData, IEnumerable
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

        private IEnumerable<CalculateTotalTestSetupData> GenerateTestData()
        {
            List<CalculateTotalTestSetupData> setupData =
                [
                    CalculateTotalTestSetupData.Setup(
                        cartLines: [("A", 4.99M, 1)],
                        /*
                        offerTestData:
                        [
                            ExecuteOfferTestSetupData.SetupData (
                                offerID: 1,
                                cartLineData: [("A", 4.00M, 3), ("B", 8.99M, 1)],
                                affectedProducts: [("A", 3), ("B", 1)],
                                mode: OfferExecutionMode.CostOverrideForProductsTotal,
                                discountValue: 10.00M,
                                conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                                limitPerCheckout: null
                            )
                        ],
                        */
                        expectedTotalExcludingDiscount: 4.99M,
                        expectedTotalIncludingDiscount: 4.99M,
                        testName: "Single product type, single product - no discount"
                    ),
                    CalculateTotalTestSetupData.Setup(
                        cartLines: [("A", 4.99M, 2)],
                        expectedTotalExcludingDiscount: 9.98M,
                        expectedTotalIncludingDiscount: 9.98M,
                        testName: "Single product type, multile units - no discount"
                    ),
                    CalculateTotalTestSetupData.Setup(
                        cartLines: [("A", 4.99M, 1), ("B", 3.50M, 1)],
                        expectedTotalExcludingDiscount: 8.49M,
                        expectedTotalIncludingDiscount: 8.49M,
                        testName: "multiple products, single units - no discount"
                    ),
                    CalculateTotalTestSetupData.Setup(
                        cartLines: [("A", 4.99M, 2), ("B", 3.50M, 4)],
                        expectedTotalExcludingDiscount: 23.98M,
                        expectedTotalIncludingDiscount: 23.98M,
                        testName: "multiple products, multiple units - no discount"
                    )
                ];

            return setupData;
        }
    }
}
