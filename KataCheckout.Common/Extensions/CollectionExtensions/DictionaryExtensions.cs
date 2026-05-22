using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace KataCheckout.Common.Extensions.CollectionExtensions
{
    /// <summary>
    /// The dictionary extensions.
    /// </summary>
    public static class DictionaryExtensions
    {
        extension<Key, Value>(Dictionary<Key, Value> target) where Key : notnull
        {
            /// <summary>
            /// Copies contents of one dictionary to another.
            /// </summary>
            /// <param name="source">The source.</param>
            public void CopyFrom(Dictionary<Key, Value> source)
            {
                // check source passed in.
                if (source != null)
                {
                    // call copy function with one to one transformation.
                    CopyFrom(target, source, x => x);
                }
            }

            /// <summary>
            /// Copies contents of one dictionary to another.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="valueTransformationFunc">The value transformation function.</param>
            public void CopyFrom(Dictionary<Key, Value> source, Func<Value, Value> valueTransformationFunc)
            {
                // check the source.
                if (source != null)
                {
                    // loop through source dictionary.
                    foreach (Key key in source.Keys)
                    {
                        Value value = source[key];
                        Value targetValue;

                        // check for transformation function.
                        if (valueTransformationFunc != null)
                        {
                            targetValue = valueTransformationFunc.Invoke(value);
                        }
                        else
                        {
                            targetValue = value;
                        }

                        // add to target dictionary.
                        target.Add(key, targetValue);
                    }
                }
            }
        }
    }
}
