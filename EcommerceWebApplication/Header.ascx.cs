using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ECommerceDLL;

namespace EcommerceWebApplication
{
    public partial class Header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetLogoImage();
            
            InitializeCategories();
            
            SetCart();

        }

        protected void SearchButton_Clicked(object sender, EventArgs e)
        {
            string keywords = searchInput.Value.Trim();
            Response.Redirect("/Search.aspx?text=" + keywords + "&page=0");
        }

        private void InitializeCategories()
        {
            var categoryList = CategoryHelper.GetAllCategories();
            this.categoryContainer.InnerHtml = "<ul class='categoryMenu'>";
            int idMaker = 1;
            foreach (var category in categoryList)
            {
                HyperLink categoryLink = new HyperLink();
                categoryLink.NavigateUrl = "/CategoryPage.aspx?catID=" + category.CategoryID.ToString();
                categoryLink.Text = category.CategoryName.ToLower();
                this.categoryContainer.InnerHtml += "<li class='categoryItem'><a class='categoryItem" + idMaker.ToString() +
                    "' href='" + categoryLink.NavigateUrl + "'>" + categoryLink.Text + "</a></li>";
                idMaker++;

            }
            this.categoryContainer.InnerHtml += "</ul>";
        }

        private void SetCart()
        {
            Cart cart = Session["cart"] as Cart;
            if (cart == null)
            {
                Session["cart"] = cart = new Cart();
            }

            HyperLink linkToCart = new HyperLink();
            linkToCart.NavigateUrl = "/CartPage.aspx";
            linkToCart.Text = String.Format("Cart({0})", cart.Count().ToString());
            this.cartContainer.InnerHtml = "<a class='cartLink' href='" + linkToCart.NavigateUrl + "'>" +
                                           linkToCart.Text + "</a>";

        }

        private void SetLogoImage()
        {
            HyperLink logoLink = new HyperLink();
            logoLink.ImageUrl = "logo.png";
            logoLink.ImageWidth = 370;
            logoLink.NavigateUrl = "/MainPage.aspx";

            this.logoContainer.Controls.Add(logoLink);
        }
    }
}