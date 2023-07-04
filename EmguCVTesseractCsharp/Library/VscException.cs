using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguCVTesseractCsharp.Library
{
    public class VscException:Exception
    {
        public VscException(string message):base(message)
        {
            Console.WriteLine("Error: " + message);
        }

    }
}
