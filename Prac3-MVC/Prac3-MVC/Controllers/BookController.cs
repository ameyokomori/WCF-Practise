using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prac3_MVC.Controllers
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

        // POST: Book/DeleteBooks/
        public ActionResult DeleteBooks()
        {
            bookList = clint.ReadBook(path);
            return View(bookList);
        }

        // POST: Book/DeleteBooks/
        [HttpPost]
        public ActionResult DeleteBooks(String Index, String Value)
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
            return RedirectToAction("DeleteBooks");
        }

        // POST: Book/Create/
        public ActionResult Create()
        {
            bookList = clint.ReadBook(path);
            return View(bookList);
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult CreateBook()
        {
            bookList = clint.ReadBook(path);
            List<ServiceReference1.Book> books = bookList.ToList<ServiceReference1.Book>();
            ServiceReference1.Book book = new ServiceReference1.Book();

            try
            {
                book.Id = Convert.ToInt32(System.Web.HttpContext.Current.Request.Params[("Id")]);
                book.Name = System.Web.HttpContext.Current.Request.Params[("Name")];
                book.Author = System.Web.HttpContext.Current.Request.Params[("Author")];
                book.Year = Convert.ToInt32(System.Web.HttpContext.Current.Request.Params[("Year")]);
                book.Price = Convert.ToDecimal(System.Web.HttpContext.Current.Request.Params[("Price")]);
                book.Stock = Convert.ToInt32(System.Web.HttpContext.Current.Request.Params[("Stock")]);
            }
            catch (Exception e)
            {
                ViewBag.Error = "Incorrect value." + e.ToString();
                return RedirectToAction("Create");
            }
            

            books.Add(book);
            clint.WriteBook(books.ToArray<ServiceReference1.Book>(), path);

            return RedirectToAction("Create");
        }

        // POST: Book/Purchase/
        // [HttpPost]
        public ActionResult Purchase()
        {
            bookList = clint.ReadBook(path);
            List<ServiceReference1.Book> books = bookList.ToList<ServiceReference1.Book>();
            decimal balance = Convert.ToDecimal(System.Web.HttpContext.Current.Request.Params["Budget"]);

            ServiceReference1.BookPurchaseResponse response = new ServiceReference1.BookPurchaseResponse();

            Dictionary<int, int> item = new Dictionary<int, int>();            

            int inputCount = Convert.ToInt32(System.Web.HttpContext.Current.Request.Params["InputCount"]);

            for (int i = 1; i <= inputCount; i++)
            {
                int key = Convert.ToInt32(System.Web.HttpContext.Current.Request.Params[("Number" + i)]);
                int value = Convert.ToInt32(System.Web.HttpContext.Current.Request.Params[("Amount" + i)]);
                item.Add(key, value);
            }

            var result = clint.PurchaseBooks(balance, item, out response.result);

            if (result.Contains("left")) {
                string[] temp = result.Split(' ');
                temp = temp[0].Remove(0, 1).Split('.');
                if (temp[1].Length > 2)
                {
                    balance = Convert.ToDecimal(temp[0] + "." + temp[1].Remove(2, temp[1].Length - 2));
                }
                else
                {
                    balance = Convert.ToDecimal(temp[0] + "." + temp[1]);
                }               
            }

            ViewBag.Result = result;
            ViewBag.Balance = balance;
            bookList = clint.ReadBook(path);

            return View(bookList);
        }
    }    
}