using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Prac3
{
    public partial class Book1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ServiceReference1.BookServiceClient client = new ServiceReference1.BookServiceClient();
            IEnumerable<ServiceReference1.Book> books = client.ReadBook(@"C:/Programs/SOA/Prac3/books.txt");

            Dictionary<int, int> items = new Dictionary<int, int>();
            items.Add(470376848, 3);


            //bpi.SetDic(470376848, 3);


        }
    }
}