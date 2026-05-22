using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Common.Tests.ExtenstionsTests.CollectionExtensionsTests.DictionaryExtensionsTests.TestData
{
    /// <summary>
    /// The copy from no transform test data.
    /// </summary>
    public class CopyFromNoTransformTestData : BaseCopyFromTestData, IEnumerable
    {
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {

            yield return this.GenerateTest<int, string>([(1, "A"), (2, "B"), (3, "C")], "Dictionary<int, string> - populates");
            yield return this.GenerateTest<string, decimal>([("A", 5.00M), ("B", 3.99M), ("C", 8.2M)], "Dictionary<string, decimal> - populates");
            yield return this.GenerateTest<long, bool>([(60089, true), (473476534, false), (5, false), (3856936, true)], "Dictionary<long, bool> - populates");
        }
    }
}
