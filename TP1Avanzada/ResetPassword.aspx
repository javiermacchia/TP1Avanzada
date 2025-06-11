<%@ Page Title="Nueva contraseña"
         Language="C#" 
         MasterPageFile="~/Site.Master"
         AutoEventWireup="true"
         CodeBehind="ResetPassword.aspx.cs"
         Inherits="TP1Avanzada.ResetPassword" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

  <div class="register-bg py-5">
    <div class="registration-form-wrapper">
      <div class="bg-white bg-opacity-75 rounded p-4">
        <h2>Establecer nueva contraseña</h2>

        <asp:Label ID="litError" runat="server" Visible="false" CssClass="text-danger" />

        <asp:Panel ID="pnlForm" runat="server" Visible="false">
          <asp:ValidationSummary runat="server" CssClass="text-danger" />

          <div class="form-group mb-3">
            <asp:Label runat="server" AssociatedControlID="txtPassword">Nueva contraseña:</asp:Label>
            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ControlToValidate="txtPassword"
                ErrorMessage="* Obligatorio" CssClass="text-danger" runat="server" />
          </div>

          <div class="form-group mb-4">
            <asp:Label runat="server" AssociatedControlID="txtConfirm">Confirmar contraseña:</asp:Label>
            <asp:TextBox ID="txtConfirm" TextMode="Password" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ControlToValidate="txtConfirm"
                ErrorMessage="* Obligatorio" CssClass="text-danger" runat="server" />
            <asp:CompareValidator ControlToValidate="txtConfirm"
                ControlToCompare="txtPassword"
                ErrorMessage="Las contraseñas no coinciden"
                CssClass="text-danger" runat="server" />
          </div>

          <asp:Button ID="btnReset" runat="server"
              Text="Restablecer contraseña"
              CssClass="btn btn-success w-100"
              OnClick="btnReset_Click" />
        </asp:Panel>
      </div>
    </div>
  </div>

</asp:Content>
