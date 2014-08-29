<%@ Page Language="C#" MasterPageFile="~/Ecommerce.Master" AutoEventWireup="true" CodeBehind="CheckoutSuccessPage.aspx.cs" Inherits="EcommerceWebApplication.CheckoutSuccessPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p id="errorText" runat="server"></p>
    <div id="Div1" runat="server" class="pageContent">
        <p id="result" runat="server"> </p>
    </div>
</asp:Content>