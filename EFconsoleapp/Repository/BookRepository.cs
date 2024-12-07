using FirstApp.Models;
using FirstApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FirstApp.Repositories 
{
    public class BookRepository
    {
        private readonly AppContext _context;

        public BookRepository(AppContext context)
        {
            _context = context;
        }
 

        //Обновление года выпуска книги по  идентификатору
        public void  ChangeYearById(int id, int newYear)
        {
            BookLibrary? book= _context.Books.FirstOrDefault(bookId => bookId.Id == id);
            book.BookYear=newYear;
            _context.SaveChanges();
        }

        // поиск книги по жанру и диапазону годов
        public IEnumerable<BookLibrary> GetBookByGenreAndYears(string genre, int yearFrom, int yearTo)
        {
            return _context.Books.Where(g => g.Genre == genre && g.BookYear>=yearFrom && g.BookYear<=yearTo);
        }
        
        //Получать количество книг определенного автора в библиотеке
        public int GetBookCountByAuthor(string authorName)
        {
            return _context.Books.Where(g => g.AuthorName == authorName).Count();
        }
        //Получать количество книг определенного жанра в библиотеке
        public int GetBookCountByGenre(string genre)
        {
            return _context.Books.Where(g => g.Genre == genre).Count();
        }

        //Есть ли книга автора с определённым жанром в библиотеке
        public bool ExistBookByGenreAndAuthor(string genre, string author)
        {
            return (_context.Books.Where(g => g.Genre == genre && g.AuthorName==author).Count()>0)?true:false ;
        }

        //Есть ли книга на руках у пользователя
        public bool UserHasBook(UserLibrary user, BookLibrary book)
        {
            return user.Books.Contains(book);
        }
        //Есть ли книга на руках у пользователя
        public BookLibrary LastBook()
        {
            return _context.Books.OrderByDescending(b=>b.BookYear).FirstOrDefault();
        }

        // Весь список по алфавиту
        public IEnumerable<BookLibrary> GetAllBooksSortByName()
        {
            return _context.Books.OrderBy(g => g.BookName);
        }

        // Весь список по убыванию года выпуска
        public IEnumerable<BookLibrary> GetAllBooksSortByYear()
        {
            return _context.Books.OrderByDescending(g => g.BookYear);
        }
    }
}