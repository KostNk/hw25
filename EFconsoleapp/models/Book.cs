using System;

namespace FirstApp.Models;

public class BookLibrary
{
    public int Id { get; set; }
    public string BookName { get; set; }
    public int BookYear { get; set; }
    public string AuthorName { get; set; }
    public string Genre { get; set; }
    // Внешний ключ
    public int? UserId { get; set; }
    // Навигационное свойство
    public UserLibrary? UserLibrary { get; set; }

}

