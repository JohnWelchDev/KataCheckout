using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Interfaces
{
    /// <summary>
    /// The mergeable.
    /// </summary>
    /// <typeparam name="T">The object type.</typeparam>
    public interface IMergeable<T>
    {
        /// <summary>
        /// Merges item into object.
        /// </summary>
        /// <param name="item">The item from which to merge values in.</param>
        void Merge(T item);
    }
}
