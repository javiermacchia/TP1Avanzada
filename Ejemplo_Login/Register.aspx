<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site.Master"
         AutoEventWireup="true" CodeBehind="Register.aspx.cs"
         Inherits="Ejemplo_Login.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">
    <h2>Registro de nuevo usuario</h2>
    
    <asp:ValidationSummary runat="server" CssClass="text-danger" />

    <!-- Usuario -->
    <div class="form-group">
      <asp:Label runat="server" AssociatedControlID="txtUsername">Usuario:</asp:Label>
      <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
      <asp:RequiredFieldValidator ControlToValidate="txtUsername"
           ErrorMessage="* Campo obligatorio"
           CssClass="text-danger"
           runat="server" />
    </div>

    <!-- Email -->
    <div class="form-group">
      <asp:Label runat="server" AssociatedControlID="txtEmail">Email:</asp:Label>
      <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
      <asp:RequiredFieldValidator ControlToValidate="txtEmail"
           ErrorMessage="* Campo obligatorio"
           CssClass="text-danger"
           runat="server" />
      <asp:RegularExpressionValidator ControlToValidate="txtEmail"
           ErrorMessage="Email inválido"
           ValidationExpression="\w+([-+.'']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
           CssClass="text-danger"
           runat="server" />
    </div>

    <!-- Contraseña -->
    <div class="form-group">
      <asp:Label runat="server" AssociatedControlID="txtPassword">Contraseña:</asp:Label>
      <asp:TextBox ID="txtPassword" runat="server"
                   TextMode="Password"
                   CssClass="form-control" />
      <asp:RequiredFieldValidator ControlToValidate="txtPassword"
           ErrorMessage="* Campo obligatorio"
           CssClass="text-danger"
           runat="server" />
    </div>

    <!-- Confirmar contraseña -->
    <div class="form-group">
      <asp:Label runat="server" AssociatedControlID="txtConfirmPassword">
        Confirmar contraseña:
      </asp:Label>
      <asp:TextBox ID="txtConfirmPassword" runat="server"
                   TextMode="Password"
                   CssClass="form-control" />
      <asp:RequiredFieldValidator ControlToValidate="txtConfirmPassword"
           ErrorMessage="* Campo obligatorio"
           CssClass="text-danger"
           runat="server" />
      <asp:CompareValidator ControlToValidate="txtConfirmPassword"
           ControlToCompare="txtPassword"
           ErrorMessage="Las contraseñas no coinciden"
           CssClass="text-danger"
           runat="server" />
    </div>

    <!-- Botón Registrar -->
    <asp:Button ID="BtRegister"
                runat="server"
                Text="Registrarse"
                CssClass="btn btn-success"
                OnClick="BtRegister_Click" />

    <!-- Mensaje resultado -->
    <div class="mt-3">
      <asp:Label ID="lblMessage"
                 runat="server"
                 CssClass=""
                 Visible="false" />
    </div>
  </div>
</asp:Content>
