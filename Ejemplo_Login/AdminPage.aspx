<%@ Page Title="Panel de Administrador"
         Language="C#"
         MasterPageFile="~/Site.Master"
         AutoEventWireup="true"
         CodeBehind="AdminPage.aspx.cs"
         Inherits="Ejemplo_Login.AdminPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="mt-4">
    <h2>Panel de Administrador</h2>
    <p>Bienvenido al área de administración. Aquí puedes gestionar la aplicación.</p>

    <asp:GridView ID="gvUsers"
                  runat="server"
                  AutoGenerateColumns="false"
                  CssClass="table table-striped">
      <Columns>
        <asp:BoundField DataField="UserId" HeaderText="ID" />
        <asp:BoundField DataField="Username" HeaderText="Usuario" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="PermissionLevel" HeaderText="Nivel" />
        <asp:BoundField DataField="CreatedAt" HeaderText="Creado" DataFormatString="{0:yyyy-MM-dd}" />
      </Columns>
    </asp:GridView>
  </div>
</asp:Content>
