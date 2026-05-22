using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Common.Tests.ExtenstionsTests.CollectionExtensionsTests.DictionaryExtensionsTests.TestData
{
    public class CopyFromWithValueTransformTestData : BaseCopyFromTestData, IEnumerable
    {
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            yield return this.GenerateTest<int, string>([(1, "A"), (2, "B"), (3, "C")], x => x.ToLower(), [(1, "a"), (2, "b"), (3, "c")], "with transform - Dictionary<int, string> - populates");
            yield return this.GenerateTest<string, decimal>([("A", 5.00M), ("B", 3.99M), ("C", 8.2M)], x => x * 2, [("A", 10.00M), ("B", 7.98M), ("C", 16.4M)], "with transform - Dictionary<string, decimal> - populates");
            yield return this.GenerateTest<long, bool>([(60089, true), (473476534, false), (5, false), (3856936, true)], x => !x, [(60089, false), (473476534, true), (5, true), (3856936, false)], "with transform - Dictionary<long, bool> - populates");
        }
    }
}
