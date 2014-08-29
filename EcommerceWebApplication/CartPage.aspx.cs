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
    public partial class CartPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var cart = GetCart();
            var products = GetProducts(cart);
            this.DrawProducts(products, cart.GetSum());
        }

        private Dictionary<Product, int> GetProducts(Cart cart)
        {   
            if (cart.Count() < 1)
            {
                return null;
            }

            return cart.Products;
        }

        private void DrawProducts(Dictionary<Product, int> products, double sum)
        {
            if (products == null)
            {
                this.errorText.InnerText = "Your cart is empty";
            }
            else
            {
                DrawHeading();
                foreach (var product in products)
                {
                    DrawProduct(product);    
                }

                Label sumLb = new Label();
                sumLb.Text = "Sum: " + sum.ToString("C");
                this.sumContainer.Controls.Add(sumLb);

                DrawCustomerForm();         

            }

            DrawBreadcrumbs();
        }

        protected void RemoveProduct(object sender, EventArgs e, Product product)
        {
            var cart = GetCart();
            cart.Remove(product);
            Response.Redirect("/CartPage.aspx");
        }

        private Cart GetCart()
        {
            Cart cart = Session["cart"] as Cart;
            if (cart == null)
            {
                Session["cart"] = cart = new Cart();
            }
            return cart;
        }

        private void DrawProduct(KeyValuePair<Product, int> product)
        {
            TableRow singleProductContainer = new TableRow();
            singleProductContainer.CssClass = "productCartDisplay";
            this.productTable.Rows.Add(singleProductContainer);


            TableRow spaceRow = new TableRow();
            spaceRow.Height = 10;
            this.productTable.Rows.Add(spaceRow);

            Image img = new Image();
            img.ImageUrl = "/productImages/" + product.Key.ProductImage;
            img.CssClass = "cartProductImage";
            HyperLink imageLink = new HyperLink();
            imageLink.Controls.Add(img);
            imageLink.NavigateUrl = "/ProductPage.aspx?productID=" + product.Key.ProductID.ToString();

            TableCell imageCell = new TableCell();
            imageCell.Controls.Add(imageLink);

            TableCell nameCell = new TableCell();
            HyperLink nameLink = new HyperLink();
            nameLink.NavigateUrl = imageLink.NavigateUrl;
            nameLink.Text = product.Key.ProductName;
            nameCell.Controls.Add(nameLink);

            TableCell productQuantitCell = new TableCell();
            productQuantitCell.Text = product.Value.ToString();

            TableCell priceCell = new TableCell();
            if (product.Key.ProductOnSale && product.Key.ProductSalePrice != null)
            {
                priceCell.Text = ((double)product.Key.ProductSalePrice * product.Value).ToString("C");
            }
            else
            {
                priceCell.Text = (product.Key.ProductPrice * product.Value).ToString("C");
            }
            

            Button removeButton = new Button();
            removeButton.Text = "Remove";
            removeButton.Click += new EventHandler((s, e) => RemoveProduct(s, e, product.Key));
            TableCell removeCell = new TableCell();
            removeCell.Controls.Add(removeButton);

            singleProductContainer.Cells.Add(imageCell);
            singleProductContainer.Cells.Add(nameCell);
            singleProductContainer.Cells.Add(productQuantitCell);
            singleProductContainer.Cells.Add(priceCell);
            singleProductContainer.Cells.Add(removeCell);
        }

        private void DrawHeading()
        {
            TableRow h = new TableRow();
            this.productTable.Rows.Add(h);

            TableRow spaceRow = new TableRow();
            spaceRow.Height = 10;
            this.productTable.Rows.Add(spaceRow);

            TableCell hCell = new TableCell();
            hCell.Text = "Your shopping cart: ";
            hCell.CssClass = "heading";
            h.Cells.Add(hCell);


        }

        // draw form for customer information
        private void DrawCustomerForm()
        {
            // row for a name
            TableRow nameRow = new TableRow();
            this.formTable.Rows.Add(nameRow);

            TableCell nameLbCell = new TableCell();
            nameRow.Cells.Add(nameLbCell);

            Label nameLb = new Label();
            nameLb.Text = "Full name: ";
            nameLbCell.Controls.Add(nameLb);

            TableCell nameTbCell = new TableCell();
            nameRow.Cells.Add(nameTbCell);
            TextBox nameTb = new TextBox();
            nameTb.ID = "nameTb";
            nameTb.ClientIDMode = ClientIDMode.Static;
            nameTb.Attributes.Add("onfocus", "javascript:Validate(this, 1)");
            nameTb.Attributes.Add("onkeyup", "javascript:Validate(this, 1)");
            nameTb.Attributes.Add("oninput", "javascript:Validate(this, 1)");
            nameTb.Attributes.Add("onblur", "javascript:RemoveTip(this)");
            nameTbCell.Controls.Add(nameTb);

            // row for phone #
            TableRow phoneRow = new TableRow();
            this.formTable.Rows.Add(phoneRow);

            TableCell phoneLbCell = new TableCell();
            phoneRow.Cells.Add(phoneLbCell);

            Label phoneLb = new Label();
            phoneLb.Text = "Telephone: ";
            phoneLbCell.Controls.Add(phoneLb);

            TableCell phoneTbCell = new TableCell();
            phoneRow.Cells.Add(phoneTbCell);
            TextBox phoneTb = new TextBox();
            phoneTb.ID = "phoneTb";
            phoneTb.ClientIDMode = ClientIDMode.Static;
            phoneTb.Attributes.Add("onfocus", "javascript:Validate(this, 1)");
            phoneTb.Attributes.Add("onkeyup", "javascript:Validate(this, 1)");
            phoneTb.Attributes.Add("oninput", "javascript:Validate(this, 1)");
            phoneTb.Attributes.Add("onblur", "javascript:RemoveTip(this)");
            phoneTbCell.Controls.Add(phoneTb);

            // row for email
            TableRow emailRow = new TableRow();
            this.formTable.Rows.Add(emailRow);

            TableCell emailLbCell = new TableCell();
            emailRow.Cells.Add(emailLbCell);

            Label emailLb = new Label();
            emailLb.Text = "E-mail: ";
            emailLbCell.Controls.Add(emailLb);

            TableCell emailTbCell = new TableCell();
            emailRow.Cells.Add(emailTbCell);
            TextBox emailTb = new TextBox();
            emailTb.ID = "emailTb";
            emailTb.ClientIDMode = ClientIDMode.Static;
            emailTb.Attributes.Add("onfocus", "javascript:Validate(this, 1)");
            emailTb.Attributes.Add("onkeyup", "javascript:Validate(this, 1)");
            emailTb.Attributes.Add("oninput", "javascript:Validate(this, 1)");
            emailTb.Attributes.Add("onblur", "javascript:RemoveTip(this)");
            emailTbCell.Controls.Add(emailTb);

            // row for address
            TableRow addressRow = new TableRow();
            this.formTable.Rows.Add(addressRow);

            TableCell addressLbCell = new TableCell();
            addressRow.Cells.Add(addressLbCell);

            Label addressLb = new Label();
            addressLb.Text = "Address: ";
            addressLbCell.Controls.Add(addressLb);

            TableCell addressTbCell = new TableCell();
            addressRow.Cells.Add(addressTbCell);
            TextBox addressTb = new TextBox();
            addressTb.Attributes.Add("onfocus", "javascript:Validate(this, 1)");
            addressTb.Attributes.Add("onkeyup", "javascript:Validate(this, 1)");
            addressTb.Attributes.Add("oninput", "javascript:Validate(this, 1)");
            addressTb.Attributes.Add("onblur", "javascript:RemoveTip(this)");
            addressTb.ID = "addressTb";
            addressTb.ClientIDMode = ClientIDMode.Static;
            addressTbCell.Controls.Add(addressTb);

            // checkout button
            Button checkoutButton = new Button();
            checkoutButton.ID = "checkoutButton";
            checkoutButton.ClientIDMode = ClientIDMode.Static;
            checkoutButton.Text = "CHECKOUT";
            checkoutButton.CssClass = "chechoutButton";
            checkoutButton.Click += new EventHandler((s, e) => Checkout(s, e, nameTb.Text, phoneTb.Text, emailTb.Text, addressTb.Text));
            this.formContainer.Controls.Add(checkoutButton);

        }

        protected void Checkout(object sender, EventArgs e, string name, string phone, string email, string address)
        {
            using (var db = new ECommerceEntities())
            {
                Order order = new Order();
                order.OrderDate = DateTime.Now.Date;
                order.CustomersName = name;
                order.CustomersPhone = phone;
                order.CustomersEmail = email;
                order.CustomersAddress = address;

                var cart = GetCart();
                var products = cart.Products;

                var checkoutSuccess = OrderHelper.AddNewOrder(order, db, products);
                if (checkoutSuccess)
                {
                    Session["cart"] = new Cart();
                    Response.Redirect("CheckoutSuccessPage.aspx?success=true");
                }
                else
                {
                    Response.Redirect("CheckoutSuccessPage.aspx?success=false");
                }
            }
        }

        private void DrawBreadcrumbs()
        {
            HyperLink mainPageLink = new HyperLink();
            mainPageLink.Text = "Main page";
            mainPageLink.NavigateUrl = "/MainPage.aspx";
            mainPageLink.CssClass = "breadcrumbLink";

            Label navigationArrow = new Label();
            navigationArrow.Text = ">>";
            navigationArrow.CssClass = "breadcrumbLink";

            HyperLink cartLink = new HyperLink();
            cartLink.CssClass = "breadcrumbLink";
            cartLink.Text = "Shopping cart";
            cartLink.NavigateUrl = "/CartPage.aspx";

            this.breadcrumbContainer.Controls.Add(mainPageLink);
            this.breadcrumbContainer.Controls.Add(navigationArrow);
            this.breadcrumbContainer.Controls.Add(cartLink);

        }
    }


}