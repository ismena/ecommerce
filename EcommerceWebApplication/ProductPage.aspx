<%@ Page Language="C#" MasterPageFile="~/Ecommerce.Master" AutoEventWireup="true" CodeBehind="ProductPage.aspx.cs" Inherits="EcommerceWebApplication.ProductPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div runat="server" class="pageContent">
        <div runat="server" id="breadcrumbContainer" class="breadcrumbContainer"></div>
        <p id="errorText" runat="server"></p>
        <div runat="server" class="productLayout" id="productLayout">
            <div class="productName">
                <asp:Label runat="server" ID="lbName"/>
              
            </div>
            <div class="productName">
                <asp:Label runat="server" ID="lbPrice" CssClass="productPagePrice"/>
                <asp:Label runat="server" ID="lbSalePrice" CssClass="productPageSalePrice"/>
            </div>
            <div >
                <asp:Image runat="server" ID="imgProduct" CssClass="normalProductImage"/>
            </div>
            <div class="productInfo">
                <asp:Label runat="server" ID="lbDescription"/>
            </div>
            <div class="productInfo">
                 <asp:Label runat="server" CssClass="lbQuantity">Quantity: </asp:Label>
                <asp:TextBox runat="server" Text="1" Width="25" Height="20" CssClass="quantityInput" ID="quantityInput"> </asp:TextBox>
                <asp:Button runat="server" OnClick="AddProduct" CssClass="searchButton" Text="ADD TO CART"/>
            </div>
        </div>
    </div>
</asp:Content>