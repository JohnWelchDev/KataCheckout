using KataCheckout.Common.Extensions.CollectionExtensions;
using KataCheckout.Entities.Cart;
using KataCheckout.Entities.Offers;
using KataCheckout.Entities.Products;

namespace KataCheckout.Logic.Utilities
{
    /// <summary>
    /// The offer utility.
    /// </summary>
    public static class OfferUtility
    {
        /// <summary>
        /// Extacts offers that could apply to the list of specified products.
        /// </summary>
        /// <param name="offers">The offers.</param>
        /// <param name="products">The products.</param>
        /// <returns>The applicable offers based on products.</returns>
        public static List<SpecialOffer> GetPotentialApplicableOffers(IEnumerable<SpecialOffer> offers, IEnumerable<BaseProduct> products)
        {
            HashSet<string> productSkus = new HashSet<string>();

            // loop through products.
            foreach (BaseProduct product in products)
            {
                // check sku is set.
                if (!string.IsNullOrEmpty(product.SKU))
                {
                    productSkus.Add(product.SKU);
                }
            }

            return GetPotentialApplicableOffers(offers, productSkus);
        }

        /// <summary>
        /// Extacts offers that could apply to the list of specified products.
        /// </summary>
        /// <param name="offers">The offers.</param>
        /// <param name="productSkus">The product skus.</param>
        /// <returns>The applicable offers based on products.</returns>
        public static List<SpecialOffer> GetPotentialApplicableOffers(IEnumerable<SpecialOffer> offers, IEnumerable<string> productSkus)
        {
            List<SpecialOffer> applicableOffers = new List<SpecialOffer>();

            // check the offers and products passed in.
            if (offers != null && productSkus != null)
            {
                // loop through offers.
                foreach (SpecialOffer offer in offers)
                {
                    // get the skus from the offer.
                    IEnumerable<string> offerSkus = offer.ExtractSkus();

                    // loop through products.
                    foreach (string productSku in productSkus)
                    {
                        // track whether product has been found in the offer.
                        bool found = false;

                        // check sku is set.
                        if (!string.IsNullOrEmpty(productSku))
                        {
                            // loop through sku list.
                            foreach (string offerSku in offerSkus)
                            {
                                // check the sku match.
                                if (productSku.Equals(offerSku, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    // sku matched, offer can be applicable.
                                    found = true;

                                    break;
                                }
                            }

                            // check if product was found.
                            if (found)
                            {
                                // add offer to applicable list.
                                applicableOffers.Add(offer);

                                // stop searching products, one is enough to make the
                                // offer potentially a match for the whole checkout.
                                break;
                            }
                        }
                    }
                }
            }

            return applicableOffers;
        }

        /// <summary>
        /// Indicates whether the offer applies to the specified item list.
        /// </summary>
        /// <param name="dicUniqueCartItems">The cart items.</param>
        /// <param name="offer">The offer.</param>
        /// <returns>Value indicating whether or not the offer applies.</returns>
        public static bool OfferApplies(Dictionary<string, CartLineItem> dicUniqueCartItems, SpecialOffer offer)
        {
            bool applies = false;

            // check offer passed in.
            if (dicUniqueCartItems != null && offer != null)
            {
                // check the condition is set.
                if (offer.Condition != null)
                {
                    applies = EvaluateCondition(offer.Condition, dicUniqueCartItems);
                }
                else
                {
                    // if no condition, always applies.
                    applies = true;
                }
            }

            return applies;
        }

        /// <summary>
        /// Evaluates offer condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="cartLines">The cart lines.</param>
        /// <returns>Value indicating whether or not the condition passed.</returns>
        public static bool EvaluateCondition(OfferCondition condition, Dictionary<string, CartLineItem> cartLines)
        {
            bool anyMatch = false;
            bool allMatch = true;

            // check if all units must evaluate true in order for condition to pass.
            bool allRequired = condition.RelationCode == RelationCodes.AND;

            bool unitsChecked = false;

            // check unit conditions set.
            if (condition.UnitConditions != null && condition.UnitConditions.Count > 0)
            {
                // flag units as having been checked.
                unitsChecked = true;

                // loop through the condition units.
                foreach (ProductConditionUnit unit in condition.UnitConditions)
                {
                    // check the sku is set.
                    if (!string.IsNullOrEmpty(unit.SKU))
                    {
                        // check cart line exists for product.
                        if (cartLines.ContainsKey(unit.SKU))
                        {
                            // get the cart line item.
                            CartLineItem cartLineItem = cartLines[unit.SKU];

                            // evaluate the condition unit.
                            bool unitResult = OperatorUtility.Evaluate(cartLineItem.NumUnits, unit.Operator, unit.NumUnits);

                            // update matchn tracking variables.
                            anyMatch |= unitResult;
                            allMatch &= unitResult;

                            // check if all are required and a unit failed.
                            if (allRequired && !allMatch)
                            {
                                // stop searching, already failed.
                                break;
                            }
                        }
                        else
                        {
                            // no cart line found for product.
                            allMatch = false;

                            // check if all required to pass.
                            if (allRequired)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        // unable to match condition as can't identify product.
                        allMatch = false;

                        // check if all required to pass.
                        if (allRequired)
                        {
                            // all required to pass so evaluate condition fails.
                            break;
                        }
                    }
                }
            }

            // matched so far if all are matched or one or more matches found and not all are required to pass.
            bool pass = allMatch || (!allRequired && anyMatch);

            // check if can continue to evaluate child conditions, scenarios being:
            // no units checked - only child conditions set
            // all passed - still potential match
            // not all required to pass - still could be potential pass in child group.
            if (pass || !allRequired || !unitsChecked)
            {
                // check the child offer conditions are populated.
                if (condition.ChildOfferConditions != null && condition.ChildOfferConditions.Count > 0)
                {
                    // loop through the child offers.
                    foreach (OfferCondition childCondition in condition.ChildOfferConditions)
                    {
                        // evaluate child condition.
                        bool childPass = EvaluateCondition(childCondition, cartLines);

                        anyMatch |= childPass;
                        allMatch &= childPass;

                        // check if child condition passed.
                        if (!childPass && allRequired)
                        {
                            // we know the overall evaluation will now fail, stop searching.
                            break;
                        }
                    }

                    // check how to incorporate child result.
                    if (allRequired)
                    {
                        pass &= allMatch;
                    }
                    else
                    {
                        pass |= anyMatch;
                    }
                }
            }

            // check if condition is set to invert it's overall evaluation.
            if (condition.InvertEvaluation)
            {
                // invert result.
                pass = !pass;
            }

            return pass;
        }

        /// <summary>
        /// Executes offer.
        /// </summary>
        /// <param name="cartLines">The cart lines.</param>
        /// <param name="offer">The offer.</param>
        /// <returns>The result.</returns>
        public static ExecuteOfferResult ExecuteOffer(Dictionary<string, CartLineItem> cartLines, SpecialOffer offer)
        {
            ExecuteOfferResult result = new ExecuteOfferResult();

            // check cart lines present.
            if (cartLines != null && cartLines.Count > 0)
            {
                // check if offer is configured to apply flat discount.
                if (offer.OfferMode == OfferExecutionMode.FlatTotalDiscount || offer.OfferMode == OfferExecutionMode.FlatPercentageDiscount)
                {
                    // set the amount.
                    // shouldn't be any product related rules as configured to be a flat
                    // across the board discount.
                    decimal discountAmount = CalculateDiscount(cartLines, offer, out string? discountLog);

                    // check discount calculated.
                    if (discountAmount > 0)
                    {
                        result.DiscountAmountApplied = discountAmount;
                        result.NumTimesApplied = 1;
                        result.DiscountLog = discountLog;
                    }
                    else
                    {
                        // failed to calculate discount.
                        result.ErrorMessage = discountLog ?? "Unable to calculate discount";
                    }
                }
                else
                {
                    // check offer passed in.
                    if (offer != null && offer.ExecutionRules != null && offer.ExecutionRules.Count() > 0)
                    {
                        // make copy of cart line items.
                        Dictionary<string, CartLineItem> copyItems = new Dictionary<string, CartLineItem>();
                        copyItems.CopyFrom(cartLines, x => x.Clone());

                        List<CartLineItem> offerAffectedItems = new List<CartLineItem>();

                        // loop through execution rules.
                        foreach (OfferExecutionRule rule in offer.ExecutionRules)
                        {
                            // check product can be found in cart lines.
                            if (copyItems.ContainsKey(rule.SKU))
                            {
                                CartLineItem targetItem = copyItems[rule.SKU];

                                // work out the number of units to be affected by offer.
                                // if no limit set or limit is above number of line item units, use line item units,
                                // otherwise use the rule limit as number of line item units exceeds it.
                                int affectedUnitCount = !rule.NumUnitLimit.HasValue || rule.NumUnitLimit.Value > targetItem.NumUnits ? targetItem.NumUnits : rule.NumUnitLimit.Value;

                                // clone item to create affected item entry.
                                CartLineItem affectedItem = targetItem.Clone();
                                affectedItem.NumUnits = affectedUnitCount;
                                offerAffectedItems.Add(affectedItem);

                                // set the number of units to remain unaffected by offer.
                                targetItem.NumUnits -= affectedUnitCount;

                                // check the number of units remaining.
                                if (targetItem.NumUnits <= 0)
                                {
                                    // remove item from collection.
                                    copyItems.Remove(rule.SKU);
                                }
                            }
                        }

                        // add affected offers to result.
                        result.FillAffectedItems(offerAffectedItems);

                        // check if there are any remaining items.
                        if (copyItems.Count > 0)
                        {
                            // add unaffected items to result.
                            result.FillUnaffectedItems(copyItems.Values);
                        }

                        // check affected line items.
                        if (result.AffectedLineItems.Count > 0)
                        {
                            // calculate discount to apply.
                            decimal discountAmount = CalculateDiscount(result.AffectedLineItems, offer, out string? log);

                            // check discount was calculated
                            if (discountAmount > 0)
                            {
                                // set discount amount.
                                result.DiscountAmountApplied = discountAmount;
                                result.NumTimesApplied++;
                                result.DiscountLog = log;
                            }
                            else
                            {
                                // set log as error message.
                                result.ErrorMessage = log ?? "Unable to calculate discount";
                            }
                        }
                    }
                    else
                    {
                        // no execution rules.
                        result.ErrorMessage = "No offer detected or no execution rules detected";

                        // all lines are unaffected.
                        result.UnaffectedLineItems.CopyFrom(cartLines);
                    }
                }
            }
            else
            {
                // no cart lines.
                result.ErrorMessage = "No cart lines detected";
            }

            return result;
        }

        /// <summary>
        /// Calculates discount.
        /// </summary>
        /// <param name="affectedProducts">The group of affected products.</param>
        /// <param name="offer">The offer.</param>
        /// <param name="log">The log.</param>
        /// <returns>The discount amount.</returns>
        public static decimal CalculateDiscount(Dictionary<string, CartLineItem> affectedProducts, SpecialOffer offer, out string? log)
        {
            log = null;
            decimal discountAmount;

            switch (offer.OfferMode)
            {
                case OfferExecutionMode.FlatTotalDiscount:
                    {
                        // get the totals.
                        decimal total = GetTotalForProductLineItems(affectedProducts);

                        // check that the offer discount amount does not exceed the calculated total.
                        if (total >= offer.DiscountAmount)
                        {
                            // use flat amount specified on offer.
                            discountAmount = offer.DiscountAmount;

                            // set log.
                            log = $"applied discount to cart total of {total} applying discount of {discountAmount}";
                        }
                        else
                        {
                            // discount would push into negatives - abandon.
                            discountAmount = 0;

                            // set log.
                            log = $" discount not applied as amount {offer.DiscountAmount} would exceed the calculated cart total of {total}";
                        }

                        break;
                    }

                case OfferExecutionMode.CostOverrideForProductsTotal:
                    {
                        // get the totals.
                        decimal total = GetTotalForProductLineItems(affectedProducts);

                        // check that the offer discount amount does not exceed the calculated total.
                        if (total >= offer.DiscountAmount)
                        {
                            // use flat amount specified on offer.
                            discountAmount = total - offer.DiscountAmount;

                            // set log.
                            log = $"applied discount overriding product total of {total} to {offer.DiscountAmount} (discount of {discountAmount}";
                        }
                        else
                        {
                            // discount would push into negatives - abandon.
                            discountAmount = 0;

                            // set log.
                            log = $"price override discount not applies as amount {offer.DiscountAmount} would exceed the calculated product total of {total}";
                        }

                        break;
                    }

                case OfferExecutionMode.FlatPercentageDiscount:
                    {
                        string subject = offer.OfferMode == OfferExecutionMode.FlatTotalDiscount ? "cart" : "product";

                        // get the totals.
                        decimal total = GetTotalForProductLineItems(affectedProducts);

                        // convert percentage to multiplier.
                        decimal discountMultiplier = offer.DiscountAmount / 100;

                        if (discountMultiplier <= 1)
                        {
                            // apply percentage ratio to calculate discount amount.
                            discountAmount = Math.Round(total * discountMultiplier, 2);

                            // set log.
                            log = $"applied {offer.DiscountAmount}% discount based on cart total ({total}) applying discount amount of {discountAmount}";
                        }
                        else
                        {
                            // discount multiplier calculated would increase price.
                            discountAmount = 0;

                            // set log.
                            log = $"discount percentage {offer.DiscountAmount}% calculated multiplier ({discountMultiplier}) that would increase price";
                        }

                        break;
                    }

                case OfferExecutionMode.CheapestProductFlatDiscount:
                    {
                        // get the cheapest
                        decimal? cheapestUnitPrice = GetCheapestItemUnitPrice(affectedProducts);

                        // check cheapest unit price found and discount would not put it into negative.
                        if (cheapestUnitPrice.HasValue)
                        {
                            if (cheapestUnitPrice.Value >= offer.DiscountAmount)
                            {
                                // set the discount amount to flat amount.
                                discountAmount = offer.DiscountAmount;

                                // set log.
                                log = $"applies flat discount {offer.DiscountAmount} to cheapest product (unit price {cheapestUnitPrice.Value}";
                            }
                            else
                            {
                                // discount amount exceeds unit price.
                                discountAmount = 0;

                                // set log.
                                log = $"discount amount ({offer.DiscountAmount}) exceeds the unit price {cheapestUnitPrice ?? 0}";
                            }
                        }
                        else
                        {
                            // discount amount exceeds unit price.
                            discountAmount = 0;

                            // set log.
                            log = $"failed to calculate cheapest unit price";
                        }

                        break;
                    }

                case OfferExecutionMode.CheapestProductPercentageDiscount:
                    {
                        // get the cheapest
                        decimal? cheapestUnitPrice = GetCheapestItemUnitPrice(affectedProducts);

                        // check cheapest unit price found and discount would not put it into negative.
                        if (cheapestUnitPrice.HasValue)
                        {
                            // convert percentage to multiplier.
                            decimal discountMultiplier = offer.DiscountAmount / 100;

                            if (discountMultiplier <= 1)
                            {
                                // apply percentage ratio to calculate discount amount.
                                discountAmount = Math.Round(cheapestUnitPrice.Value * discountMultiplier, 2);

                                // set log.
                                log = $"applied {offer.DiscountAmount}% discount based on cheapest product ({cheapestUnitPrice.Value}) applying discount amount of {discountAmount}";
                            }
                            else
                            {
                                // discount multiplier calculated would increase price.
                                discountAmount = 0;

                                // set log.
                                log = $"discount percentage {offer.DiscountAmount}% calculated multiplier ({discountMultiplier}) that would increase price";
                            }
                        }
                        else
                        {
                            // no cheapest unit price found.
                            discountAmount = 0;

                            log = "Unable to find cheapest unit price";
                        }

                        break;
                    }

                case OfferExecutionMode.ExpensiveProductFlatDiscount:
                    {
                        // get the cheapest
                        decimal? expensiveUnitPrice = GetMostExpensiveItemUnitPrice(affectedProducts);

                        // check cheapest unit price found and discount would not put it into negative.
                        if (expensiveUnitPrice.HasValue)
                        {
                            if (expensiveUnitPrice.Value >= offer.DiscountAmount)
                            {
                                // set the discount amount to flat amount.
                                discountAmount = offer.DiscountAmount;

                                // set log.
                                log = $"applies flat discount {offer.DiscountAmount} to most expensive product (unit price {expensiveUnitPrice.Value}";
                            }
                            else
                            {
                                // discount amount exceeds unit price.
                                discountAmount = 0;

                                // set log.
                                log = $"discount amount ({offer.DiscountAmount}) exceeds the unit price {expensiveUnitPrice ?? 0}";
                            }
                        }
                        else
                        {
                            // discount amount exceeds unit price.
                            discountAmount = 0;

                            // set log.
                            log = $"failed to calculate most expensive unit price";
                        }

                        break;
                    }

                case OfferExecutionMode.ExpensiveProductPercentageDiscount:
                    {
                        // get the cheapest
                        decimal? expensiveUnitPrice = GetMostExpensiveItemUnitPrice(affectedProducts);

                        // check cheapest unit price found and discount would not put it into negative.
                        if (expensiveUnitPrice.HasValue)
                        {
                            // convert percentage to multiplier.
                            decimal discountMultiplier = offer.DiscountAmount / 100;

                            if (discountMultiplier <= 1)
                            {
                                // apply percentage ratio to calculate discount amount.
                                discountAmount = Math.Round(expensiveUnitPrice.Value * discountMultiplier, 2);

                                // set log.
                                log = $"applied {offer.DiscountAmount}% discount based on cheapest product ({expensiveUnitPrice.Value}) applying discount amount of {discountAmount}";
                            }
                            else
                            {
                                // discount multiplier calculated would increase price.
                                discountAmount = 0;

                                // set log.
                                log = $"discount percentage {offer.DiscountAmount}% calculated multiplier ({discountMultiplier}) that would increase price";
                            }
                        }
                        else
                        {
                            // no cheapest unit price found.
                            discountAmount = 0;

                            log = "Unable to find cheapest unit price";
                        }

                        break;
                    }
                default:
                    {
                        // offer mode not recognized.
                        discountAmount = 0;

                        log = "Unable to determine offer mode";

                        break;
                    }
            }

            return discountAmount;
        }
        
        /// <summary>
        /// Gets total for product line items.
        /// </summary>
        /// <param name="cartLineItems">The cart line items.</param>
        /// <returns>The total.</returns>
        public static decimal GetTotalForProductLineItems(Dictionary<string, CartLineItem> cartLineItems)
        {
            decimal total = 0;

            // check the cart line items.
            if (cartLineItems != null && cartLineItems.Count > 0)
            {
                // loop through skus.
                foreach (string sku in cartLineItems.Keys)
                {
                    // get the line item.
                    CartLineItem lineItem = cartLineItems[sku];

                    // multiply out line total.
                    decimal lineTotal = lineItem.NumUnits * lineItem.UnitPrice;

                    // add to grand total.
                    total += lineTotal;
                }
            }

            return total;
        }

        /// <summary>
        /// Gets total for product line items.
        /// </summary>
        /// <param name="cartLineItems">The cart line items.</param>
        /// <returns>The total.</returns>
        public static decimal? GetCheapestItemUnitPrice(Dictionary<string, CartLineItem> cartLineItems)
        {
            decimal? cheapest = null;

            // check the cart line items.
            if (cartLineItems != null && cartLineItems.Count > 0)
            {
                // loop through skus.
                foreach (string sku in cartLineItems.Keys)
                {
                    // get the line item.
                    CartLineItem lineItem = cartLineItems[sku];

                    // check if current cheapest is set and if this line is cheaper.
                    if (!cheapest.HasValue || cheapest.Value > lineItem.UnitPrice)
                    {
                        cheapest = lineItem.UnitPrice;
                    }
                }
            }

            return cheapest;
        }

        /// <summary>
        /// Gets total for product line items.
        /// </summary>
        /// <param name="cartLineItems">The cart line items.</param>
        /// <returns>The total.</returns>
        public static decimal? GetMostExpensiveItemUnitPrice(Dictionary<string, CartLineItem> cartLineItems)
        {
            decimal? expensive = null;

            // check the cart line items.
            if (cartLineItems != null && cartLineItems.Count > 0)
            {
                // loop through skus.
                foreach (string sku in cartLineItems.Keys)
                {
                    // get the line item.
                    CartLineItem lineItem = cartLineItems[sku];

                    // check if current expensive is set and if this line is cheaper.
                    if (!expensive.HasValue || expensive.Value < lineItem.UnitPrice)
                    {
                        expensive = lineItem.UnitPrice;
                    }
                }
            }

            return expensive;
        }
    }
}
