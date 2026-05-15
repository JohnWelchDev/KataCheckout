using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Interfaces
{
    /// <summary>
    /// Defines contract for object containing extractable list of items.
    /// </summary>
    public interface ISkuExtractable
    {
        /// <summary>
        /// Extracts product skus.
        /// </summary>
        /// <returns>The product skus.</returns>
        IEnumerable<string> ExtractSkus();
    }
}
