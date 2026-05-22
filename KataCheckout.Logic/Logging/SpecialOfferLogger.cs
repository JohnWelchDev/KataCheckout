using KataCheckout.Entities.Offers;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Logging
{
    /// <summary>
    /// The special offer logger.
    /// </summary>
    public class SpecialOfferLogger : ISpecialOfferLogger
    {
        /// <summary>
        /// The logs.
        /// </summary>
        private Dictionary<int, List<string>> dicLog;

        public SpecialOfferLogger()
        {
            this.dicLog = new Dictionary<int, List<string>>();
        }

        /// <summary>
        /// Logs message against offer.
        /// </summary>
        /// <param name="offer">The offer.</param>
        /// <param name="message">The message.</param>
        public void Log(SpecialOffer offer, string message)
        {
            // check the parameters passed in.
            if (offer != null && !string.IsNullOrWhiteSpace(message))
            {
                List<string> logs;
                if (this.dicLog.ContainsKey(offer.SpecialOfferID))
                {
                    // get existing entry.
                    logs = this.dicLog[offer.SpecialOfferID];
                }
                else
                {
                    // create new entry.
                    logs = new List<string>();

                    // add to dictionary.
                    this.dicLog.Add(offer.SpecialOfferID, logs);
                }

                // add message to log.
                logs.Add(message);
            }
        }
    }
}
