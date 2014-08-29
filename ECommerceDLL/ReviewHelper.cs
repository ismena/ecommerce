using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDLL
{
    public class ReviewHelper
    {
        // add new reviw 
        public static bool AddNewReview(Review review)
        {
            using (var db = new ECommerceEntities())
            {
                // if review's already in the database return false
                List<int> allReviewIds = db.Reviews.Select(r => r.ReviewID).ToList();
                if (allReviewIds.Contains(review.ReviewID))
                {
                    return false;
                }

                // otherwise add review and return true
                db.Reviews.Add(review);
                db.SaveChanges();
                return true;
            }
        }

        // remove review
        public static bool RemoveReview(Review review)
        {
            using (var db = new ECommerceEntities())
            {
                // check if review's in the database
                // if it is, remove it and return true
                List<int> allReviewIds = db.Reviews.Select(r => r.ReviewID).ToList();
                if (allReviewIds.Contains(review.ReviewID))
                {
                    db.Reviews.Remove(review);
                    db.SaveChanges();
                    return true;
                }

                // otherwise return false
                return false;
            }
        }

        // edit review
        public static bool EditCategory(Review review, string text = "")
        {
            using (var db = new ECommerceEntities())
            {
                // check if review's in the database
                // if it isn't return false right away
                List<int> allReviewIds = db.Reviews.Select(r => r.ReviewID).ToList();
                if (!allReviewIds.Contains(review.ReviewID))
                {
                    return false;
                }

                // otherwise
                if (text != "")
                { review.ReviewText = text; }

                // update review and return true
                db.Reviews.AddOrUpdate(review);
                db.SaveChanges();
                return true;
            }
        }

        // get reviews for a product
        public static List<Review> FindReviewsForProduct(Product product)
        {
            using (var db = new ECommerceEntities())
            {
                return db.Reviews.Where(r => r.ProductID == product.ProductID).ToList();
            }
        }
    }
}
