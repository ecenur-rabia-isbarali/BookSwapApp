using Microsoft.AspNetCore.Mvc;
using BookSwapApp.Models;
using System.Linq;
namespace BookSwapApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

public BooksController(AppDbContext context)
{
    _context = context;
}
        public IActionResult Index(string searchText)
{
    var books = _context.Books.AsQueryable();

    if (!string.IsNullOrEmpty(searchText))
    {
        books = books.Where(b => b.Name.Contains(searchText) || b.Author.Contains(searchText));
    }

    return View(books.ToList());
}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
public IActionResult Create(string Name, string Author)
{
    var book = new Book
{
    Name = Name,
    Author = Author
};

_context.Books.Add(book);
_context.SaveChanges();
    return RedirectToAction("Index");
}
public IActionResult Delete(int id)
{
    var book = _context.Books.Find(id);

    if (book != null)
    {
        _context.Books.Remove(book);
        _context.SaveChanges();
    }

    return RedirectToAction("Index");
}
    }
    
}
