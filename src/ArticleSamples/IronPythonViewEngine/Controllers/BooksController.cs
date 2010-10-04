using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IronPythonViewEngine.Models;

namespace IronPythonViewEngine.Controllers {
    public class BooksController : Controller  {

        [HttpGet]
        public ActionResult Index() {

            List<Book> books = new List<Book>() {
                new Book() { Title = "The Omnivore's Dilemma", ISBN = "0143038583", Author = new Author() { FirstName = "Michael", LastName =  "Pollen" }  },
                new Book() { Title = "The Godfather", ISBN = "0451205766", Author = new Author() { FirstName = "Mario", LastName = "Skinner" }  },
                new Book() { Title = "Walden Two", ISBN = "0872207781", Author = new Author() { FirstName = "B.F.", LastName = "Skinner" }  },
                new Book() { Title = "The Doors of Perception", ISBN = "0061729078", Author = new Author() { FirstName = "Alduos", LastName = "Huxley" }  }
            };

            if (TempData["Book"] != null) {
                books.Add(TempData["Book"] as Book);
            }
            return View(books);
        }

        [HttpPost]
        public ActionResult Index(string title, string author, string isbn) {

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author) || string.IsNullOrEmpty(isbn)) {
                TempData["Title"] = title;
                TempData["Author"] = author;
                TempData["ISBN"] = isbn;
                TempData["ErrorMessage"] = "Title, author and ISBN are all required.";
            } else {
                string[] authorInfo = author.Split(" ".ToCharArray());
                TempData["Book"] = new Book() { Author = new Author() { FirstName = authorInfo[0], LastName = authorInfo[1] }, ISBN = isbn, Title = title };
            }
            return RedirectToAction("Index");
        }

    }
}