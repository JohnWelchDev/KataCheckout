using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Offers
{
    /// <summary>
    /// The operator codes.
    /// </summary>
    public static class OperatorCodes
    {
        /// <summary>
        /// The operator code for equal to.
        /// </summary>
        public const string Equals = "=";

        /// <summary>
        /// The operator code for less than.
        /// </summary>
        public const string LessThan = "<";

        /// <summary>
        /// The operator code for less than or equal to.
        /// </summary>
        public const string LessThanOrEqual = "<=";

        /// <summary>
        /// The operator code for greater than.
        /// </summary>
        public const string GreaterThan = ">";

        /// <summary>
        /// The operator code for greater than or equal to.
        /// </summary>
        public const string GreaterThanOrEqual = ">=";
    }
}
