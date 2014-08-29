<%@ Page Language="C#" MasterPageFile="~/Ecommerce.Master" AutoEventWireup="true" CodeBehind="CartPage.aspx.cs" Inherits="EcommerceWebApplication.CartPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="JavaScripts/tipTip.css" />
    <script type="text/javascript" src="JavaScripts/jquery/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="JavaScripts/jquery/ui/jquery-ui.js"></script> 
    <script type="text/javascript" src="JavaScripts/jquery.tipTip.js"></script>

    <script type="text/javascript">
        var numberOfArrays = 1;

        var neededVars1 = {};
        neededVars1['nameTb'] = 'NotEmpty';
        neededVars1['addressTb'] = 'NotEmpty';
        neededVars1['phoneTb'] = 'CheckPhone';
        neededVars1['emailTb'] = 'NotEmpty';

        var buttonId1 = "checkoutButton";

    </script>
    <script type="text/javascript" src="JavaScripts/Validation.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pageContent">
        <div runat="server" id="breadcrumbContainer" class="breadcrumbContainer"></div>
        <p id="errorText" runat="server"></p>
        <div runat="server" class="categoryLayout" id="productLayout">
            <asp:Table ID="productTable" CssClass="contantTable productTableCart" runat="server" cellspacing="0" cellpadding="2"></asp:Table>
            <div id="sumContainer" class="sumContainer" runat="server"></div>
            <div id="formContainer" runat="server">
                <asp:Table ID="formTable" CssClass="contantTable formTable" runat="server" cellspacing="0" cellpadding="2"></asp:Table>
            </div>
        </div>
    </div>
</asp:Content>