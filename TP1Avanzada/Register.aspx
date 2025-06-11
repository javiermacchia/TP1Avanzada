<%@ Page Title="Registro"
         Language="C#"
         MasterPageFile="~/Site.Master"
         AutoEventWireup="true"
         CodeBehind="Register.aspx.cs"
         Inherits="TP1Avanzada.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

  <!-- Wrapper de fondo pastel -->
  <div class="register-bg py-5">
    <!-- Aquí centramos el form y limitamos ancho a 300px -->
    <div class="registration-form-wrapper">

      <div class="bg-white bg-opacity-75 rounded p-4">
        <h2>Registro de nuevo usuario</h2>
        <asp:ValidationSummary runat="server" CssClass="text-danger" />

        <!-- Nombre -->
        <div class="form-group mb-3">
          <asp:Label runat="server" AssociatedControlID="txtFirstName">Nombre:</asp:Label>
          <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" />
          <asp:RequiredFieldValidator ControlToValidate="txtFirstName"
              ErrorMessage="* Campo obligatorio"
              CssClass="text-danger"
              runat="server" />
        </div>

        <!-- Apellido -->
        <div class="form-group mb-3">
          <asp:Label runat="server" AssociatedControlID="txtLastName">Apellido:</asp:Label>
          <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" />
          <asp:RequiredFieldValidator ControlToValidate="txtLastName"
              ErrorMessage="* Campo obligatorio"
              CssClass="text-danger"
              runat="server" />
        </div>

        <!-- Usuario -->
        <div class="form-group mb-3">
          <asp:Label runat="server" AssociatedControlID="txtUsername">Usuario:</asp:Label>
          <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
          <asp:RequiredFieldValidator ControlToValidate="txtUsername"
               ErrorMessage="* Campo obligatorio"
               CssClass="text-danger"
               runat="server" />
        </div>

        <!-- Email -->
        <div class="form-group mb-3">
          <asp:Label runat="server" AssociatedControlID="txtEmail">Email:</asp:Label>
          <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
          <asp:RequiredFieldValidator ControlToValidate="txtEmail"
               ErrorMessage="* Campo obligatorio"
               CssClass="text-danger"
               runat="server" />
          <asp:RegularExpressionValidator ControlToValidate="txtEmail"
               ValidationExpression="\w+([-+.'']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
               ErrorMessage="Email inválido"
               CssClass="text-danger"
               runat="server" />
        </div>

        <!-- Contraseña -->
        <div class="form-group mb-3">
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
        <div class="form-group mb-4">
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
                    CssClass="btn btn-success w-100"
                    OnClick="BtRegister_Click" />

        <!-- Mensaje resultado -->
        <div class="mt-3 text-center">
          <asp:Label ID="lblMessage"
                     runat="server"
                     CssClass=""
                     Visible="false" />
        </div>
      </div>

    </div>
  </div>

</asp:Content>
