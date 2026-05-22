using KataCheckout.Entities.Products;
using KataCheckout.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KataCheckout.Entities.Cart
{
    public class CartLineItem : ICanClone<CartLineItem>, IMergeable<CartLineItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartLineItem" /> class.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="numUnits">The number of units.</param>
        public CartLineItem(BaseProduct product, int numUnits)
        {
            this.Product = product;
            this.NumUnits = numUnits;
            this.UnitPrice = product.UnitPrice;
        }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        public BaseProduct Product { get; set; }

        /// <summary>
        /// Gets or sets the number of units.
        /// </summary>
        public int NumUnits { get; set; }

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Clones object.
        /// </summary>
        /// <returns>The clone.</returns>
        public CartLineItem Clone()
        {
            CartLineItem clone = new CartLineItem(this.Product, this.NumUnits);

            return clone;
        }

        /// <summary>
        /// Merges line item into this line item.
        /// </summary>
        /// <param name="item">The line item to merge in.</param>
        public void Merge(CartLineItem item)
        {
            this.NumUnits += item.NumUnits;
        }
    }
}
