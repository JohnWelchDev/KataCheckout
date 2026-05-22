using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Entities.Products;
using KataCheckout.Tests.Common.Offers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData.ExecuteOfferTestData
{
    /// <summary>
    /// The execute offer test data.
    /// </summary>
    public class ExecuteOfferSingleProductTestData : BaseExecuteOfferTestData, IEnumerable
    {
        /// <summary>
        /// Gets enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            IEnumerable<ExecuteOfferTestSetupData> testData = this.SetupTestData();

            return this.GenerateTests(testData);
        }

        /// <summary>
        /// Sets up test data.
        /// </summary>
        /// <returns>The test data.</returns>
        private IEnumerable<ExecuteOfferTestSetupData> SetupTestData()
        {
            ExecuteOfferResult result = new ExecuteOfferResult();

            List<ExecuteOfferTestSetupData> listData =
                [
                    // PRODUCT GROUP OVERRIDE
                    ExecuteOfferTestSetupData.SetupData (
                        offerID: 1,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3)],
                        mode: OfferExecutionMode.CostOverrideForProductsTotal,
                        discountValue: 10.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 2.00M,
                        expectedLog: "Test log",
                        expectedAffectedLineItems: [("A", 4.00M, 3)],
                        expectedUnaffectedItems: [("A", 4.00M, 1), ("B", 8.99M, 2)],
                        testName: "Product Total Override Discount - less than total - no limit - more items than limit"
                    ),
                    ExecuteOfferTestSetupData.SetupData (
                        offerID: 1,
                        cartLineData: [("A", 4.00M, 3), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3)],
                        mode: OfferExecutionMode.CostOverrideForProductsTotal,
                        discountValue: 10.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 2.00M,
                        expectedLog: "Test log",
                        expectedAffectedLineItems: [("A", 4.00M, 3)],
                        expectedUnaffectedItems: [("B", 8.99M, 2)],
                        testName: "Product Total Override Discount - less than total - no limit - same num items as limit"
                    ),
                    ExecuteOfferTestSetupData.SetupData (
                        offerID: 1,
                        cartLineData: [("A", 4.00M, 2), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3)],
                        mode: OfferExecutionMode.CostOverrideForProductsTotal,
                        discountValue: 6.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 2.00M,
                        expectedLog: "Test log",
                        expectedAffectedLineItems: [("A", 4.00M, 2)],
                        expectedUnaffectedItems: [("B", 8.99M, 2)],
                        testName: "Product Total Override Discount - less than total - no limit - less items than limit"
                    ),


                    ExecuteOfferTestSetupData.SetupData (
                        offerID: 1,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3)],
                        mode: OfferExecutionMode.CostOverrideForProductsTotal,
                        discountValue: 10.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: 2,
                        expectedDiscountAmount: 2.00M,
                        expectedLog: "Test log",
                        expectedAffectedLineItems: [("A", 4.00M, 3)],
                        expectedUnaffectedItems: [("A", 4.00M, 1), ("B", 8.99M, 2)],
                        testName: "Product Total Override Discount - less than total - 2 item limit - more items than limit"
                    ),
                    ExecuteOfferTestSetupData.SetupData (
                        offerID: 1,
                        cartLineData: [("A", 4.00M, 3), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3)],
                        mode: OfferExecutionMode.CostOverrideForProductsTotal,
                        discountValue: 10.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: 2,
                        expectedDiscountAmount: 2.00M,
                        expectedLog: "Test log",
                        expectedAffectedLineItems: [("A", 4.00M, 3)],
                        expectedUnaffectedItems: [("B", 8.99M, 2)],
                        testName: "Product Total Override Discount - less than total - 2 item limit - same num items as limit"
                    ),
                    ExecuteOfferTestSetupData.SetupData (
                        offerID: 1,
                        cartLineData: [("A", 4.00M, 2), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3)],
                        mode: OfferExecutionMode.CostOverrideForProductsTotal,
                        discountValue: 6.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: 2,
                        expectedDiscountAmount: 2.00M,
                        expectedLog: "Test log",
                        expectedAffectedLineItems: [("A", 4.00M, 2)],
                        expectedUnaffectedItems: [("B", 8.99M, 2)],
                        testName: "Product Total Override Discount - less than total - 2 item limit - less items than limit"
                    )

                ];

            return listData;
        }
    }
}
