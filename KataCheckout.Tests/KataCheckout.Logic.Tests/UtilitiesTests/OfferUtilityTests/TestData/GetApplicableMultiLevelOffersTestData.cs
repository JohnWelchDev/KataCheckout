using KataCheckout.Tests.Common.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Tests.UtilitiesTests.OfferUtilityTests.TestData
{
    /// <summary>
    /// Gets applicable offers data for multi level offers.
    /// </summary>
    internal class GetApplicableMultiLevelOffersTestData : BaseGetApplicableOffersTestData, IEnumerable
    {
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            yield return this.GenerateTest(
                ["A", "B", "C", "D"],
                [
                    new SkuNest { SKUs = ["A"], ChildLevels = [new SkuNest { SKUs = ["B"] }] },
                    new SkuNest { SKUs = ["B"], ChildLevels = [new SkuNest { SKUs = ["A"] }] },
                    new SkuNest { SKUs = ["B"], ChildLevels = [new SkuNest { SKUs = ["A", "C"] }] },
                    new SkuNest { SKUs = ["B"], ChildLevels = [new SkuNest { SKUs = ["A", "C"] }, new SkuNest { ChildLevels = [new SkuNest { SKUs = ["D"] }] }] },
                ],
                [0, 1, 2, 3],
                "Multi Level Product Offers - all match");

            yield return this.GenerateTest(
                ["A", "B", "C", "D", "E"],
                [
                    new SkuNest { SKUs = ["A"], ChildLevels = [new SkuNest { SKUs = ["B"] }] },
                    new SkuNest { SKUs = ["B"], ChildLevels = [new SkuNest { SKUs = ["A"] }] },
                    new SkuNest { SKUs = ["B"], ChildLevels = [new SkuNest { SKUs = ["A", "C"] }] },
                    new SkuNest { SKUs = ["B"], ChildLevels = [new SkuNest { SKUs = ["A", "C"] }, new SkuNest { ChildLevels = [new SkuNest { SKUs = ["D"] }] }] },
                ],
                [0, 1, 2, 3],
                "Multi Level Product Offers - all match, excess producs");

            yield return this.GenerateTest(
                ["A", "B", "E"],
                [
                    new SkuNest { SKUs = ["A"], ChildLevels = [new SkuNest { SKUs = ["B"] }] },
                    new SkuNest { SKUs = ["B"], ChildLevels = [new SkuNest { SKUs = ["A"] }] },
                    new SkuNest { SKUs = ["D"], ChildLevels = [new SkuNest { SKUs = ["F", "C"] }] },
                    new SkuNest { SKUs = ["B"], ChildLevels = [new SkuNest { SKUs = ["A", "C"] }, new SkuNest { ChildLevels = [new SkuNest { SKUs = ["D"] }] }] },
                ],
                [0, 1, 3],
                "Multi Level Product Offers - not all match, less producs");

            yield return this.GenerateTest(
                ["A", "B", "C", "D"],
                [
                    new SkuNest { SKUs = ["A"], ChildLevels = [new SkuNest { SKUs = ["B"] }] },
                    new SkuNest { SKUs = ["B"], ChildLevels = [new SkuNest { SKUs = ["F"] }] },
                    new SkuNest { SKUs = ["B"], ChildLevels = [new SkuNest { SKUs = ["H", "I"] }] },
                    new SkuNest { SKUs = ["B"], ChildLevels = [new SkuNest { SKUs = ["A", "C"] }, new SkuNest { ChildLevels = [new SkuNest { SKUs = ["D"] }] }] },
                ],
                [0, 1, 2, 3],
                "Multi Level Product Offers - not all match");

            yield return this.GenerateTest(
                ["F", "G", "H", "I"],
                [
                    new SkuNest { SKUs = ["A"], ChildLevels = [new SkuNest { SKUs = ["B"] }] },
                    new SkuNest { SKUs = ["B"], ChildLevels = [new SkuNest { SKUs = ["A"] }] },
                    new SkuNest { SKUs = ["B"], ChildLevels = [new SkuNest { SKUs = ["A", "C"] }] },
                    new SkuNest { SKUs = ["B"], ChildLevels = [new SkuNest { SKUs = ["A", "C"] }, new SkuNest { ChildLevels = [new SkuNest { SKUs = ["D"] }] }] },
                ],
                [0, 1, 2, 3],
                "Multi Level Product Offers - no matches");
        }
    }
}
