<%@ Page Title="Mi perfil"
         Language="C#"
         MasterPageFile="~/Site.Master"
         AutoEventWireup="true"
         CodeBehind="ProfilePage.aspx.cs"
         Inherits="TP1Avanzada.ProfilePage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

  <!-- Fondo y centrar solo el formulario -->
  <div class="profile-bg py-5">
    <div class="registration-form-wrapper">

      <!-- Caja blanca semitransparente con max-width aplicado -->
      <div class="bg-white bg-opacity-75 rounded p-4">
        <h2>Mi perfil</h2>
        <asp:ValidationSummary runat="server" CssClass="text-danger" />

        <!-- Nombre -->
        <div class="form-group mb-3">
          <asp:Label runat="server" AssociatedControlID="txtFirstName">Nombre:</asp:Label>
          <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" />
          <asp:RequiredFieldValidator ControlToValidate="txtFirstName"
              ErrorMessage="* Obligatorio" CssClass="text-danger" runat="server" />
        </div>

        <!-- Apellido -->
        <div class="form-group mb-3">
          <asp:Label runat="server" AssociatedControlID="txtLastName">Apellido:</asp:Label>
          <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" />
          <asp:RequiredFieldValidator ControlToValidate="txtLastName"
              ErrorMessage="* Obligatorio" CssClass="text-danger" runat="server" />
        </div>

        <!-- Usuario -->
        <div class="form-group mb-3">
          <asp:Label runat="server" AssociatedControlID="txtUsername">Usuario:</asp:Label>
          <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
          <asp:RequiredFieldValidator ControlToValidate="txtUsername"
              ErrorMessage="* Obligatorio" CssClass="text-danger" runat="server" />
        </div>

        <!-- Email -->
        <div class="form-group mb-3">
          <asp:Label runat="server" AssociatedControlID="txtEmail">Email:</asp:Label>
          <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
          <asp:RequiredFieldValidator ControlToValidate="txtEmail"
              ErrorMessage="* Obligatorio" CssClass="text-danger" runat="server" />
          <asp:RegularExpressionValidator ControlToValidate="txtEmail"
              ValidationExpression="\w+([-+.'']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
              ErrorMessage="Email inválido" CssClass="text-danger" runat="server" />
        </div>

        <!-- Nueva contraseña -->
        <div class="form-group mb-3">
          <asp:Label runat="server" AssociatedControlID="txtPassword">Nueva contraseña:</asp:Label>
          <asp:TextBox ID="txtPassword" runat="server"
                       TextMode="Password"
                       CssClass="form-control" />
        </div>

        <!-- Confirmar contraseña -->
        <div class="form-group mb-4">
          <asp:Label runat="server" AssociatedControlID="txtConfirm">Confirmar contraseña:</asp:Label>
          <asp:TextBox ID="txtConfirm" runat="server"
                       TextMode="Password"
                       CssClass="form-control" />
          <asp:CompareValidator ControlToValidate="txtConfirm"
              ControlToCompare="txtPassword"
              ErrorMessage="Las contraseñas no coinciden"
              CssClass="text-danger" runat="server" />
        </div>

        <!-- Botón Guardar cambios -->
        <asp:Button ID="btnUpdate"
                    runat="server"
                    Text="Guardar cambios"
                    CssClass="btn btn-primary w-100"
                    OnClick="btnUpdate_Click" />

        <div class="mt-3 text-center">
          <asp:Label ID="lblMsg" runat="server" Visible="false" />
        </div>
      </div>

    </div>
  </div>

</asp:Content>
