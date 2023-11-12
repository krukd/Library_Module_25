using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Module_25.Repositories
{
    public class BookRepository
    {

        public AppContext DbContext { get; private set; }
        public BookRepository()
        {

            DbContext = new AppContext();

        }
        public BookRepository(AppContext appContext)
        {

            DbContext = appContext;

        }


        public int AddBook(string bookTitle, int publishYear)
        {

            DbContext.Books.Add(new Book { Title = bookTitle, Publish_Year = publishYear });
            return DbContext.SaveChanges();
        }

        public Book GetBook(int id)
        {
            return DbContext.Books.SingleOrDefault(b => b.Id == id);
        }


        public List<Book> GetAllBooks()
        {

            return DbContext.Books.ToList();
        }


        public int UpdatePublishYearOfBook(int bookId, int publishYearNew)
        {
            Book book = GetBook(bookId);
            book.Publish_Year = publishYearNew;
            return DbContext.SaveChanges();
        }

        public int DeleteBook(int bookId)
        {
            int result;

            Book bookToDelete = GetBook(bookId);

            if (bookToDelete.UserId is not null)
            {
                DbContext.Users.SingleOrDefault(u => u.Id == bookToDelete.Id).
                        BooksBorrowed.Remove(bookToDelete);
            }

            foreach (Author author in bookToDelete.Authors)
            {
                author.Books.Remove(bookToDelete);
            }

            DbContext.Books.Remove(bookToDelete);
            result = DbContext.SaveChanges();
            return result;
        }

        //ДЗ 1
        public List<Book> GetBooksByGenreInYearInterval(int? genreId = null, int? startYear = null, int? endYear = null)
        {
            var query = from book in DbContext.Books
                        where ((genreId == null) || (book.GenreId == genreId))
                        && ((startYear == null) || (book.Publish_Year >= startYear))
                        && ((endYear == null) || (book.Publish_Year <= endYear))
                        select book;
            return query.ToList();
        }

        //ДЗ 3

        public int GetBooksCountByGenre(int genreId)
        {
            var query = from book in DbContext.Books
                        where book.GenreId == genreId
                        select book;
            return query.Count();
        }

        //ДЗ 4

        public bool CheckForBookByAuthorsLastnameAndTitle(string? authorsLastname = null, string? bookTitle = null)
        {
            var query = from book in DbContext.Books
                        where ((authorsLastname == null) || (book.Authors.Any(a => a.LastName == authorsLastname)))
                        && ((bookTitle == null) || (book.Title == bookTitle))
                        select book;
            return query.Any();
        }

        //ДЗ 5

        public bool IsBookBorrowed(int bookId)
        {
            return GetBook(bookId).BorrowedBy is not null;
        }

        //ДЗ 7

        public Book GetNewestBook()
        {
            var query = from Book book in DbContext.Books
                        orderby book.Publish_Year descending, book.Id descending
                        select book;
            return query.First();
        }


        //ДЗ 8

        public List<Book> GetAllBooksOrderedByTitle()
        {
            var query = from Book book in DbContext.Books
                        orderby book.Title
                        select book;
            return query.ToList();
        }

        //ДЗ 9

        public List<Book> GetAllBooksOrderedByYearFromNewest()
        {
            var query = from Book book in DbContext.Books
                        orderby book.Publish_Year descending
                        select book;
            return query.ToList();
        }

    }
}
