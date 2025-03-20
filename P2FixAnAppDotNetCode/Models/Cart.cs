using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    using System;

    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {

        private List<CartLine> _cartLines = new List<CartLine>();
        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => _cartLines;

        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private List<CartLine> GetCartLineList()
        {
            return _cartLines; 
        }


        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {

            var cartLines = GetCartLineList();
            var line = cartLines.FirstOrDefault(l => l.Product.Id == product.Id);

            if (line == null)
            {
                line = new CartLine(product, quantity);
                cartLines.Add(line);
            }
            else
            {
                line.Quantity += quantity;
            }
            Console.WriteLine($"Produit ajouté : {line.Product.Name}, Quantité : {line.Quantity}");
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            return _cartLines.Sum(line => line.Product.Price * line.Quantity);
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            
            var totalQuantity = _cartLines.Sum(line => line.Quantity);
            var totalValue = _cartLines.Sum(line => line.Product.Price * line.Quantity);

            return totalQuantity > 0 ? totalValue / totalQuantity : 0;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            return Lines
                .FirstOrDefault(line => line.Product.Id == productId)?
                .Product;
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public CartLine(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
