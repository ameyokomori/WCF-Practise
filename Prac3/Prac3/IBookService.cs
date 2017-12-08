using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Prac3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBookService" in both code and config file together.

    [DataContract]
    public class Book
    {
        int num;
        int id;
        string name;
        string author;
        int year;
        Decimal price;
        int stock;

        public Book()
        {
            this.num = 0;
            this.id = 0;
            this.name = "";
            this.author = "";
            this.year = 0;
            this.price = 0;
            this.stock = 0;
        }

        [DataMember]
        public int Num
        {
            get { return num; }
            set { this.num = value; }
        }

        [DataMember]
        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }

        [DataMember]
        public string Author
        {
            get { return author; }
            set { this.author = value; }
        }

        [DataMember]
        public int Year
        {
            get { return year; }
            set { this.year = value; }
        }

        [DataMember]
        public Decimal Price
        {
            get { return price; }
            set { this.price = value; }
        }

        [DataMember]
        public int Stock
        {
            get { return stock; }
            set { this.stock = value; }
        }
    }       

    [ServiceContract]
    public interface IBookService
    {
        [OperationContract]
        IEnumerable<Book> ReadBook(string path);   // IEnumerable is like a list

        [OperationContract]
        void WriteBook(IEnumerable<Book> books, string path);
    }
}
