using Microsoft.AspNetCore.Mvc;
using LibraryBookApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibraryBookApp.Controllers
{
    public class BookController : Controller
    {
        private static List<Book> books = new List<Book>();
        private static int nextId = 1;

        public IActionResult Index()
        {
            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = nextId++;
                books.Add(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book != null)
                books.Remove(book);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Search(string query)
        {
            var result = books
                .Where(b => b.Title.Contains(query ?? "", System.StringComparison.OrdinalIgnoreCase)
                         || b.Author.Contains(query ?? "", System.StringComparison.OrdinalIgnoreCase))
                .ToList();

            ViewBag.Query = query;
            return View(result);
        }
    }
}