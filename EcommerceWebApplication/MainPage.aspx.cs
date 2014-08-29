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
    public partial class MainContent : System.Web.UI.Page
    {

        // delegat to draw products in different context
        private delegate void DrawProduct(Product product, TableRow tr);
        
        protected void Page_Load(object sender, EventArgs e)
        {
            SetMainImage();

            // show most recently added products on the page
            DrawProductLayout(this.newProductsText, categoryTable, "New products", "There is no products yet", ProductHelper.GetNewProducts(6), ProductDrawer.DrawProduct);

            // show products on sale
            DrawProductLayout(this.productsOnSaleText, saleTable, "Products on sale", "There is no products currently on sale", ProductHelper.GetProductsOnSale(6, 0), ProductDrawer.DrawProductOnSale);


        }

        private void SetMainImage()
        {
            Image mainImage = new Image();
            mainImage.ImageUrl = "mainpic.jpg";
            mainImage.CssClass = "mainImage";

            this.mainImageHolder.Controls.Add(mainImage);
        }

        private void DrawProductLayout(HtmlGenericControl p, Table table, string success, string fail, List<Product> products, DrawProduct DrawFunction)
        {
            
            if (products.Count < 1)
            {
                p.InnerText = fail;
            }
            else
            {
                p.InnerText = success; 
                
                // keep track of how many products in a row we have so far
                int productInRowCounter = 0;
                TableRow tr = new TableRow();
               table.Rows.Add(tr);
                foreach (var product in products)
                {
                    if (productInRowCounter != 0 && productInRowCounter % 3 == 0)
                    {
                        tr = new TableRow();
                        table.Rows.Add(tr);
                    }
                    DrawFunction(product, tr);
                    productInRowCounter++;
                }
            }
        }





 


    }
}