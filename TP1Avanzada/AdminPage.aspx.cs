using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BIZ;

namespace TP1Avanzada
{
    public partial class AdminPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isAdmin = Session["UserId"] != null
                           && (byte)Session["PermissionLevel"] == 0;

            pnlAdmin.Visible = isAdmin;
            pnlNoAccess.Visible = !isAdmin;

            if (isAdmin && !IsPostBack)
                BindBooks();
        }

        private void BindBooks()
        {
            var list = BooksService.GetAll();
            bool empty = list.Count == 0;
            if (empty) list.Add(new Book());
            gvBooks.DataSource = list;
            gvBooks.DataBind();
            if (empty) gvBooks.Rows[0].Visible = false;
        }

        // Este manejador evita el error de RowDeleting
        protected void gvBooks_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // No hacemos nada, ya se procesa en RowCommand
            e.Cancel = true;
        }

        protected void ValidateYear_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (int.TryParse(args.Value, out int y)
                && y >= 1000 && y <= DateTime.Now.Year)
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void gvBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Insert")
            {
                if (!Page.IsValid) return;

                var f = gvBooks.FooterRow;
                var title = ((TextBox)f.FindControl("txtNewTitle")).Text.Trim();
                var author = ((TextBox)f.FindControl("txtNewAuthor")).Text.Trim();
                var yearTxt = ((TextBox)f.FindControl("txtNewYear")).Text.Trim();
                int? year = int.TryParse(yearTxt, out var y) ? y : (int?)null;
                var isbn = ((TextBox)f.FindControl("txtNewISBN")).Text.Trim();

                BooksService.AddBook(new Book
                {
                    Title = title,
                    Author = author,
                    YearPub = year,
                    ISBN = isbn
                });
                BindBooks();
            }
            else if (e.CommandName == "Delete")
            {
                int idx = int.Parse(e.CommandArgument.ToString());
                int id = (int)gvBooks.DataKeys[idx].Value;
                BooksService.DeleteBook(id);
                BindBooks();
            }
        }

        protected void gvBooks_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBooks.EditIndex = e.NewEditIndex;
            BindBooks();
        }

        protected void gvBooks_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBooks.EditIndex = -1;
            BindBooks();
        }

        protected void gvBooks_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (!Page.IsValid) return;

            int idx = e.RowIndex;
            int id = (int)gvBooks.DataKeys[idx].Value;
            var row = gvBooks.Rows[idx];
            var title = ((TextBox)row.FindControl("txtEditTitle")).Text.Trim();
            var author = ((TextBox)row.FindControl("txtEditAuthor")).Text.Trim();
            var yearTxt = ((TextBox)row.FindControl("txtEditYear")).Text.Trim();
            int? year = int.TryParse(yearTxt, out var y) ? y : (int?)null;
            var isbn = ((TextBox)row.FindControl("txtEditISBN")).Text.Trim();

            BooksService.Update(new Book
            {
                BookId = id,
                Title = title,
                Author = author,
                YearPub = year,
                ISBN = isbn
            });

            gvBooks.EditIndex = -1;
            BindBooks();
        }
    }
}
