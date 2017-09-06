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
            //Sets default path if none is specified
            if (args.Length == 0)
            {
                args = new[] { Directory.GetCurrentDirectory() + @"\images\processed\" };
            }
            //Sets working path
            string folderPath = args[0];
            string[] fileList = Directory.GetFiles(folderPath, "*.png");
            foreach (string file in fileList)
            {
                Dictionary<string, int> statsDict = new Dictionary<string, int>();
                string[] legend = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\cfg\legend.txt");
                string ocrResult = (OCR.ReadImage(file));
                string ocrClean = OCR.CleanUp(ocrResult);
                string[] ocrValues = ocrClean.Split(' ');
                for (int i = 0; i < ocrValues.Length; i++)
                {
                    statsDict.Add(legend[i], (int.Parse(ocrValues[i])));
                    Console.WriteLine(legend[i] + " " + statsDict[legend[i]]);
                }
                SQLiteConnection dbconnection = DBManagement.InitializeDB();

            }
            Console.ReadLine();
        }
    }
}
