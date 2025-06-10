<%@ Page Title="Inicio"
         Language="C#"
         MasterPageFile="~/Site.Master"
         AutoEventWireup="true"
         CodeBehind="Default.aspx.cs"
         Inherits="TP1Avanzada.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="jumbotron mt-4">
    <h1>Bienvenido a Mi Aplicación</h1>
    <p>Esta es la página principal. Desde aquí puedes navegar por las diferentes secciones.</p>

    <% if (Session["UserName"] == null) { %>
      <!-- Usuario no logueado -->
      <p>
        <asp:HyperLink runat="server" CssClass="btn btn-primary btn-lg me-2" NavigateUrl="~/Login.aspx">
          Iniciar Sesión
        </asp:HyperLink>
        <asp:HyperLink runat="server" CssClass="btn btn-secondary btn-lg" NavigateUrl="~/Register.aspx">
          Registrarse
        </asp:HyperLink>
      </p>
    <% } else { %>
      <!-- Usuario logueado -->
      <p class="lead">
        Hola, <strong><%= Session["UserName"] as string %></strong>!
      </p>
      <p>
        <asp:HyperLink runat="server" CssClass="btn btn-success" NavigateUrl="~/Default.aspx">
          Test
        </asp:HyperLink>
      </p>
    <% } %>
  </div>
</asp:Content>
