using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewDescription
{
    public class View
    {
        public void ShowInfo(string text)
        {
            Console.WriteLine($"{text}");
        }

        public string GetInfo()
        {
            return Console.ReadLine();
        }
    }
}
