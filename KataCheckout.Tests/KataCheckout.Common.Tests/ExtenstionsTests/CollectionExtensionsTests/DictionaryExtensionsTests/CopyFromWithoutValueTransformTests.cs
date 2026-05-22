using KataCheckout.Common.Extensions.CollectionExtensions;
using KataCheckout.Common.Tests.ExtenstionsTests.CollectionExtensionsTests.DictionaryExtensionsTests.TestData;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Common.Tests.ExtenstionsTests.CollectionExtensionsTests.DictionaryExtensionsTests
{
    /// <summary>
    /// The copy from without transform value function tests.
    /// </summary>
    public class CopyFromWithoutValueTransformTests
    {
        [TestCaseSource(typeof(CopyFromNoTransformTestData))]
        public async Task FillsDictionaryCorrectlyWithoutTransform<Tkey, TValue>(Dictionary<Tkey, TValue> sourceDictionary, Func<TValue, TValue>? transformValueFunc, Dictionary<Tkey, TValue> expectedDictionary) where Tkey : notnull
        {
            await this.ExecuteTest(sourceDictionary, transformValueFunc, expectedDictionary);
        }

        [TestCaseSource(typeof(CopyFromWithValueTransformTestData))]
        public async Task FillsDictionaryCorrectlyWithTransform<Tkey, TValue>(Dictionary<Tkey, TValue> sourceDictionary, Func<TValue, TValue>? transformValueFunc, Dictionary<Tkey, TValue> expectedDictionary) where Tkey : notnull
        {
            await this.ExecuteTest(sourceDictionary, transformValueFunc, expectedDictionary);
        }

        public async Task ExecuteTest<Tkey, TValue>(Dictionary<Tkey, TValue> sourceDictionary, Func<TValue, TValue>? transformValueFunc, Dictionary<Tkey, TValue> expectedDictionary) where Tkey : notnull
        {
            Dictionary<Tkey, TValue> target = new Dictionary<Tkey, TValue>();

            // copy dicitonary contents.
            if (transformValueFunc != null)
            {
                target.CopyFrom(sourceDictionary, transformValueFunc);
            }
            else
            {
                target.CopyFrom(sourceDictionary);
            }

            Assert.That(target.Count, Is.EqualTo(expectedDictionary.Count), "Dictionary count mismatch");

            // loop through keys in source dictionary.
            foreach (Tkey key in expectedDictionary.Keys)
            {
                // check key is in dictionary.
                Assert.That(target.ContainsKey(key), Is.True, "Expected key not found in dictionary");

                Assert.That(target[key], Is.EqualTo(expectedDictionary[key]), "Values not equal");
            }
        }
    }
}
