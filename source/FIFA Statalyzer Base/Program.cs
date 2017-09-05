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
#if DEBUG
            args = new[] { @"C:\test" };
#endif
            string folderPath = args[0];
            string[] fileList = Directory.GetFiles(folderPath, "*.png");
            var dbConnection = DBManagement.InitializeDB();
            foreach(string file in fileList)
            {
                string ocr = (OCR.ReadImage(file));
                Console.ReadLine();
            }
        }
    }
}
