using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ECommerceDLL;

namespace EcommerceWebApplication
{
    public partial class CategoryPage : System.Web.UI.Page
    {
        public const int PRODUCTS_ON_PAGE = 3;
        public int categoryId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request["catID"], out categoryId))
            {
                this.errorText.InnerText = "Invalid category id was given";
            }
            else
            {
                Category category = CategoryHelper.GetCategoryById(categoryId);
                if (category == null)
                {
                    this.errorText.InnerText = "No category was found";
                }

                else
                {
                    string pageString = Request["page"];
                    int page;

                    if (!int.TryParse(pageString, out page))
                    {
                        page = 0;
                    }
                    DrawCategoryLayout(category, page);
                    DrawBreadcrumbs(category);
                }

            }
            
           

        }

        private void DrawCategoryLayout(Category category, int page)
        {
            // how many products to skip
            int skip = page * PRODUCTS_ON_PAGE;
            
            // hold the quantity of all the products in category
            int productsInCategory;
            var products = ProductHelper.FindProductsInCategory(category, skip, PRODUCTS_ON_PAGE, out productsInCategory);
            if (products.Count < 1)
            {
                this.errorText.InnerText = "No products in selected category";
            }
            else
            {
                // keep track of how many products in a row we have so far
                int productInRowCounter = 0;
                TableRow tr = new TableRow();
                this.categoryTable.Rows.Add(tr);
                foreach (var product in products)
                {
                    if (productInRowCounter != 0 && productInRowCounter%3 == 0)
                    {
                        tr = new TableRow();
                        this.categoryTable.Rows.Add(tr);
                    }

                    if (product.ProductOnSale)
                    {
                        ProductDrawer.DrawProductOnSale(product, tr);
                    }
                    else
                    {
                        ProductDrawer.DrawProduct(product, tr);
                    }
                    
                    productInRowCounter++;
                }
                // if we only have 2 products, create additional td to keep the layout
                if (productInRowCounter == 2)
                {
                    TableCell cell = new TableCell();
                    cell.Width = 300;
                    tr.Cells.Add(cell);

                }

                DrawPagingNavigation(page, productsInCategory);
            }
        }

       

        // draw prev and next links if needed
        private void DrawPagingNavigation(int currentPage, int productCount)
        {
            // if it's not the first page draw prev link
            if (currentPage != 0)
            {
                HyperLink prevLink = new HyperLink();
                prevLink.NavigateUrl = "/CategoryPage.aspx?catID=" + categoryId + "&page=" + (currentPage - 1).ToString();
                prevLink.Text = "&#60;&#60;Prev";
                prevLink.CssClass = "prevLink";

                this.pageLinks.Controls.Add(prevLink);
            }

            // if current page is not the last one, draw next link
            if ((currentPage + 1)*PRODUCTS_ON_PAGE < productCount)
            {
                HyperLink nextLink = new HyperLink();
                nextLink.NavigateUrl = "/CategoryPage.aspx?catID=" + categoryId + "&page=" + (currentPage + 1).ToString();
                nextLink.Text = "Next>>";
                nextLink.CssClass = "nextLink";

                this.pageLinks.Controls.Add(nextLink);
            }
        }

        private void DrawBreadcrumbs(Category category)
        {
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
            categoryLink.NavigateUrl = "/CategoryPage.aspx?catID=" + categoryId + "&page=0";
            categoryLink.CssClass = "breadcrumbLink";

            this.breadcrumbContainer.Controls.Add(categoryLink);
        }
    }
}