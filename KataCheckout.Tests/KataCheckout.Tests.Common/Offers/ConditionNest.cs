using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Tests.Common.Offers
{
    /// <summary>
    /// The condition nest.
    /// </summary>
    public class ConditionNest
    {
        public ConditionNest()
        {
            this.Units = new List<(string, string, int)>();
            this.ChildNests = new List<ConditionNest>();
        }

        /// <summary>
        /// Gets or sets the units
        /// </summary>
        public List<(string, string, int)> Units { get; init; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to invert the evaluation result.
        /// </summary>
        public bool Invert { get; set; }

        /// <summary>
        /// Gets or sets the child nests.
        /// </summary>
        public List<ConditionNest> ChildNests { get; init; }
    }
}
