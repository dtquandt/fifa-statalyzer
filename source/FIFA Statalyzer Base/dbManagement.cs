using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;

public class DBManagement
{

    public static SQLiteConnection InitializeDB()
    {
        SQLiteConnection dbConnection;
        if (!File.Exists("FifaStatalyzer.sqlite"))
        {
            SQLiteConnection.CreateFile("FifaStatalyzer.sqlite");
        }
        dbConnection = new SQLiteConnection("Data Source =FifaStatalyzer.sqlite;Version=3;");
        dbConnection.Open();
        ExecuteNonQuery(File.ReadAllText(Directory.GetCurrentDirectory()+@"\cfg\dbconfig.txt"), dbConnection);
        return dbConnection;
    }

    public static void ExecuteNonQuery(string command, SQLiteConnection dbConnection)
    {
        SQLiteCommand sqlcommand = new SQLiteCommand(command, dbConnection);
        sqlcommand.ExecuteNonQuery();
    }

    public static SQLiteDataReader ExecuteReader(string command, SQLiteConnection dbConnection)
    {
        SQLiteCommand sqlcommand = new SQLiteCommand(command, dbConnection);
        return sqlcommand.ExecuteReader();
    }
    
}
