using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Common.Tests.ExtenstionsTests.CollectionExtensionsTests.DictionaryExtensionsTests.TestData
{
    /// <summary>
    /// The base copy from data.
    /// </summary>
    public class BaseCopyFromTestData
    {
        /// <summary>
        /// Generates dictionary.
        /// </summary>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <typeparam name="TValue">The value type.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns>The dictionary.</returns>
        protected Dictionary<TKey, TValue> GenerateDictionary<TKey, TValue>(List<(TKey, TValue)> data) where TKey : notnull
        {
            Dictionary<TKey, TValue> dicionary = new Dictionary<TKey, TValue>();

            // check data passed in.
            if (data != null)
            {
                // loop through data.
                foreach ((TKey, TValue) item in data)
                {
                    // check item not already in dictionary.
                    if (!dicionary.ContainsKey(item.Item1))
                    {
                        // add to dictionary.
                        dicionary.Add(item.Item1, item.Item2);
                    }
                }
            }

            return dicionary;
        }

        protected TestCaseData GenerateTest<TKey, TValue>(List<(TKey, TValue)> data, string testName) where TKey : notnull
        {
            return this.GenerateTest(data, null, null, testName);
        }

        protected TestCaseData GenerateTest<TKey, TValue>(List<(TKey, TValue)> data, Func<TValue, TValue>? transformValueFunc, List<(TKey, TValue)>? expectedData, string testName) where TKey : notnull
        {
            Dictionary<TKey, TValue> dicData = this.GenerateDictionary(data);

            Dictionary<TKey, TValue> dicExpepcted;

            // check iif expected data explicitly passed in.
            if (expectedData != null)
            {
                // use expected data.
                dicExpepcted = this.GenerateDictionary(expectedData);
            }
            else
            {
                // use source data.
                dicExpepcted = dicData;
            }

            return new TestCaseData(dicData, transformValueFunc, dicExpepcted).SetName(testName);
        }
    }
}
