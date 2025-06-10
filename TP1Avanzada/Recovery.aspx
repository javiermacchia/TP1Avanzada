<%@ Page Title="Recuperar contraseña"
         Language="C#"
         MasterPageFile="~/Site.Master"
         AutoEventWireup="true"
         CodeBehind="Recovery.aspx.cs"
         Inherits="TP1Avanzada.Recovery" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <h2>Recuperar contraseña</h2>
  <asp:ValidationSummary runat="server" CssClass="text-danger" />

  <div class="form-group">
    <asp:Label runat="server" AssociatedControlID="txtEmail">Email:</asp:Label>
    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
    <asp:RequiredFieldValidator ControlToValidate="txtEmail"
        ErrorMessage="* Campo obligatorio" CssClass="text-danger" runat="server" />
    <asp:RegularExpressionValidator ControlToValidate="txtEmail"
        ValidationExpression="\w+([-+.'']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        ErrorMessage="Email inválido" CssClass="text-danger" runat="server" />
  </div>

  <asp:Button ID="btnSend" runat="server"
      Text="Enviar enlace de recuperación"
      CssClass="btn btn-primary"
      OnClick="btnSend_Click" />

  <div class="mt-3">
    <asp:Label ID="lblMsg" runat="server" Visible="false" />
  </div>
</asp:Content>
