using FirstApp.Models;
using FirstApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FirstApp.Repositories
{
    public class UserRepository
    {
        private readonly AppContext _context;

        public UserRepository(AppContext context)
        {
            _context = context;
        }
        //выбор объекта из БД по его идентификатору
        public UserLibrary GetUserById(int id)
        {

                UserLibrary? user= _context.Users.FirstOrDefault(userId => userId.Id == id);
                return user;
        }

        //выбор всех объектов
        public IEnumerable<UserLibrary> GetAllBooks()
        {
            return  _context.Users;
        }

        //добавление
        public void AddUser(UserLibrary user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        //Удаление
        public void DeleteUser(UserLibrary user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        //Обновление имени по  идентификатору
        public void  ChangeUserNameById(int id, string newName)
        {
                UserLibrary? user= _context.Users.FirstOrDefault(userId => userId.Id == id);
                user.UserName=newName;
                _context.SaveChanges();
        }

        //Количество книг на руках у пользователя
        public int  GetBookCount(UserLibrary user)
        {
            return user.Books.Count();
        }
    }
}