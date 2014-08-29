<%@ Page Language="C#" MasterPageFile="~/Ecommerce.Master" AutoEventWireup="true" CodeBehind="CategoryPage.aspx.cs" Inherits="EcommerceWebApplication.CategoryPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pageContent">
        <div runat="server" id="breadcrumbContainer" class="breadcrumbContainer"></div>
        <p id="errorText" runat="server"></p>
        <div runat="server" class="categoryLayout" id="categoryLayout">
            <asp:Table ID="categoryTable" CssClass="contantTable categoryTable" runat="server" cellspacing="0" cellpadding="2"></asp:Table>
            <br/>
            <br/>
            <div runat="server" id="pageLinks"></div>
        </div>
    </div>
</asp:Content>