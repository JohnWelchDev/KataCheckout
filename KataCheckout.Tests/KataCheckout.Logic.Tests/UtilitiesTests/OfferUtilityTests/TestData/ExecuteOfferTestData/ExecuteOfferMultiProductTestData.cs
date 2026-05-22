using KataCheckout.Entities.Offers;
using KataCheckout.Tests.Common.Offers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData.ExecuteOfferTestData
{
    /// <summary>
    /// The execute offer for multiple product types test data.
    /// </summary>
    public class ExecuteOfferMultiProductTestData : BaseExecuteOfferTestData, IEnumerable
    {
        /// <summary>
        /// Gets the enumerator.
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
                        affectedProducts: [("A", 3), ("B", 1)],
                        mode: OfferExecutionMode.CostOverrideForProductsTotal,
                        discountValue: 10.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 10.99M,
                        expectedLog: "Test log",
                        expectedAffectedLineItems: [("A", 4.00M, 3), ("B", 8.99M, 1)],
                        expectedUnaffectedItems: [("A", 4.00M, 1), ("B", 8.99M, 1)],
                        testName: "Product Total Override Discount - less than total - multi affected - more items than limit"
                    ),
                    ExecuteOfferTestSetupData.SetupData (
                        offerID: 1,
                        cartLineData: [("A", 4.00M, 3), ("B", 8.99M, 1)],
                        affectedProducts: [("A", 3), ("B", 1)],
                        mode: OfferExecutionMode.CostOverrideForProductsTotal,
                        discountValue: 10.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 10.99M,
                        expectedLog: "Test log",
                        expectedAffectedLineItems: [("A", 4.00M, 3), ("B", 8.99M, 1)],
                        expectedUnaffectedItems: [],
                        testName: "Product Total Override Discount - less than total - multi affected - same num items as limit"
                    ),
                    ExecuteOfferTestSetupData.SetupData (
                        offerID: 1,
                        cartLineData: [("A", 4.00M, 3), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 5), ("B", 5)],
                        mode: OfferExecutionMode.CostOverrideForProductsTotal,
                        discountValue: 25.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 4.98M,
                        expectedLog: "Test log",
                        expectedAffectedLineItems: [("A", 4.00M, 3), ("B", 8.99M, 2)],
                        expectedUnaffectedItems: [],
                        testName: "Product Total Override Discount - less than total - multi affected - less items than limit"
                    )
                ];

            return listData;
        }
    }
}
