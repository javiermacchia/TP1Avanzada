<%@ Page Title="Inicio"
         Language="C#"
         MasterPageFile="~/Site.Master"
         AutoEventWireup="true"
         CodeBehind="Default.aspx.cs"
         Inherits="TP1Avanzada.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <link href="~/Content/Site.css" rel="stylesheet" />

  <div class="bg-library">
    <div class="container py-5">

      <div class="jumbotron bg-transparent text-white">
        <h1 class="display-4">Bienvenido a la Biblioteca</h1>
        <p class="lead">Consulta nuestro catálogo de libros y administra tus préstamos.</p>
      </div>

      <asp:Panel ID="pnlNotLogged" runat="server" CssClass="text-center mb-5">
        <asp:HyperLink runat="server" CssClass="btn btn-primary btn-lg me-2" NavigateUrl="~/Login.aspx">
          Iniciar Sesión
        </asp:HyperLink>
        <asp:HyperLink runat="server" CssClass="btn btn-secondary btn-lg" NavigateUrl="~/Register.aspx">
          Registrarse
        </asp:HyperLink>
      </asp:Panel>

      <asp:Panel ID="pnlCatalog" runat="server">

        <p class="lead text-white text-center">
          Hola, <strong><%= Session["UserName"] as string %></strong>!
        </p>

        <div class="input-group mb-4 bg-light p-3 rounded">
          <asp:TextBox ID="txtSearch" runat="server"
                       CssClass="form-control"
                       Placeholder="Buscar por título o autor..." />
          <button class="btn btn-outline-secondary"
                  runat="server" id="btnSearch"
                  onserverclick="btnSearch_Click">
            <i class="fas fa-search"></i> Buscar
          </button>
        </div>

        <asp:Panel ID="pnlNoBooks" runat="server"
                   CssClass="alert alert-info text-center"
                   Visible="false">
          <strong>No se encontraron libros.</strong>
        </asp:Panel>

        <div class="row">
          <asp:Repeater ID="rptBooks" runat="server">
            <HeaderTemplate>
              <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
              <div class="col-md-3 mb-4">
                <div class="card h-100 bg-light">
                  <div class="card-body">
                    <h5 class="card-title"><%# Eval("Title") %></h5>
                    <p class="card-text">
                      <strong>Autor:</strong> <%# Eval("Author") %><br/>
                      <strong>Año:</strong> <%# Eval("YearPub") %>
                    </p>
                  </div>
                </div>
              </div>
            </ItemTemplate>
            <FooterTemplate>
              </div>
            </FooterTemplate>
          </asp:Repeater>
        </div>

      </asp:Panel>

    </div>
  </div>
</asp:Content>
