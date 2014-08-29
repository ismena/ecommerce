using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceWebApplication
{
    public partial class CheckoutSuccessPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string success = Request["success"];
            if (success == "true")
            {
                this.result.InnerText = "You order has been succesfully submitted! Thank you for shopping with us.";
            }
            else
            {
                this.result.InnerText = "Something went wrong. But this is still a good program, you need to believe me!";
            }
        }
    }
}