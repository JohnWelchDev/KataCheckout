using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Offers
{
    /// <summary>
    /// The special offer.
    /// </summary>
    public class SpecialOffer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialOffer" /> class.
        /// </summary>
        public SpecialOffer()
        {
            this.Conditions = new List<OfferCondition>();
        }

        /// <summary>
        /// Gets or sets the conditions to be met for the offer to apply.
        /// </summary>
        public List<OfferCondition> Conditions { get; set; }
    }
}
