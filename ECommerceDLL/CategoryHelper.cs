using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDLL
{
    public class CategoryHelper
    {
        // add new category 
        public static bool AddNewCategory(Category category)
        {
            using (var db = new ECommerceEntities())
            {
                // if category's already in the database return false
                List<int> allCategoryIds = db.Categories.Select(c => c.CategoryID).ToList();
                if (allCategoryIds.Contains(category.CategoryID))
                {
                    return false;
                }

                // otherwise add category and return true
                db.Categories.Add(category);
                db.SaveChanges();
                return true;
            }
        }

        // remove an order
        public static bool RemoveCategory(Category category)
        {
            using (var db = new ECommerceEntities())
            {
                // check if category's in the database
                // if it is, remove it and return true
                List<int> allCategoryIds = db.Categories.Select(c => c.CategoryID).ToList();
                if (allCategoryIds.Contains(category.CategoryID))
                {
                    db.Categories.Remove(category);
                    db.SaveChanges();
                    return true;
                }

                // otherwise return false
                return false;
            }
        }

        // edit an category
        public static bool EditCategory(Category category, string name = "")
        {
            using (var db = new ECommerceEntities())
            {
                // check if category's in the database
                // if it isn't return false right away
                List<int> allCategoryIds = db.Categories.Select(c => c.CategoryID).ToList();
                if (!allCategoryIds.Contains(category.CategoryID))
                {
                    return false;
                }

                // otherwise
                if (name != "")
                { category.CategoryName = name; }
                
                // update category (hope that works!) and return true
                db.Categories.AddOrUpdate(category);
                db.SaveChanges();
                return true;
            }
        }

        public static List<Category> GetAllCategories()
        {
            using (var db = new ECommerceEntities())
            {
                return db.Categories.ToList();
            }
        }

        public static Category GetCategoryById(int id)
        {
            using (var db = new ECommerceEntities())
            {
                return db.Categories.FirstOrDefault(c => c.CategoryID == id);
            }
        }
    }
}
