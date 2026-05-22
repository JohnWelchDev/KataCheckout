using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Logic.Utilities;
using KataCheckout.Tests.Common.Offers;
using System.Collections;
using System.ComponentModel;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData.CalculateDiscountTestData
{
    /// <summary>
    /// The calculate discount test data.
    /// </summary>
    public class CalculateDiscountTestData : IEnumerable
    {
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            IEnumerable tests = GenerateTests();

            return tests.GetEnumerator();
        }

        private IEnumerable GenerateTests()
        {
            List<DiscountTestSetupData> listData =
                [
                    // FLAT AMOUNT - ALL PRODUCTS
                    DiscountTestSetupData.SetupData (
                        offerID: 1,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3)],
                        mode: OfferExecutionMode.FlatTotalDiscount,
                        discountValue: 10.50M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 10.50M,
                        expectedLog: "Test log",
                        testName: "Flat Discount - less than total"
                    ),
                    DiscountTestSetupData.SetupData (
                        offerID: 2,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3)],
                        mode: OfferExecutionMode.FlatTotalDiscount,
                        discountValue: 33.98M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 33.98M,
                        expectedLog: "Test log",
                        testName: "Flat Discount - same as total"
                    ),
                    DiscountTestSetupData.SetupData (
                        offerID: 3,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3)],
                        mode: OfferExecutionMode.FlatTotalDiscount,
                        discountValue: 50.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 0.00M,
                        expectedLog: "Test log",
                        testName: "Flat Discount - more than total"
                    ),

                    // FLAT PERCENTAGE - ALL PRODUCTS
                    DiscountTestSetupData.SetupData (
                        offerID: 4,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 4), ("B", 2)],
                        mode: OfferExecutionMode.FlatPercentageDiscount,
                        discountValue: 10.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 3.40M,
                        expectedLog: "Test log",
                        testName: "Flat Percentage Discount - 10%"
                    ),
                    DiscountTestSetupData.SetupData (
                        offerID: 3,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 4), ("B", 2)],
                        mode: OfferExecutionMode.FlatPercentageDiscount,
                        discountValue: 100.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 33.98M,
                        expectedLog: "Test log",
                        testName: "Flat Percentage Discount - 100%"
                    ),
                    DiscountTestSetupData.SetupData (
                        offerID: 3,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 4), ("B", 2)],
                        mode: OfferExecutionMode.FlatPercentageDiscount,
                        discountValue: 120.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 0.00M,
                        expectedLog: "Test log",
                        testName: "Flat Percentage Discount - 120%"
                    ),

                    // OVERRIDE GROUP PRODUCT TOTAL
                    DiscountTestSetupData.SetupData (
                        offerID: 4,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3)],
                        mode: OfferExecutionMode.CostOverrideForProductsTotal,
                        discountValue: 30.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 3.98M,
                        expectedLog: "Test log",
                        testName: "Cost override for products 30.00 - less than total"
                    ),
                    DiscountTestSetupData.SetupData (
                        offerID: 5,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3)],
                        mode: OfferExecutionMode.CostOverrideForProductsTotal,
                        discountValue: 33.98M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 0.00M,
                        expectedLog: "Test log",
                        testName: "Cost override for products 33.98 - equal to total (no discount)"
                    ),
                    DiscountTestSetupData.SetupData (
                        offerID: 6,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3)],
                        mode: OfferExecutionMode.CostOverrideForProductsTotal,
                        discountValue: 35.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 0.00M,
                        expectedLog: "Test log",
                        testName: "Cost override for products 35.00 - more than total)"
                    ),

                    // FLAT DISCOUNT - CHEAPEST PRODUCT
                    DiscountTestSetupData.SetupData (
                        offerID: 7,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3), ("B", 2)],
                        mode: OfferExecutionMode.CheapestProductFlatDiscount,
                        discountValue: 1.50M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 1.50M,
                        expectedLog: "Test log",
                        testName: "Flat discount cheapest product 1.50 - less than unit price)"
                    ),
                    DiscountTestSetupData.SetupData (
                        offerID: 7,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3), ("B", 2)],
                        mode: OfferExecutionMode.CheapestProductFlatDiscount,
                        discountValue: 4.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 4.00M,
                        expectedLog: "Test log",
                        testName: "Flat discount cheapest product 4.00 - same as unit price)"
                    ),
                    DiscountTestSetupData.SetupData (
                        offerID: 9,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3), ("B", 2)],
                        mode: OfferExecutionMode.CheapestProductFlatDiscount,
                        discountValue: 5.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 0.00M,
                        expectedLog: "Test log",
                        testName: "Flat discount cheapest product 5.00 - more than unit price)"
                    ),

                    // PERCENTAGE DISCOUNT - CHEAPEST PRODUCT
                    DiscountTestSetupData.SetupData (
                        offerID: 10,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3), ("B", 2)],
                        mode: OfferExecutionMode.CheapestProductPercentageDiscount,
                        discountValue: 10.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 0.40M,
                        expectedLog: "Test log",
                        testName: "Percentage discount 10% cheapest product - 0.40)"
                    ),
                    DiscountTestSetupData.SetupData (
                        offerID: 11,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3), ("B", 2)],
                        mode: OfferExecutionMode.CheapestProductPercentageDiscount,
                        discountValue: 100.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 4.00M,
                        expectedLog: "Test log",
                        testName: "Percentage discount 100% cheapest product - 4.00)"
                    ),
                    DiscountTestSetupData.SetupData (
                        offerID: 12,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3), ("B", 2)],
                        mode: OfferExecutionMode.CheapestProductPercentageDiscount,
                        discountValue: 101.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 0.00M,
                        expectedLog: "Test log",
                        testName: "Percentage discount 101% cheapest product - no discount)"
                    ),

                    // FLAT DISCOUNT - EXPENSIVE PRODUCT
                    DiscountTestSetupData.SetupData (
                        offerID: 13,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3), ("B", 2)],
                        mode: OfferExecutionMode.ExpensiveProductFlatDiscount,
                        discountValue: 5.50M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 5.50M,
                        expectedLog: "Test log",
                        testName: "Flat discount most expensive product 5.50 - less than unit price)"
                    ),
                    DiscountTestSetupData.SetupData (
                        offerID: 14,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3), ("B", 2)],
                        mode: OfferExecutionMode.ExpensiveProductFlatDiscount,
                        discountValue: 8.99M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 8.99M,
                        expectedLog: "Test log",
                        testName: "Flat discount most expensive product 8.99 - same as unit price)"
                    ),
                    DiscountTestSetupData.SetupData (
                        offerID: 15,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3), ("B", 2)],
                        mode: OfferExecutionMode.ExpensiveProductFlatDiscount,
                        discountValue: 9.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 0.00M,
                        expectedLog: "Test log",
                        testName: "Flat discount most expensive product 9.00 - more than unit price)"
                    ),

                    // PERCENTAGE DISCOUNT - EXPENSIVE PRODUCT
                    DiscountTestSetupData.SetupData (
                        offerID: 13,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3), ("B", 2)],
                        mode: OfferExecutionMode.ExpensiveProductPercentageDiscount,
                        discountValue: 10.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 0.90M,
                        expectedLog: "Test log",
                        testName: "Percentage discount 10% most expensive product - 0.90)"
                    ),
                    DiscountTestSetupData.SetupData (
                        offerID: 14,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3), ("B", 2)],
                        mode: OfferExecutionMode.ExpensiveProductPercentageDiscount,
                        discountValue: 100.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 8.99M,
                        expectedLog: "Test log",
                        testName: "Percentage discount 100% most expensive product - 8.99)"
                    ),
                    DiscountTestSetupData.SetupData (
                        offerID: 15,
                        cartLineData: [("A", 4.00M, 4), ("B", 8.99M, 2)],
                        affectedProducts: [("A", 3), ("B", 2)],
                        mode: OfferExecutionMode.ExpensiveProductPercentageDiscount,
                        discountValue: 101.00M,
                        conditionNest: new ConditionNest { Units = [("A", ">", 1)], RelationCode = RelationCodes.OR, Invert = false },
                        limitPerCheckout: null,
                        expectedDiscountAmount: 0.00M,
                        expectedLog: "Test log",
                        testName: "Percentage discount 101% most expensive product - no discount)"
                    )

                ];

            // loop through setup data.
            foreach (DiscountTestSetupData data in listData)
            {
                int offerID = data.SpecialOfferID;
                Dictionary<string, CartLineItem> cartLines = OfferTestUtility.GetCartLines(data.CartLineData ?? new List<(string, decimal, int)>());
                List<(string, int?)> affectedProducts = data.AffectedProducts ?? new List<(string, int?)>();
                OfferExecutionMode mode = data.Mode;
                decimal discountAmount = data.DiscountValue;
                ConditionNest? conditionNest = data.ConditionNest;
                int? limitPerCheckout = data.LimitPerCheckout;
                decimal expectedAmount = data.ExpectedDiscountAmount;
                string? expectedLog = data.ExpectedLog;
                string? testName = data.TestName;

                SpecialOffer offer = OfferTestUtility.GetSpecialOffer(offerID, affectedProducts, mode, discountAmount, limitPerCheckout, conditionNest);

                yield return new TestCaseData( cartLines, offer, expectedAmount, expectedLog).SetName(testName?.Replace(".", "\u2024"));
            }
        }
    }
}
