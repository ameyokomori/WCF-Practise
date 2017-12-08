using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;

namespace Prac3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BookService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BookService.svc or BookService.svc.cs at the Solution Explorer and start debugging.
    public class BookService : IBookService
    {
        public IEnumerable<Book> ReadBook(string path)
        {
            List<Book> bookList = new List<Book>();
            Book book;
            using (StreamReader sr = new StreamReader(path))
            {
                int num = 0;
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    num++;
                    book = new Book();
                    String[] temp = line.Split(',');
                    book.Num = num;
                    book.Id = Convert.ToInt32(temp[0]);
                    book.Name = temp[1];
                    book.Author = temp[2];
                    book.Year = Convert.ToInt32(temp[3]);
                    book.Price = Convert.ToDecimal(temp[4].Remove(0, 1));
                    book.Stock = Convert.ToInt32(temp[5]);

                    bookList.Add(book);
                }
                sr.Close();
            }
            return bookList;
        }

        public void WriteBook(IEnumerable<Book> books, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (Book b in books)
                {
                    sw.WriteLine(b.Id + "," + b.Name + "," + b.Author + "," + b.Year + ",$" + b.Price + "," + b.Stock);
                }
                sw.Close();
            }
        }

    }
}
