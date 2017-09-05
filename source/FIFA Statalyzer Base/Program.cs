using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            args = new[] { @"C:\test\active" };
#endif
            string folderPath = args[0];
            string[] fileList = Directory.GetFiles(folderPath, "*.png");
            var dbConnection = DBManagement.InitializeDB();
            foreach(string file in fileList)
            {
                string ocr = (OCR.ReadImage(file));
                ocr = ocr.Replace("\n", " ");
                ocr = Regex.Replace(ocr, "[^0-9 ]", "");
                ocr = Regex.Replace(ocr, @"\s+", " ");
                string[] values = ocr.Split(' ');
                Console.WriteLine(ocr);
                foreach (string value in values)
                {
                    Console.WriteLine(value);
                }
            }
            Console.ReadLine();
        }
    }
}
