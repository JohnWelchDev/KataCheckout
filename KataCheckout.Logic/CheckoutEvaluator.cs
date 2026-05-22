using KataCheckout.Common.Extensions.CollectionExtensions;
using KataCheckout.Entities;
using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Entities.Products;
using KataCheckout.Logic.Logging;
using KataCheckout.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace KataCheckout.Logic
{
    /// <summary>
    /// The checkout evaluator.
    /// </summary>
    public class CheckoutEvaluator
    {
        /// <summary>
        /// The special offers.
        /// </summary>
        private List<SpecialOffer> specialOffers;

        /// <summary>
        /// The special offer logger.
        /// </summary>
        private ISpecialOfferLogger offerLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckoutEvaluator" /> class.
        /// </summary>
        /// <param name="specialOffers">The special offers.</param>
        /// <param name="offerLogger">The offer logger to use.</param>
        public CheckoutEvaluator(IEnumerable<SpecialOffer> specialOffers, ISpecialOfferLogger offerLogger)
        {
            this.specialOffers = new List<SpecialOffer>();

            // check object passed in.
            if (specialOffers != null)
            {
                // fill thje special offers collection for evaluator.
                this.specialOffers.AddRange(specialOffers);
            }

            this.offerLogger = offerLogger;
        }

        public CartCalculationResponse CalculateTotal(IEnumerable<CartLineItem> lineItems)
        {
            CartCalculationResponse response = new CartCalculationResponse();

            // check line items passed in.
            // note: could have added check for collection length that line items in
            // collection had units set, but chose to evaluate reference being set and
            // allow empty list to be a valid input that evaluates to 0.
            if (lineItems != null)
            {
                // create dictionary to track unique skus and their quantities.
                // setup is intended to be one sku per line, however can't control how it is passed in.
                Dictionary<string, CartLineItem> dicLineItems = CartUtility.GetUniqueLineItems(lineItems, out string? uniqueLineItemErrorMessage);

                // check the unique items returned successfully.
                if (string.IsNullOrWhiteSpace(uniqueLineItemErrorMessage))
                {
                    // calculate the total excluding offers.
                    response.TotalExcludingOffers = OfferUtility.GetTotalForProductLineItems(dicLineItems);

                    // filter offers to those that could potentially apply.
                    IEnumerable<SpecialOffer> applicableOffers = OfferUtility.GetPotentialApplicableOffers(this.specialOffers, dicLineItems.Keys);

                    // check potential applicable offers found.
                    if (applicableOffers != null && applicableOffers.Count() > 0)
                    {
                        // create copy of cart line items.
                        Dictionary<string, CartLineItem> copyItems = new Dictionary<string, CartLineItem>();
                        copyItems.CopyFrom(dicLineItems, x => x.Clone());

                        // loop through potential offers checking if conditions are met.
                        foreach (SpecialOffer offer in applicableOffers)
                        {
                            int numTimesApplied = 0;
                            bool canContinue = true;
                            bool stopCheckingOffer = false;

                            do
                            {
                                // check offer applies.
                                bool applies = OfferUtility.OfferApplies(dicLineItems, offer);
                                
                                // check the special offer applies.
                                if (applies)
                                {
                                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                                    string? error = null;

                                    /*
                                    Dictionary<string, (CartLineItem, OfferExecutionRule)> dicLineItemRuleMappings = new Dictionary<string, (CartLineItem, OfferExecutionRule)>();
                                    Dictionary<string, CartLineItem> dicTargetExecutionItems = new Dictionary<string, CartLineItem>();

                                    // loop through the execution rules.
                                    foreach (OfferExecutionRule rule in offer.ExecutionRules)
                                    {
                                        // check the sku can be found in cart line items.
                                        if (copyItems.ContainsKey(rule.SKU))
                                        {
                                            // check if mapping exists for sku.
                                            if (!dicLineItemRuleMappings.ContainsKey(rule.SKU))
                                            {
                                                // add mapping for sku connecting line item to execution rule.
                                                CartLineItem lineItem = copyItems[rule.SKU];
                                                dicLineItemRuleMappings.Add(rule.SKU, (lineItem, rule));

                                                // add to dicitonary of items to be targeted.
                                                dicTargetExecutionItems.Add(rule.SKU, lineItem);
                                            }
                                            else
                                            {
                                                // duplicate sku in rule list.
                                                error = $"SKU \"{rule.SKU}\" appears in offer execution rule list more than once";

                                                break;
                                            }
                                        }
                                        else
                                        {
                                            // sku not found in line items.
                                            error = $"execution rule could not be executed because sku \"{rule.SKU}\" was not found in cart line items";

                                            break;
                                        }
                                    }
                                    */

                                    // check can continue with offer execution.
                                    if (string.IsNullOrWhiteSpace(error))
                                    {
                                        Dictionary<string, CartLineItem> dicAffectedItems = new Dictionary<string, CartLineItem>();
                                        Dictionary<string, CartLineItem> dicRemainderItems = new Dictionary<string, CartLineItem>();

                                        // execute the offer.
                                        ExecuteOfferResult offerResult = OfferUtility.ExecuteOffer(copyItems, offer);

                                        // check offer result returned.
                                        if (offerResult != null)
                                        {
                                            // check discount applied.
                                            if (offerResult.DiscountAmountApplied > 0)
                                            {
                                                // increment number of times offer applied.
                                                numTimesApplied++;

                                                // create discount line.
                                                CartCalculationDiscountLine discountLine = new CartCalculationDiscountLine();
                                                discountLine.SpecialOfferID = offer.SpecialOfferID;
                                                discountLine.DiscountAmount = offerResult.DiscountAmountApplied;

                                                // check affected line items are listed.
                                                if (offerResult.AffectedLineItems != null)
                                                {
                                                    discountLine.LineItemsAffected.AddRange(offerResult.AffectedLineItems.Values);
                                                }

                                                // add to response.
                                                response.DiscountLines.Add(discountLine);

                                                // override copy items with list of unaffected items.
                                                copyItems = offerResult.UnaffectedLineItems;

                                                // check copy items is not null.
                                                if (copyItems == null)
                                                {
                                                    copyItems = new Dictionary<string, CartLineItem>();
                                                }
                                            }
                                            else
                                            {
                                                // no discount applied, stop checking.
                                                stopCheckingOffer = true;

                                                // check if error message returned.
                                                if (!string.IsNullOrEmpty(offerResult.ErrorMessage))
                                                {
                                                    // log error message.
                                                    offerLogger.Log(offer, offerResult.ErrorMessage);
                                                }
                                                else if (!string.IsNullOrWhiteSpace(offerResult.DiscountLog))
                                                {
                                                    // log result log message.
                                                    offerLogger.Log(offer, offerResult.DiscountLog);
                                                }
                                                else
                                                {
                                                    // no message to log, set generic message.
                                                    offerLogger.Log(offer, "Offer did not apply discount");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // no offer result returned.
                                            offerLogger.Log(offer, "No result returned when attempting to execute offer");
                                            stopCheckingOffer = true;
                                        }

                                        /*
                                        // loop through rule mappings.
                                        foreach (string sku in dicLineItemRuleMappings.Keys)
                                        {
                                            // get mapping.
                                            (CartLineItem, OfferExecutionRule) mapping = dicLineItemRuleMappings[sku];

                                            // check if sku has a number of units limit.
                                            if (mapping.Item2.NumUnitLimit.HasValue)
                                            {
                                                // use rule limit if num units has hit or exceeded the cap, otherwise use number of units from line item.
                                                int numAffected = mapping.Item1.NumUnits >= mapping.Item2.NumUnitLimit.Value ? mapping.Item2.NumUnitLimit.Value : mapping.Item1.NumUnits;

                                                // copy affected item and set the number of items set.
                                                CartLineItem affectedLineItem = mapping.Item1.Clone();
                                                affectedLineItem.NumUnits = numAffected;
                                                dicAffectedItems.Add(sku, affectedLineItem);

                                                mapping.Item1.NumUnits -= numAffected;
                                            }
                                            else
                                            {
                                                // no limit specified, all items affected.
                                                CartLineItem affected = mapping.Item1.Clone();
                                                affected.NumUnits = mapping.Item1.NumUnits;

                                                // in a loop of unique keys so check shouldn't be required here.
                                                dicAffectedItems.Add(sku, affected);
                                            }
                                        }
                                        */
                                    }
                                    else
                                    {
                                        // log error message.
                                        offerLogger.Log(offer, error);

                                        // flag to stop checking offer.
                                        stopCheckingOffer = true;
                                    }

                                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                }
                                else
                                {
                                    // offer did not apply
                                    stopCheckingOffer = false;
                                }

                                // check number of times offer applied is still within offer per checkout limit.
                                bool withinLimit = offer.LimitPerCheckout.HasValue && offer.LimitPerCheckout.Value > 0 && numTimesApplied < offer.LimitPerCheckout.Value;
                                
                                // check if can continue checking offer against item list.
                                canContinue = withinLimit && !stopCheckingOffer;
                            }
                            while (canContinue);
                        }

                        // start with total before discount applied.
                        decimal totalIncludingDiscount = response.TotalExcludingOffers;

                        // check discount lines were generated when applying offers.
                        if (response.DiscountLines.Count > 0)
                        {
                            // loop through discount lines.
                            foreach (CartCalculationDiscountLine discountLine in response.DiscountLines)
                            {
                                // apply discount total.
                                totalIncludingDiscount -= discountLine.DiscountAmount;
                            }
                        }

                        response.TotalIncludingOffers = totalIncludingDiscount;

                        response.Success = true;
                    }
                    else
                    {
                        // no offers to apply, flag successful.
                        response.Success = true;
                        response.TotalIncludingOffers = response.TotalExcludingOffers;
                    }
                }
                else
                {
                    // set response error message.
                    response.ErrorMessage = uniqueLineItemErrorMessage;
                }
            }
            else
            {
                // no line items passed in.
                response.ErrorMessage = "No line items detected";
            }

            return response;
        }


        public void ApplySpecialOffers(Dictionary<string, CartLineItem> lineItems, IEnumerable<SpecialOffer> specialOffers)
        {
            Dictionary<string, CartLineItem> copyItems = new Dictionary<string, CartLineItem>();

            // loop through skus.
            foreach (string sku in lineItems.Keys)
            {
                // copy into dicitonary.
                copyItems.Add(sku, lineItems[sku].Clone());
            }

            Dictionary<int, int> dicOfferAppliedCount = new Dictionary<int, int>();

            // loop through offers to apply.
            foreach (SpecialOffer offer in specialOffers)
            {
                // check the limit per checkout.
                if (offer.LimitPerCheckout.HasValue && offer.LimitPerCheckout.Value > 0)
                {
                    // check if there is an offer count.
                    if (dicOfferAppliedCount.ContainsKey(offer.SpecialOfferID))
                    {
                        // check if limit has been reached.
                        if (dicOfferAppliedCount[offer.SpecialOfferID] >= offer.LimitPerCheckout.Value)
                        {
                            continue;
                        }
                    }
                }

                // apply the offer.
                ExecuteOfferResult result = OfferUtility.ExecuteOffer(copyItems, offer);

                // check if offer applied count exists.
                if (dicOfferAppliedCount.ContainsKey(offer.SpecialOfferID))
                {
                    // increment the count.
                    dicOfferAppliedCount[offer.SpecialOfferID]++;
                }
                else
                {
                    // add entry.
                    dicOfferAppliedCount.Add(offer.SpecialOfferID, 1);
                }
            }
        }
    }
}
