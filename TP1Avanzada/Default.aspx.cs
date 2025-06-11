using System;
using System.Collections.Generic;
using System.Web.UI;
using BIZ;

namespace TP1Avanzada
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool loggedIn = Session["UserName"] != null;
            pnlNotLogged.Visible = !loggedIn;
            pnlCatalog.Visible = loggedIn;

            if (loggedIn && !IsPostBack)
                LoadBooks();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var term = txtSearch.Text.Trim();
            List<Book> books = string.IsNullOrEmpty(term)
                ? BooksService.GetAll()
                : BooksService.Search(term);

            pnlNoBooks.Visible = (books.Count == 0);
            rptBooks.Visible = (books.Count > 0);

            BindRepeater(books);
        }

        private void LoadBooks()
        {
            var all = BooksService.GetAll();
            pnlNoBooks.Visible = (all.Count == 0);
            rptBooks.Visible = (all.Count > 0);
            BindRepeater(all);
        }

        private void BindRepeater(List<Book> books)
        {
            rptBooks.DataSource = books;
            rptBooks.DataBind();
        }
    }
}
