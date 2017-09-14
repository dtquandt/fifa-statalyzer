﻿using System;
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
            if (args == null || args.Length == 0)
            {
                args = new[] { @".\images\processed\" };
            }
            //Sets working path
            string folderPath = args[0]; 
            string[] fileList = Directory.GetFiles(folderPath, "*.png");
            SQLiteConnection dbConnection = DBManagement.InitializeDB();
            foreach (string file in fileList)
            {
                Dictionary<string, int> statsDict = new Dictionary<string, int>();
                string[] legend = File.ReadAllLines(@".\cfg\legend.txt");
                string ocrResult = (OCR.ReadImage(file));
                string ocrClean = OCR.CleanUp(ocrResult);
                string[] ocrValues = ocrClean.Split(' ');
                for (int i = 0; i < ocrValues.Length; i++)
                {
                    statsDict.Add(legend[i], (int.Parse(ocrValues[i])));
                }
                int result = 0;
                statsDict.TryGetValue("hGoals", out int hGoals);
                statsDict.TryGetValue("aGoals", out int aGoals);
                if (hGoals == aGoals) { result = 0; } // Draw
                else if (hGoals > aGoals) { result = 1; } // Home win
                else if (hGoals < aGoals) { result = 2; } // Away win
                statsDict.Add("result", result);
                foreach (var pair in statsDict)
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }
                StringBuilder commandBuilder = new StringBuilder("insert into fifa(");
                foreach (var pair in statsDict)
                {
                    commandBuilder.Append(pair.Key + ", ");
                }
                commandBuilder.Length -= 2;
                commandBuilder.Append(") values (");
                foreach (var pair in statsDict)
                {
                    commandBuilder.Append(pair.Value + ", ");
                }
                commandBuilder.Length -= 2;
                commandBuilder.Append(");");
                string sqlCommand = commandBuilder.ToString();
                DBManagement.ExecuteNonQuery(sqlCommand, dbConnection);
            }
            Console.ReadLine();
        }
    }
}
