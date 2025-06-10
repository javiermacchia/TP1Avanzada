<%@ Page Title="Mi perfil"
         Language="C#"
         MasterPageFile="~/Site.Master"
         AutoEventWireup="true"
         CodeBehind="ProfilePage.aspx.cs"
         Inherits="Ejemplo_Login.ProfilePage" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <h2>Mi perfil</h2>
  <asp:ValidationSummary runat="server" CssClass="text-danger" />

  <div class="form-group">
    <asp:Label runat="server" AssociatedControlID="txtUsername">Usuario:</asp:Label>
    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
    <asp:RequiredFieldValidator ControlToValidate="txtUsername"
        ErrorMessage="* Obligatorio" CssClass="text-danger" runat="server" />
  </div>

  <div class="form-group">
    <asp:Label runat="server" AssociatedControlID="txtEmail">Email:</asp:Label>
    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
    <asp:RequiredFieldValidator ControlToValidate="txtEmail"
        ErrorMessage="* Obligatorio" CssClass="text-danger" runat="server" />
    <asp:RegularExpressionValidator ControlToValidate="txtEmail"
        ValidationExpression="\w+([-+.'']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        ErrorMessage="Email inválido" CssClass="text-danger" runat="server" />
  </div>

  <div class="form-group">
    <asp:Label runat="server" AssociatedControlID="txtPassword">Nueva contraseña:</asp:Label>
    <asp:TextBox ID="txtPassword" runat="server"
                 TextMode="Password" CssClass="form-control" />
  </div>

  <div class="form-group">
    <asp:Label runat="server" AssociatedControlID="txtConfirm">Confirmar contraseña:</asp:Label>
    <asp:TextBox ID="txtConfirm" runat="server"
                 TextMode="Password" CssClass="form-control" />
    <asp:CompareValidator ControlToValidate="txtConfirm"
        ControlToCompare="txtPassword"
        ErrorMessage="Las contraseñas no coinciden"
        CssClass="text-danger" runat="server" />
  </div>

  <asp:Button ID="btnUpdate"
              runat="server"
              Text="Guardar cambios"
              CssClass="btn btn-primary"
              OnClick="btnUpdate_Click" />

  <div class="mt-3">
    <asp:Label ID="lblMsg" runat="server" Visible="false" />
  </div>
</asp:Content>
