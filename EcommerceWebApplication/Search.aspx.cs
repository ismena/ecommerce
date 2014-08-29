using System.Web.UI.HtmlControls;
using ECommerceDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceWebApplication
{
    public partial class Search : System.Web.UI.Page
    {
        public const int PRODUCTS_ON_PAGE = 3;
        private string searchText;
        protected void Page_Load(object sender, EventArgs e)
        {
            searchText = Request["text"];
            string pageString = Request["page"];
            int page;

            if (!int.TryParse(pageString, out page))
            {
                page = 0;
            }

            if (string.IsNullOrEmpty(searchText))
            {
                this.errorText.InnerText = "Search text not specified";
                return;
            }

            int skip = page * PRODUCTS_ON_PAGE;

            // count products for paging
            int countAllProducts;
            var products = ProductHelper.FindProductsByName(searchText, skip, PRODUCTS_ON_PAGE, out countAllProducts);

            if (!products.Any())
            {
                this.errorText.InnerText = "No products found";
                return;
            }
            // keep track of how many products in a row we have so far
            int productInRowCounter = 0;
            TableRow tr = new TableRow();
            this.categoryTable.Rows.Add(tr);
            foreach (var product in products)
            {
                if (productInRowCounter != 0 && productInRowCounter % 3 == 0)
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
            if (productInRowCounter == 2)
            {
                TableCell cell = new TableCell();
                cell.Width = 300;
                tr.Cells.Add(cell);
            }
            DrawPagingNavigation(page, countAllProducts);
        }


        // draw prev and next links if needed
        private void DrawPagingNavigation(int currentPage, int productCount)
        {
            // if it's not the first page draw prev link
            if (currentPage != 0)
            {
                HyperLink prevLink = new HyperLink();
                prevLink.NavigateUrl = "/SearchPage.aspx?text=" + searchText + "&page=" + (currentPage - 1).ToString();
                prevLink.Text = "Prev";
                prevLink.CssClass = "prevLink";

                this.pageLinks.Controls.Add(prevLink);
            }

            // if current page is not the last one, draw next link
            if ((currentPage + 1) * PRODUCTS_ON_PAGE < productCount)
            {
                HyperLink nextLink = new HyperLink();
                nextLink.NavigateUrl = "/SearchPage.aspx?text=" + searchText + "&page=" + (currentPage + 1).ToString();
                nextLink.Text = "Next>>";
                nextLink.CssClass = "nextLink";

                this.pageLinks.Controls.Add(nextLink);
            }
        }
    }
}