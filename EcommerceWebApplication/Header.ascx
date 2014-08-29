<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="EcommerceWebApplication.Header" %>

<div runat="server" class="headerContainer">
    <div runat="server" class="headerItem" id="logoContainer" ClientIDMode="Static">
        
    </div>
    <div runat="server" class="headerItem">
        <input runat="server" type="text" id="searchInput" class="searchInput" placeholder="Search for products"/>
        <asp:Button runat="server" ID="SearchButton" CssClass="searchButton" Text="GO" OnClick="SearchButton_Clicked"/> 
    </div>
    <div runat="server" class="headerItem" id="cartContainer"></div>
    <div runat="server" class="categoryContainer" id="categoryContainer"></div>
</div>