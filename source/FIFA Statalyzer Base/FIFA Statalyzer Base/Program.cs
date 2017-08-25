﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using tessnet2;
using Tesseract;

namespace FIFA_Statalyzer_Base
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteConnection dbConnection;
            if (File.Exists("FifaStatalyzer.sqlite") == false)
            {
                SQLiteConnection.CreateFile("FifaStatalyzer.sqlite");
            }
            dbConnection = new SQLiteConnection("Data Source =FifaStatalyzer.sqlite;Version=3;");
            dbConnection.Open();
            string sql = "CREATE TABLE if not exists 'fifa' ('pscore' INT, 'ppos' INT, 'opscore' INT, 'oppos' INT, 'result' INT);";
            SQLiteCommand sqlcommand = new SQLiteCommand(sql, dbConnection);
            sqlcommand.ExecuteNonQuery();
            // 0 = loss, 1 = draw, 2 = win
            int result = 0;
            Console.WriteLine("What was your score?");
            int.TryParse(Console.ReadLine(), out int pscore);
            Console.WriteLine("What was the opponent's score?");
            int.TryParse(Console.ReadLine(), out int opscore);
            Console.WriteLine("What was your ball possession?");
            int.TryParse(Console.ReadLine(), out int ppos);
            //  Console.WriteLine("What was your opponent's ball possession?");
            //  int.TryParse(Console.ReadLine(), out int oppos);
            int oppos = (100 - ppos);
            if (opscore > pscore) { result = 0; }
            if (opscore == pscore) { result = 1; }
            if (opscore < pscore) { result = 2; }
            sql = "insert into fifa (pscore, opscore, ppos, oppos, result) values ("+ pscore + ", " + opscore + ", " + ppos + ", " + oppos + ", " + result + ");";
            sqlcommand = new SQLiteCommand(sql, dbConnection);
            sqlcommand.ExecuteNonQuery();
        }
    }
}