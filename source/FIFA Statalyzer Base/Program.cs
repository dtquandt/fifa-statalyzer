using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using Tesseract;

namespace FIFA_Statalyzer_Base
{

    class Program
    {

        static void Main(string[] args)
        {
            var dbConnection = DBManagement.InitializeDB();
            Console.WriteLine(OCR.ReadImage(@"C:\test2.png"));
            Console.ReadLine();
        }
        
    }
}
