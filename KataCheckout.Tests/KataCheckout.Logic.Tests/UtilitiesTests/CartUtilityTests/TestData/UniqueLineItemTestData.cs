using Castle.Core.Resource;
using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.CartUtilityTests.TestData
{
    public class UniqueLineItemTestData : IEnumerable
    {
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            yield return this.GenerateTestData([("A", 1), ("B", 3), ("C", 2), ("D", 1)], [("A", 1), ("B", 3), ("C", 2), ("D", 1)], "Unique skus");
            yield return this.GenerateTestData([("A", 1), ("A", 3), ("C", 2), ("D", 1)], [("A", 4), ("C", 2), ("D", 1)], "One duplicate sku");
            yield return this.GenerateTestData([("A", 1), ("B", 3), ("B", 2), ("A", 1)], [("A", 2), ("B", 5)], "Multiple duplicate skus");
            yield return this.GenerateTestData([("A", 1), ("A", 3), ("A", 2), ("A", 1)], [("A", 7)], "all duplicate skus");
        }

        private TestCaseData GenerateTestData(IEnumerable<(string, int)> items, IEnumerable<(string, int)> expectedData, string testName)
        {
            List<CartLineItem> cartLineItems = this.GetCartLineItems(items);

            Dictionary<string, CartLineItem> expectedResults = new Dictionary<string, CartLineItem>();

            // loop through constructing expected data.
            foreach ((string, int) data in expectedData)
            {
                CartLineItem item = new CartLineItem(new Product { SKU = data.Item1, UnitPrice = 4.99M }, data.Item2);
                expectedResults.Add(data.Item1, item);
            }

            return new TestCaseData(cartLineItems, expectedResults).SetName(testName);
        }

        private List<CartLineItem> GetCartLineItems(IEnumerable<(string, int)> items)
        {
            List<CartLineItem> cartLineItems = new List<CartLineItem>();

            foreach ((string, int) item in items)
            {
                CartLineItem cartLineItem = new CartLineItem(new Product { SKU = item.Item1, UnitPrice = 4.99M }, item.Item2);

                cartLineItems.Add(cartLineItem);
            }


            return cartLineItems;
        }

    }
}
