using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ECommerceDLL;

namespace EcommerceWebApplication
{
    public static class ProductDrawer
    {
        // draw each product
        public static void DrawProduct(Product product, TableRow tr)
        {

            TableCell productCell = new TableCell();

            HyperLink productLink = new HyperLink();
            productLink.NavigateUrl = "/ProductPage.aspx?productID=" + product.ProductID.ToString();
            productLink.Text = product.ProductName;


            HtmlGenericControl productInfoContainer = new HtmlGenericControl("div");
            productInfoContainer.InnerHtml = "<a href='/ProductPage.aspx?productID=" + product.ProductID.ToString() +
                "'><img class='smallProductImage' src='/productImages/" + product.ProductImage + "'></a><br/>";
            productInfoContainer.InnerHtml += "<br/><a href='/ProductPage.aspx?productID=" + product.ProductID.ToString() +
                                              "'>" + product.ProductName + "</a>";
            productInfoContainer.InnerHtml += "<br/><br/>" + product.ProductPrice.ToString("C");
            productInfoContainer.Attributes["class"] = "productInCategory";

            productCell.Controls.Add(productInfoContainer);
            tr.Cells.Add(productCell);
        }

        public static void DrawProductOnSale(Product product, TableRow tr)
        {
            if (product.ProductSalePrice == null)
            {
                return;
            }

            double salePrice = (double)product.ProductSalePrice;
            TableCell productCell = new TableCell();

            HyperLink productLink = new HyperLink();
            productLink.NavigateUrl = "/ProductPage.aspx?productID=" + product.ProductID.ToString();
            productLink.Text = product.ProductName;


            HtmlGenericControl productInfoContainer = new HtmlGenericControl("div");
            productInfoContainer.InnerHtml = "<a href='/ProductPage.aspx?productID=" + product.ProductID.ToString() +
                "'><img class='smallProductImage' src='/productImages/" + product.ProductImage + "'></a><br/>";
            productInfoContainer.InnerHtml += "<br/><a href='/ProductPage.aspx?productID=" + product.ProductID.ToString() +
                                              "'>" + product.ProductName + "</a>";
            productInfoContainer.InnerHtml += "<br/><br/><s>" + product.ProductPrice.ToString("C") + "</s>";

            productInfoContainer.InnerHtml += "<p class='newProductPrice'>" + salePrice.ToString("C") + "</p>";
            productInfoContainer.Attributes["class"] = "productInCategory";

            productCell.Controls.Add(productInfoContainer);
            tr.Cells.Add(productCell);
        }

      

    }
}