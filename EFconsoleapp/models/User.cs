using System;

namespace FirstApp.Models;

public class UserLibrary
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }

    // Навигационное свойство
    public List<BookLibrary> Books { get; set; } = new List<BookLibrary>();
}

