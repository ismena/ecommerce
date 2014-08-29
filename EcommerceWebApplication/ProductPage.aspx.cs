using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ECommerceDLL;

namespace EcommerceWebApplication
{
    public partial class ProductPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int productId;
            if (!int.TryParse(Request["productID"], out productId))
            {
                this.errorText.InnerText = "Error: invalid product id.";
            }
            else
            {
                Product product = ProductHelper.GetProductById(productId);
                if (product == null)
                {
                    this.errorText.InnerText = "Error: no product found.";
                }

                else
                {
                    this.lbName.Text = product.ProductName;
                    if (product.ProductOnSale && product.ProductSalePrice != null)
                    {
                        this.lbPrice.CssClass = "productPageCrossedPrice";
                        this.lbSalePrice.Text = ((double)product.ProductSalePrice).ToString("C");
                    }
                    this.lbPrice.Text = product.ProductPrice.ToString("C");
                    this.lbDescription.Text = product.ProductDescription;
                    this.imgProduct.ImageUrl = "/productImages/" + product.ProductImage;

                    DrawBreadcrumbs(product);
                }

            }
        }

        public void AddProduct(object sender, EventArgs e)
        {
            int productId;  
            if (!int.TryParse(Request["productID"], out productId))
            {
                this.errorText.InnerText = "Error: invalid product id.";
                return;
            }

            Product product = ProductHelper.GetProductById(productId);

            Cart cart = Session["cart"] as Cart;
            if (cart == null)
            {
                Session["cart"] = cart = new Cart();
            }

            int quantity;
            if (int.TryParse(this.quantityInput.Text, out quantity))
            {
                cart.Add(product, quantity);
            }
            else
            {
                cart.Add(product);
            }
            Response.Redirect("/CartPage.aspx");
        }

        private void DrawBreadcrumbs(Product product)
        {
            Category category = CategoryHelper.GetCategoryById(product.CategoryID);
            
            HyperLink mainPageLink = new HyperLink();
            mainPageLink.Text = "Main page";
            mainPageLink.NavigateUrl = "/MainPage.aspx";
            mainPageLink.CssClass = "breadcrumbLink";

            Label navigationArrow = new Label();
            navigationArrow.Text = ">>";
            navigationArrow.CssClass = "breadcrumbLink";

            this.breadcrumbContainer.Controls.Add(mainPageLink);
            this.breadcrumbContainer.Controls.Add(navigationArrow);

            HyperLink categoryLink = new HyperLink();
            categoryLink.Text = category.CategoryName;
            categoryLink.NavigateUrl = "/CategoryPage.aspx?catID=" + category.CategoryID.ToString() + "&page=0";
            categoryLink.CssClass = "breadcrumbLink";

            Label navigationArrow2 = new Label();
            navigationArrow2.Text = ">>";
            navigationArrow2.CssClass = "breadcrumbLink";

            this.breadcrumbContainer.Controls.Add(categoryLink);
            this.breadcrumbContainer.Controls.Add(navigationArrow2);

            HyperLink productLink = new HyperLink();
            productLink.Text = product.ProductName;
            productLink.NavigateUrl = "/ProductPage.aspx?productID=" + product.ProductID.ToString();
            productLink.CssClass = "breadcrumbLink";

            this.breadcrumbContainer.Controls.Add(productLink);
        }
    }
}