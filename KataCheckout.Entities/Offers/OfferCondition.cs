using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Offers
{
    /// <summary>
    /// The offer condition.
    /// </summary>
    public class OfferCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfferCondition" /> class.
        /// </summary>
        public OfferCondition()
        {
            this.UnitConditions = new List<ProductConditionUnit>();
            this.ChildOfferConditions = new List<OfferCondition>();
            this.RelationCode = RelationCodes.AND;
        }

        /// <summary>
        /// Gets or sets the units.
        /// </summary>
        public List<ProductConditionUnit> UnitConditions { get; set; }

        /// <summary>
        /// Gets or sets the offer condition.
        /// </summary>
        public List<OfferCondition> ChildOfferConditions { get; set; }

        /// <summary>
        /// Gets or sets the code defining relationship between the units
        /// that forms the evaluation (&& = all unit conditions must be met, || = 1 condition must be met).
        /// </summary>
        public string RelationCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to invert the evaluation result (basically a "NOT" statement).
        /// </summary>
        public bool InvertEvaluation { get; set; }
    }
}
