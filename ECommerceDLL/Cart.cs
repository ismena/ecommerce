using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDLL
{
    public class Cart
    {
        // inner collection to store products and their quantities
        private Dictionary<Product, int> products;
        
        // default constuctor to create an empty cart
        public Cart()
        {
            products = new Dictionary<Product, int>();
        }

        // constructor to create a cart with products in it
        public Cart(Product product, int quantity)
        {
            products = new Dictionary<Product, int>();
            products.Add(product, quantity);
        }

        // indexer to access quantities by product
        public int this[Product p]
        {
            get { return products[p]; }
            set { products[p] = value; }
        }

        // property to get the inner collection
        public Dictionary<Product, int> Products
        {
            get { return products; }
        }

        // add a product to cart
        public void Add(Product product, int quantity = 1)
        {
            // check if product is already in cart
            // if yes, update the quantity
            if (products.Keys.Select(p => p.ProductID).ToList().Contains(product.ProductID))
            {
                Product productToUpdate = products.Keys.First(p => p.ProductID == product.ProductID);
                products[productToUpdate] += quantity;
            }
            // else add new product
            else
            {
                products.Add(product, quantity);
            }
            
        }

        // remove product from cart
        public void Remove(Product product, int quantity = -1)
        {
            // check if product is in cart
            // if no do nothing and return
            if (!products.Keys.Select(p => p.ProductID).ToList().Contains(product.ProductID))
            {
                return;
            }

            Product productToUpdate = products.Keys.First(p => p.ProductID == product.ProductID);
            if (quantity == -1)
            {
                products.Remove(productToUpdate);
                return;
            }

            // check if we're removing product altother or just decreasing it's quantity in cart
            // decrease quantity case
            if (products[productToUpdate] - quantity > 0)
            {
                products[productToUpdate] -= quantity;
                return;
            }
            // removal case
            else
            {
                products.Remove(productToUpdate);
            }
            
        }

        public double GetSum()
        {
            double sum = 0;
            foreach (var product in products)
            {
                if (product.Key.ProductOnSale && product.Key.ProductSalePrice != null)
                {
                    sum += (double)product.Key.ProductSalePrice * product.Value;
                }
                else
                {
                    sum += product.Key.ProductPrice * product.Value;
                }
                
            }

            return sum;
        }

        public int Count()
        {
            int count = 0;
            foreach (var product in products)
            {
                count += product.Value;
            }

            return count;
        }
    }
}
