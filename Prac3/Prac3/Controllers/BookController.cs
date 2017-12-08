using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prac3.Controllers
{
    public class BookController : Controller
    {
        String path = @"C:/Programs/SOA/Prac3/books.txt";
        ServiceReference1.BookServiceClient clint = new ServiceReference1.BookServiceClient();
        IEnumerable<ServiceReference1.Book> bookList = new List<ServiceReference1.Book>();

        // GET: Book
        public ActionResult Index()
        {
            bookList = clint.ReadBook(path);
            return View(bookList);
        }

        // GET: Book/Search
        public ActionResult Search()
        {
            bookList = clint.ReadBook(path);
            return View(bookList);
        }

        [HttpPost]
        public ActionResult Search(String Index, String Value)
        {
            bookList = clint.ReadBook(path);
            List<ServiceReference1.Book> foundBook = new List<ServiceReference1.Book>();
            foreach (var b in bookList)
            {
                if (Index.Equals("Id"))
                {
                    if (b.Id.ToString().Equals(Value))
                    {
                        foundBook.Add(b);
                    }
                }
                else if (Index.Equals("Name"))
                {
                    if (b.Name.Equals(Value))
                    {
                        foundBook.Add(b);
                    }
                }
                else if (Index.Equals("Author"))
                {
                    if (b.Author.Equals(Value))
                    {
                        foundBook.Add(b);
                    }
                }
                else if (Index.Equals("Year"))
                {
                    if (b.Year.ToString().Equals(Value))
                    {
                        foundBook.Add(b);
                    }
                }
            }
            return View(foundBook);
        }

        // POST: Book/Delete/
        public ActionResult Delete()
        {
            bookList = clint.ReadBook(path);
            return View(bookList);
        }

        // POST: Book/Delete/
        [HttpPost]
        public ActionResult Delete(String Index, String Value)
        {
            bookList = clint.ReadBook(path);
            List<ServiceReference1.Book> foundBook = bookList.ToList<ServiceReference1.Book>();
            foreach (var b in bookList)
            {
                if (Index.Equals("Id"))
                {
                    if (b.Id.ToString().Equals(Value))
                    {
                        foundBook.Remove(b);                        
                    }
                }
                else if (Index.Equals("Name"))
                {
                    if (b.Name.Equals(Value))
                    {
                        foundBook.Remove(b);
                    }
                }
                else if (Index.Equals("Author"))
                {
                    if (b.Author.Equals(Value))
                    {
                        foundBook.Remove(b);
                    }
                }
                else if (Index.Equals("Year"))
                {
                    if (b.Year.ToString().Equals(Value))
                    {
                        foundBook.Remove(b);
                    }
                }
            }
            clint.WriteBook(foundBook.ToArray<ServiceReference1.Book>(), path);
            return RedirectToAction("Delete");
        }

        // POST: Book/Create/
        public ActionResult Create()
        {
            bookList = clint.ReadBook(path);
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult CreateBook()
        {
            bookList = clint.ReadBook(path);
            List<ServiceReference1.Book> books = bookList.ToList<ServiceReference1.Book>();
            ServiceReference1.Book book = new ServiceReference1.Book();
        
            book.Id = Convert.ToInt32(System.Web.HttpContext.Current.Request.Params[("Id")]);
            book.Name = System.Web.HttpContext.Current.Request.Params[("Name")];
            book.Author = System.Web.HttpContext.Current.Request.Params[("Author")];
            book.Year = Convert.ToInt32(System.Web.HttpContext.Current.Request.Params[("Year")]);
            book.Price = Convert.ToDecimal(System.Web.HttpContext.Current.Request.Params[("Price")]);
            book.Stock = Convert.ToInt32(System.Web.HttpContext.Current.Request.Params[("Stock")]);            

            books.Add(book);
            clint.WriteBook(books.ToArray<ServiceReference1.Book>(), path);

            return RedirectToAction("Create");
        }        
    }    
}