using System;
using FirstApp.Models;
using FirstApp.Repositories;

using (var db = new FirstApp.AppContext())
{
    UserRepository userRepository = new(db);
    BookRepository bookRepository = new(db);

    var user1 = new UserLibrary { UserName = "User1", UserEmail = "email1@google.com" };
    var user2 = new UserLibrary { UserName = "User2", UserEmail = "email2@google.com" };
    var user3 = new UserLibrary { UserName = "User3", UserEmail = "email3@google.com" };
    var user4 = new UserLibrary { UserName = "User4", UserEmail = "email4@google.com" };
    var user5 = new UserLibrary { UserName = "User5", UserEmail = "email5@google.com" };

    var book1 = new BookLibrary { BookName = "Book1", BookYear = 1986,AuthorName="Author11",Genre="Genre11" };
    var book2 = new BookLibrary { BookName = "Book2", BookYear = 2001,AuthorName="Author22",Genre="Genre22"  };
    var book3 = new BookLibrary { BookName = "Book3", BookYear = 2024,AuthorName="Author22",Genre="Genre22"  };
    var book4 = new BookLibrary { BookName = "Book4", BookYear = 2016,AuthorName="Author22",Genre="Genre22"  };
    var book5 = new BookLibrary { BookName = "Book5", BookYear = 1999,AuthorName="Author11",Genre="Genre11"  };


    db.Database.EnsureDeleted(); // Удаляем БД если существует
    db.Database.EnsureCreated(); // Создаём БД

    // добавление под одному юзеру
    db.Users.Add(user1);
    db.Users.Add(user2);
    db.Users.Add(user3);
    db.Users.Add(user4);
    db.Users.Add(user4);
    db.Users.Add(user5);

    db.Books.AddRange(book1, book2, book3, book4, book5);// добавление  несколько сразу

    db.SaveChanges(); // запись в БД

    // Поиск по id
    var foundUser = userRepository.GetUserById(3);
    Console.WriteLine($"1) Found user={foundUser.UserName}");

    //  Вывод всех
    var allUsers = userRepository.GetAllBooks();
    foreach (UserLibrary user in allUsers)
    {
        Console.WriteLine($"2) Found user={user.UserName}");
    }

    // Добавление одного
    var user6 = new UserLibrary { UserName = "User6", UserEmail = "email6@google.com" };
    userRepository.AddUser(user6);
    Console.WriteLine($"3) Added user={user6.UserName}");
    
    // Удаление одного
    userRepository.DeleteUser(user3);
    Console.WriteLine($"4) Deleted user={user3.UserName}");

    // Сменить имя юзера
    userRepository.ChangeUserNameById(1,"User111");
    Console.WriteLine($"5) Changed user={user1.UserName}");    
    
    // Сменить год выпуска книги
    bookRepository.ChangeYearById(1,1987);
    Console.WriteLine($"6) Changed book name={book1.BookName} and new year ={book1.BookYear}");  
    
    //  Выдаём книги на руки
    book1.UserLibrary = user1;
    user2.Books.Add(book2);
    user2.Books.Add(book3);
    book4.UserId = user4.Id;

    //  Поиск книги по жанру и диапазону годов

    foreach (var booksFound in bookRepository.GetBookByGenreAndYears("Genre22",2000,2017))
    {
         Console.WriteLine($"7) Found book name={booksFound.BookName} and  year ={booksFound.BookYear}");  
    }
    //  Количество книг по автору
    Console.WriteLine("8) Found book by author=Author22 count={0}", bookRepository.GetBookCountByAuthor("Author22"));  

   //  Количество книг по жанру
    Console.WriteLine("9) Found book by genre=Genre11 count={0}", bookRepository.GetBookCountByGenre("Genre11"));  

    // Tсть ли книга автора с жанром
    Console.WriteLine("10) Is exist book with genre=Genre11 and author=Author11? Answer is {0} ", bookRepository.ExistBookByGenreAndAuthor("Genre11","Author11"));  

    // Есть ли книга у пользователя на руках
    Console.WriteLine("11) Does user2 have book2? Answer is {0} ", bookRepository.UserHasBook(user2,book2));  

    // Количество книг у пользователя на руках
    Console.WriteLine("12) user2 has {0} count", userRepository.GetBookCount(user2));  

    // Последняя вышедшая книга
    Console.WriteLine("13) last book is {0} in {1}", bookRepository.LastBook().BookName,bookRepository.LastBook().BookYear);  

    // Весь список книг по алфавиту
    foreach (var booksFound in bookRepository.GetAllBooksSortByName())
    {
         Console.WriteLine($"14) Sorted by name ={booksFound.BookName}");  
    }

     //  список книг по убыванию года выпуска
    foreach (var booksFound in bookRepository.GetAllBooksSortByYear())
    {
         Console.WriteLine($"15) Sorted by year ={booksFound.BookName}, {booksFound.BookYear}");  
    }   
    db.SaveChanges(); 

    return;


}
