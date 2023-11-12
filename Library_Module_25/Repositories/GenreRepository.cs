using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Module_25.Repositories
{
     class GenreRepository
    {
        public AppContext DbContext { get; private set; }
        public GenreRepository()
        {

            DbContext = new AppContext();

        }
        public GenreRepository(AppContext appContext)
        {

            DbContext = appContext;

        }

        public int AddGenre(string genreName)
        {
            DbContext.Genres.Add(new Genre { Name = genreName });
            return DbContext.SaveChanges();
        }
        public Genre GetGenreById(int id)
        {
            return DbContext.Genres.SingleOrDefault(c => c.Id == id);
        }

        public List<Genre> GetAllGenres()
        {
            return DbContext.Genres.ToList();
        }

        public int UpdateGenreName(int genreId, string genreNewName)
        {
            GetGenreById(genreId).Name = genreNewName;
            return DbContext.SaveChanges();
        }

        public int DeleteGenre(int id)
        {
            int result;
           
                Genre genreToDelete = GetGenreById(id);
                foreach (Book book in genreToDelete.BooksInGenre)
                {
                    book.GenreId = null;
                    book.Genre = null;
                }
                genreToDelete.BooksInGenre.Clear();
                DbContext.Genres.Remove(genreToDelete);
                result = DbContext.SaveChanges();
                
            
            return result;
        }
    }
}
