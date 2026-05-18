using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Logic.Utilities
{
    /// <summary>
    /// The operator utility.
    /// </summary>
    public static class OperatorUtility
    {
        /// <summary>
        /// Evaluates values based on operator.
        /// </summary>
        /// <param name="subject">The subject value.</param>
        /// <param name="operatorCode">The operator code.</param>
        /// <param name="comparison">The comparison value.</param>
        /// <returns>Value indicating whether or not the evaluation matched.</returns>
        public static bool Evaluate(int subject, string operatorCode, int comparison)
        {
            switch (operatorCode)
            {
                case "=":
                    return subject == comparison;

                case "!=":
                    return subject != comparison;

                case ">":
                    return subject > comparison;
                
                case ">=":
                    return subject >= comparison;

                case "<":
                    return subject < comparison;

                case "<=":
                    return subject <= comparison;

                default:
                    throw new InvalidOperationException("Invalid operator code");
            }
        }
    }
}
