using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Module_25.Repositories
{
    public class UserRepository
    {
        public AppContext DbContext { get; private set; }
        public UserRepository() { 
        
            DbContext = new AppContext();

        }
        public UserRepository(AppContext appContext) { 
        
            DbContext = appContext;
        
        }

        public int AddUser(string userName, string email)
        {

            DbContext.Users.Add(new User {Name = userName, Email = email});
            return DbContext.SaveChanges();
        }

        public User GetUser(int id)
        {
            return DbContext.Users.SingleOrDefault(x => x.Id == id);
        }


        public List<User> GetAllUsers() {
        
            return DbContext.Users.ToList();
        }


        public int ChangeUserName(int id, string userNewName)
        {
            User updatedUser = GetUser(id);
            updatedUser.Name = userNewName;
            return DbContext.SaveChanges();
        }


        public int SubscribeBookToUser(int userId, int bookId)
        {
            User updatedUser = GetUser(userId);
            Book updatedBook = DbContext.Books.SingleOrDefault(b => b.Id == bookId);
            updatedUser.BooksBorrowed.Add(updatedBook);
            return DbContext.SaveChanges();
        }


        public int UnsubscribeBookFromUser(int userId, int bookId)
        {
            User updatedUser = GetUser(userId);
            Book updatedBook = DbContext.Books.SingleOrDefault(b => b.Id == bookId);
            updatedUser.BooksBorrowed.Remove(updatedBook);
            return DbContext.SaveChanges();
        }

        public int DeleteUser(int userId)
        {

            int result;

            User userToDelete = GetUser(userId);

            if (userToDelete.BooksBorrowed is not null) { 
            

                foreach(Book book in userToDelete.BooksBorrowed)
                {
                    book.UserId = null;
                }

                userToDelete.BooksBorrowed.Clear();
            }

            DbContext.Users.Remove(userToDelete);
            result = DbContext.SaveChanges();

            return result;

        }

        // ДЗ 6
       public int UserBorrowedBooksCount(int userId)
        {
            return GetUser(userId).BooksBorrowed.Count();
        }
    }
}
