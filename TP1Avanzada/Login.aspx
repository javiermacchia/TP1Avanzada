<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master"
         AutoEventWireup="true" CodeBehind="Login.aspx.cs"
         Inherits="TP1Avanzada.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="container">
    <!-- Usuario -->
    <div class="row">
      <div class="col-md-4"></div>
      <div class="col-md-4">
        <label for="Txusuario">Usuario:</label>
        <asp:TextBox ID="Txusuario" CssClass="form-control" runat="server" />
      </div>
      <div class="col-md-4"></div>
    </div>

    <!-- Password -->
    <div class="row mt-2">
      <div class="col-md-4"></div>
      <div class="col-md-4">
        <label for="TxPassword">Password:</label>
        <asp:TextBox ID="TxPassword"
                     TextMode="Password"
                     CssClass="form-control"
                     runat="server" />
      </div>
      <div class="col-md-4"></div>
    </div>

    <!-- Botón Ingresar -->
    <div class="row mt-3">
      <div class="col-md-4"></div>
      <div class="col-md-4">
        <asp:Button ID="BtLogin"
                    runat="server"
                    Text="Ingresar"
                    CssClass="btn btn-primary w-100"
                    OnClick="BtLogin_Click" />
      </div>
      <div class="col-md-4"></div>
    </div>

    <!-- Links de registro/recuperar -->
    <div class="row mt-2">
      <div class="col-md-4"></div>
      <div class="col-md-4 d-flex justify-content-between">
        <a href="Register.aspx">Regístrese</a>
        <a href="Recovery.aspx">Recuperar contraseña</a>
      </div>
      <div class="col-md-4"></div>
    </div>

    <!-- Mensaje de resultado -->
    <div class="row mt-3">
      <div class="col-md-4"></div>
      <div class="col-md-4">
        <asp:Label ID="lt_mensaje"
                   runat="server"
                   Visible="false"
                   CssClass="text-danger" />
      </div>
      <div class="col-md-4"></div>
    </div>
  </div>
</asp:Content>
