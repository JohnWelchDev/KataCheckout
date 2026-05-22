using KataCheckout.Entities.Offers;

namespace KataCheckout.Logic.Logging
{
    /// <summary>
    /// The special offer logger interface.
    /// </summary>
    public interface ISpecialOfferLogger
    {
        /// <summary>
        /// Logs message against offer.
        /// </summary>
        /// <param name="offer">The offer.</param>
        /// <param name="message">The message.</param>
        void Log(SpecialOffer offer, string message);
    }
}