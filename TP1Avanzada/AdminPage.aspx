<%@ Page Title="Panel Admin"
         Language="C#"
         MasterPageFile="~/Site.Master"
         AutoEventWireup="true"
         CodeBehind="AdminPage.aspx.cs"
         Inherits="TP1Avanzada.AdminPage"
         EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
    <h2>Administración de Libros</h2>

    <asp:GridView ID="gvBooks"
                  runat="server"
                  CssClass="table table-striped bg-light"
                  DataKeyNames="BookId"
                  AutoGenerateColumns="false"
                  ShowFooter="True"
                  OnRowCommand="gvBooks_RowCommand"
                  OnRowEditing="gvBooks_RowEditing"
                  OnRowCancelingEdit="gvBooks_RowCancelingEdit"
                  OnRowUpdating="gvBooks_RowUpdating"
                  OnRowDeleting="gvBooks_RowDeleting">
      <Columns>
        <asp:BoundField DataField="BookId" HeaderText="ID" ReadOnly="True" />

        <asp:TemplateField HeaderText="Título">
          <ItemTemplate><%# Eval("Title") %></ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox ID="txtEditTitle" runat="server"
                         Text='<%# Bind("Title") %>' CssClass="form-control" />
          </EditItemTemplate>
          <FooterTemplate>
            <asp:TextBox ID="txtNewTitle" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvNewTitle" runat="server"
               ControlToValidate="txtNewTitle"
               ErrorMessage="* Título obligatorio"
               CssClass="text-danger" Display="Dynamic" />
          </FooterTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Autor">
          <ItemTemplate><%# Eval("Author") %></ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox ID="txtEditAuthor" runat="server"
                         Text='<%# Bind("Author") %>' CssClass="form-control" />
          </EditItemTemplate>
          <FooterTemplate>
            <asp:TextBox ID="txtNewAuthor" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvNewAuthor" runat="server"
               ControlToValidate="txtNewAuthor"
               ErrorMessage="* Autor obligatorio"
               CssClass="text-danger" Display="Dynamic" />
          </FooterTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Año">
          <ItemTemplate><%# Eval("YearPub") %></ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox ID="txtEditYear" runat="server"
                         Text='<%# Bind("YearPub") %>' CssClass="form-control" />
            <asp:CustomValidator ID="cvEditYear" runat="server"
               ControlToValidate="txtEditYear"
               ErrorMessage="Año inválido"
               CssClass="text-danger" Display="Dynamic"
               OnServerValidate="ValidateYear_ServerValidate" />
          </EditItemTemplate>
          <FooterTemplate>
            <asp:TextBox ID="txtNewYear" runat="server" CssClass="form-control" />
            <asp:CustomValidator ID="cvNewYear" runat="server"
               ControlToValidate="txtNewYear"
               ErrorMessage="Año inválido"
               CssClass="text-danger" Display="Dynamic"
               OnServerValidate="ValidateYear_ServerValidate" />
          </FooterTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="ISBN">
          <ItemTemplate><%# Eval("ISBN") %></ItemTemplate>
          <EditItemTemplate>
            <asp:TextBox ID="txtEditISBN" runat="server"
                         Text='<%# Bind("ISBN") %>' CssClass="form-control" />
          </EditItemTemplate>
          <FooterTemplate>
            <asp:TextBox ID="txtNewISBN" runat="server" CssClass="form-control" />
          </FooterTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Acciones">
          <ItemTemplate>
            <asp:LinkButton ID="lnkEdit" runat="server"
                            CommandName="Edit"
                            CssClass="btn btn-sm btn-primary me-1">
              Editar
            </asp:LinkButton>
            <asp:LinkButton ID="lnkDelete" runat="server"
                            CommandName="Delete"
                            CssClass="btn btn-sm btn-danger btn-delete"
                            data-row='<%# Container.DataItemIndex %>'>
              Eliminar
            </asp:LinkButton>
          </ItemTemplate>
          <EditItemTemplate>
            <asp:LinkButton ID="lnkUpdate" runat="server"
                            CommandName="Update"
                            CssClass="btn btn-sm btn-success me-1">
              Guardar
            </asp:LinkButton>
            <asp:LinkButton ID="lnkCancel" runat="server"
                            CommandName="Cancel"
                            CssClass="btn btn-sm btn-secondary">
              Cancelar
            </asp:LinkButton>
          </EditItemTemplate>
          <FooterTemplate>
            <asp:LinkButton ID="lnkInsert" runat="server"
                            CommandName="Insert"
                            CssClass="btn btn-sm btn-success w-100">
              Agregar
            </asp:LinkButton>
          </FooterTemplate>
        </asp:TemplateField>

      </Columns>
    </asp:GridView>
  </asp:Panel>

  <asp:Panel ID="pnlNoAccess" runat="server" Visible="false">
    <div class="alert alert-warning">
      <h4>Acceso denegado</h4>
      <p>No tienes permisos para ver esta sección.</p>
      <asp:HyperLink runat="server" NavigateUrl="~/Login.aspx" CssClass="btn btn-primary">
        Iniciar Sesión
      </asp:HyperLink>
    </div>
  </asp:Panel>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
<script type="text/javascript">
    document.addEventListener('DOMContentLoaded', function () {
        document.querySelectorAll('.btn-delete').forEach(function (btn) {
            btn.addEventListener('click', function (e) {
                e.preventDefault();
                var rowIndex = this.getAttribute('data-row'),
                    gridId = '<%= gvBooks.UniqueID %>';
                Swal.fire({
                    title: '¿Eliminar este libro?',
                    text: 'Esta acción no se puede deshacer.',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Sí, eliminar',
                    cancelButtonText: 'Cancelar'
                }).then(function (result) {
                    if (result.isConfirmed) {
                        __doPostBack(gridId, 'Delete$' + rowIndex);
                    }
                });
            });
        });
    });
</script>
</asp:Content>
