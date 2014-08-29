using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDLL
{
    public class OrderHelper
    {
        // add new order 
        public static bool AddNewOrder(Order order, ECommerceEntities db, Dictionary<Product, int> products)
        {
            // if order's already in the database return false
            List<int> allOrderIds = db.Orders.Select(o => o.OrderID).ToList();
            if (allOrderIds.Contains(order.OrderID))
            {
                return false;
            }

            // otherwise add order
            db.Orders.Add(order);
            db.SaveChanges();

            // add all the information to OrderToProducts table
            foreach (var product in products)
            {
                OrdersToProduct op = new OrdersToProduct();
                op.OrderID = order.OrderID;
                op.ProductID = product.Key.ProductID;
                op.ProductQuantity = product.Value;
                db.OrdersToProducts.Add(op);
            }

            db.SaveChanges();
            return true;
        }

        // remove an order
        public static bool RemoveOrder(Order order, ECommerceEntities db)
        {
            // check if order's in the database
            // if it is, remove it and return true
            List<int> allOrderIds = db.Orders.Select(o => o.OrderID).ToList();
            if (allOrderIds.Contains(order.OrderID))
            {
                db.Orders.Remove(order);

                //remove all the entries from OrdersToProducts table
                var opEntries = db.OrdersToProducts.Where(op => op.OrderID == order.OrderID).ToList();
                foreach (var entry in opEntries)
                {
                    db.OrdersToProducts.Remove(entry);
                }
                db.SaveChanges();
                return true;
            }

            // otherwise return false
            return false;

        }

        // edit an order
        public static bool EditOrder(Order order, string name = "", string phone = "", string email = "", string address = "")
        {
            using (var db = new ECommerceEntities())
            {
                // check if order's in the database
                // if it isn't return false right away
                List<int> allOrderIds = db.Orders.Select(o => o.OrderID).ToList();
                if (!allOrderIds.Contains(order.OrderID))
                {
                    return false;
                }

                // otherwise
                // check which parameters to update
                if (name != "")
                { order.CustomersName = name; }
                if (phone != "")
                { order.CustomersPhone = phone; }
                if (email != "")
                { order.CustomersEmail = email; }
                if (address != "")
                { order.CustomersAddress = address; }

                // update order and return true
                db.Orders.AddOrUpdate(order);
                db.SaveChanges();
                return true;
            }
        }

    }
}
