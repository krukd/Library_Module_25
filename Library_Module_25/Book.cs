using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Module_25
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Publish_Year { get; set; }

        public List<Author> Authors { get; set; }

        public Genre Genre { get; set; }

        public int? GenreId { get; set; }


        public int? UserId { get; set; }

        public User? BorrowedBy { get; set; }
    }
}
