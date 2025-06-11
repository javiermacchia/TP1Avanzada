using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BIZ
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int? YearPub { get; set; }
        public string ISBN { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public static class BooksService
    {
        private static string CS => ConfigurationManager
            .ConnectionStrings["SQLCLASE"].ConnectionString;

        public static List<Book> GetAll()
        {
            var list = new List<Book>();
            const string sql = @"
                SELECT BookId, Title, Author, YearPub, ISBN, CreatedAt
                  FROM Books
                 ORDER BY CreatedAt DESC";

            using (var cx = new SqlConnection(CS))
            using (var cm = new SqlCommand(sql, cx))
            {
                cx.Open();
                using (var r = cm.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Book
                        {
                            BookId = (int)r["BookId"],
                            Title = r["Title"].ToString(),
                            Author = r["Author"].ToString(),
                            YearPub = r["YearPub"] as int?,
                            ISBN = r["ISBN"].ToString(),
                            CreatedAt = (DateTime)r["CreatedAt"]
                        });
                    }
                }
            }
            return list;
        }

        public static List<Book> Search(string term)
        {
            var list = new List<Book>();
            const string sql = @"
                SELECT BookId, Title, Author, YearPub, ISBN, CreatedAt
                  FROM Books
                 WHERE Title  LIKE @t
                    OR Author LIKE @t
                 ORDER BY CreatedAt DESC";

            using (var cx = new SqlConnection(CS))
            using (var cm = new SqlCommand(sql, cx))
            {
                cm.Parameters.AddWithValue("@t", "%" + term + "%");
                cx.Open();
                using (var r = cm.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Book
                        {
                            BookId = (int)r["BookId"],
                            Title = r["Title"].ToString(),
                            Author = r["Author"].ToString(),
                            YearPub = r["YearPub"] as int?,
                            ISBN = r["ISBN"].ToString(),
                            CreatedAt = (DateTime)r["CreatedAt"]
                        });
                    }
                }
            }
            return list;
        }

        public static bool AddBook(Book b)
        {
            const string sql = @"
                INSERT INTO Books (Title, Author, YearPub, ISBN, CreatedAt)
                VALUES (@title, @author, @year, @isbn, GETDATE())";

            using (var cx = new SqlConnection(CS))
            using (var cm = new SqlCommand(sql, cx))
            {
                cm.Parameters.AddWithValue("@title", b.Title);
                cm.Parameters.AddWithValue("@author", b.Author);
                cm.Parameters.AddWithValue("@year", (object)b.YearPub ?? DBNull.Value);
                cm.Parameters.AddWithValue("@isbn", b.ISBN);
                cx.Open();
                return cm.ExecuteNonQuery() > 0;
            }
        }

        public static bool Update(Book b)
        {
            const string sql = @"
                UPDATE Books
                   SET Title   = @title,
                       Author  = @author,
                       YearPub = @year,
                       ISBN    = @isbn
                 WHERE BookId = @id";

            using (var cx = new SqlConnection(CS))
            using (var cm = new SqlCommand(sql, cx))
            {
                cm.Parameters.AddWithValue("@title", b.Title);
                cm.Parameters.AddWithValue("@author", b.Author);
                cm.Parameters.AddWithValue("@year", (object)b.YearPub ?? DBNull.Value);
                cm.Parameters.AddWithValue("@isbn", b.ISBN);
                cm.Parameters.AddWithValue("@id", b.BookId);
                cx.Open();
                return cm.ExecuteNonQuery() > 0;
            }
        }

        public static bool DeleteBook(int bookId)
        {
            const string sql = @"
                DELETE FROM Books
                 WHERE BookId = @id";

            using (var cx = new SqlConnection(CS))
            using (var cm = new SqlCommand(sql, cx))
            {
                cm.Parameters.AddWithValue("@id", bookId);
                cx.Open();
                return cm.ExecuteNonQuery() > 0;
            }
        }
    }
}
