using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDLL
{
    public class ProductHelper
    {
        // add new product 
        public static bool AddNewProduct(Product product, ECommerceEntities db)
        {
            // if product's already in the database return false
            List<int> allProductIds = db.Products.Select(p => p.ProductID).ToList();
            if (allProductIds.Contains(product.ProductID))
            {
                return false;
            }

            // otherwise add product and return true
            db.Products.Add(product);
            db.SaveChanges();
            return true;
        }

        // remove a product
        public static bool RemoveProduct(Product product, ECommerceEntities db)
        {
           // check if product's in the database
                // if it is, remove it and return true
                List<int> allProductIds = db.Products.Select(p => p.ProductID).ToList();
                if (allProductIds.Contains(product.ProductID))
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                    return true;
                }

                // otherwise return false
                return false;
            
        }

        // edit a product
        public static bool EditProduct(Product product, string name = "", double price = -1, string description = "", int categoryId = -1, string image = "")
        {
            using (var db = new ECommerceEntities())
            {
                // check if product's in the database
                // if it isn't return false right away
                List<int> allProductIds = db.Products.Select(p => p.ProductID).ToList();
                if (!allProductIds.Contains(product.ProductID))
                {
                    return false;
                }

                // otherwise
                // check what parameters to update
                if (name != "")
                {product.ProductName = name;}
                if (price != -1)
                {
                    product.ProductPrice = price;
                    product.ProductOnSale = false;
                }
                if (description != "")
                {product.ProductDescription = description;}
                if (categoryId != -1)
                {product.CategoryID = categoryId;}
                if (image != "")
                {product.ProductImage = image;}

                // update product and return true
                db.Products.AddOrUpdate(product);
                db.SaveChanges();
                return true;
            }
        }

        // find products in specific category. Take as many as is needed for a particular page
        public static List<Product> FindProductsInCategory(Category category, int startFrom, int quantity, out int countAllProducts)
        {
            using (var db = new ECommerceEntities())
            {
                countAllProducts = db.Products.Count(p => p.CategoryID == category.CategoryID);
                return db.Products.Where(p => p.CategoryID == category.CategoryID).OrderBy(p => p.ProductName).Skip(startFrom).Take(quantity).ToList();
                
            }
        }

        // find productsby name. Take as many as is needed for a particular page
        public static List<Product> FindProductsByName(string keyword, int startFrom, int quantity, out int countAllProducts)
        {
            using (var db = new ECommerceEntities())
            {
                countAllProducts = db.Products.Count(p => p.ProductName.ToUpper().Contains(keyword.ToUpper()));
                return db.Products.Where(p => p.ProductName.ToUpper().Contains(keyword.ToUpper())).OrderBy(p => p.ProductName).Skip(startFrom).Take(quantity).ToList();
            }
        }
       
        public static bool SetProductOnSale(Product product, double salePrice)
        {
            using (var db = new ECommerceEntities())
            {
                // check if product's in the database
                // if it isn't return false right away
                List<int> allProductIds = db.Products.Select(p => p.ProductID).ToList();
                if (!allProductIds.Contains(product.ProductID))
                {
                    return false;
                }
                // otherwise update product's sale price and productOnSale bit and return true
                product.ProductSalePrice = salePrice;
                product.ProductOnSale = true;
                db.Products.AddOrUpdate(product);
                db.SaveChanges();
                return true;
            }
        }

        public static bool CloseSaleOnProduct(Product product)
        {
            using (var db = new ECommerceEntities())
            {
                // check if product's in the database
                // if it isn't return false right away
                List<int> allProductIds = db.Products.Select(p => p.ProductID).ToList();
                if (!allProductIds.Contains(product.ProductID))
                {
                    return false;
                }
                // otherwise null product's sale price and update productOnSale bit, return true
                product.ProductSalePrice = null;
                product.ProductOnSale = false;
                db.Products.AddOrUpdate(product);
                db.SaveChanges();
                return true;
            }
        }

        public static List<Product> GetProductsOnSale(int quantity, int skip)
        {
            using (var db = new ECommerceEntities())
            {
                return db.Products.OrderBy(p => p.ProductName).Where(p => p.ProductOnSale == true).Skip(skip).Take(quantity).ToList();
            }
        }

        // return a product with specific id
        public static Product GetProductById(int id)
        {
            using (var db = new ECommerceEntities())
            {
                return db.Products.FirstOrDefault(p => p.ProductID == id);
            } 
        }

        // return several newly added products
        public static List<Product> GetNewProducts(int i)
        {
            using (var db = new ECommerceEntities())
            {
                return db.Products.Where(p => p.ProductOnSale == false).OrderByDescending(p => p.ProductID).Take(i).ToList();
            } 
        }
    }
}
