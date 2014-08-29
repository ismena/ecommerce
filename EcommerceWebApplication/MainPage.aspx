<%@ Page Title="" Language="C#" MasterPageFile="~/Ecommerce.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="EcommerceWebApplication.MainContent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Div1" runat="server" class="pageContent">
        <div runat="server" id="mainImageHolder" class="mainImageHolder"></div> 
        <div runat="server" class="categoryLayout" id="newProductsLayout">
            <p id="newProductsText" runat="server" class="productsText"></p>
            <asp:Table ID="categoryTable" CssClass="contantTable categoryTable" runat="server" cellspacing="0" cellpadding="2"></asp:Table>
            <p id="productsOnSaleText" runat="server" class="productsText"></p>
            <asp:Table ID="saleTable" CssClass="contantTable categoryTable saleTable" runat="server" cellspacing="0" cellpadding="2"></asp:Table>
        </div>
    </div>  
</asp:Content>
