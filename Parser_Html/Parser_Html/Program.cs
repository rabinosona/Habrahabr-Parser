using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser_Html
{
    class Program
    {
        static void Main(string[] args)
        {
            Connect newb = new Connect();
            newb.Download_Write(10);
            newb.Check();
            newb.Send("rabinosona@gmail.com", "idonatos13@gmail.com");
        }
    }
}
