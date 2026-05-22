using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Entities.Products;
using KataCheckout.Tests.Common.Offers;
using System.Collections;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData.ExecuteOfferTestData
{
    /// <summary>
    /// The base execute offer test data.
    /// </summary>
    public abstract class BaseExecuteOfferTestData
    {
        public IEnumerator GenerateTests(IEnumerable<ExecuteOfferTestSetupData> testData)
        {
            int offerID = 1;

            // loop through setup data.
            foreach (ExecuteOfferTestSetupData data in testData)
            {
                Dictionary<string, CartLineItem> cartLines = OfferTestUtility.GetCartLines(data.CartLineData ?? new List<(string, decimal, int)>());
                List<(string, int?)> affectedProducts = data.AffectedProducts ?? new List<(string, int?)>();
                OfferExecutionMode mode = data.Mode;
                decimal discountAmount = data.DiscountValue;
                ConditionNest? conditionNest = data.ConditionNest;
                int? limitPerCheckout = data.LimitPerCheckout;
                decimal expectedAmount = data.ExpectedDiscountAmount;
                string? expectedLog = data.ExpectedLog;

                Dictionary<string, CartLineItem> expectedAffectedProducts = new Dictionary<string, CartLineItem>();

                // check expected affected line items set.
                if (data.ExpectedAffectedLineItems != null && data.ExpectedAffectedLineItems.Count > 0)
                {
                    // loop though expected line items.
                    foreach ((string, decimal, int) item in data.ExpectedAffectedLineItems)
                    {
                        // check item exists.
                        if (expectedAffectedProducts.ContainsKey(item.Item1))
                        {
                            // add to existing count.
                            expectedAffectedProducts[item.Item1].NumUnits += item.Item3;
                        }
                        else
                        {
                            // create cart line item.
                            Product product = new Product();
                            product.SKU = item.Item1;
                            product.UnitPrice = item.Item2;

                            CartLineItem lineItem = new CartLineItem(product, item.Item3);

                            // add to dictionary.
                            expectedAffectedProducts.Add(item.Item1, lineItem);
                        }
                    }
                }

                Dictionary<string, CartLineItem> expectedUnaffectedProducts = new Dictionary<string, CartLineItem>();

                // check expected affected line items set.
                if (data.ExpectedUnaffectedLineItems != null && data.ExpectedUnaffectedLineItems.Count > 0)
                {
                    // loop though expected line items.
                    foreach ((string, decimal, int) item in data.ExpectedUnaffectedLineItems)
                    {
                        // check item exists.
                        if (expectedUnaffectedProducts.ContainsKey(item.Item1))
                        {
                            // add to existing count.
                            expectedUnaffectedProducts[item.Item1].NumUnits += item.Item3;
                        }
                        else
                        {
                            // create cart line item.
                            Product product = new Product();
                            product.SKU = item.Item1;
                            product.UnitPrice = item.Item2;

                            CartLineItem lineItem = new CartLineItem(product, item.Item3);

                            // add to dictionary.
                            expectedUnaffectedProducts.Add(item.Item1, lineItem);
                        }
                    }
                }

                string? testName = data.TestName;

                SpecialOffer offer = OfferTestUtility.GetSpecialOffer(offerID, affectedProducts, mode, discountAmount, limitPerCheckout, conditionNest);

                yield return new TestCaseData(cartLines, offer, expectedAmount, expectedLog, expectedAffectedProducts, expectedUnaffectedProducts).SetName(testName?.Replace(".", "\u2024"));

                // increment offer id.
                offerID++;
            }
        }
    }
}
