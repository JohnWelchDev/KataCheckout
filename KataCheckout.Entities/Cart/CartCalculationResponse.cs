using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Cart
{
    /// <summary>
    /// The cart calculation response.
    /// </summary>
    public class CartCalculationResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not the operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the total with offers applied.
        /// </summary>
        public decimal TotalIncludingOffers { get; set; }

        /// <summary>
        /// Ges or sets the total excluding offers.
        /// </summary>
        public decimal TotalExcludingOffers { get; set; }
    }
}
