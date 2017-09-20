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
        ExecuteNonQuery(File.ReadAllText(@".\cfg\dbconfig.txt"), dbConnection);
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

    public static void DumpToText(SQLiteConnection dbConnection)
    {
        string query = "select * from 'fifa';";
        SQLiteDataReader reader;
        string strDelimiter = ",";
        try
        {
            using (reader = new SQLiteCommand(query, dbConnection).ExecuteReader())
            {
                if (reader.HasRows)
                {
                    StringBuilder sb = new StringBuilder();
                    Object[] items = new Object[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        sb.Append(reader.GetName(i));
                        sb.Append(strDelimiter);
                    }
                    sb.Length--;
                    sb.Append("\n");
                    while (reader.Read())
                    {
                        reader.GetValues(items);
                        foreach (var item in items)
                        {
                            sb.Append(item.ToString());
                            sb.Append(strDelimiter);
                        }
                        sb.Length--;
                        sb.Append("\n");
                    }
                    File.WriteAllText(@".\stats.txt", sb.ToString());
                    Console.WriteLine("Database 'fifa' dumped to text file.");
                    Console.WriteLine("File located at " + Directory.GetCurrentDirectory() + @"\stats.txt");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Console.ReadLine();
        };
        dbConnection.Close();
    }

    public static void ClearDB(SQLiteConnection dbConnection)
    {
        int sure = 1;
        ExecuteNonQuery(@"DROP TABLE 'fifa';", dbConnection);
        Console.WriteLine("Database cleared.");
        while (sure == 0)
        {
            Console.WriteLine("Do you wish to clear the database? (Y/N)");
            string answer = Console.ReadLine();
            if (answer == "Y" || answer == "y")
            {
                sure = 1;
                ExecuteNonQuery(@"DROP TABLE 'fifa';", dbConnection);
                Console.WriteLine("Database cleared.");
            }
            else if (answer == "N" || answer == "n")
            {
                Console.WriteLine("Database clear canceled.");
                break;
            }
            else
            {
                Console.WriteLine("Invalid answer. Enter Y or N.");
            }
        }
        dbConnection.Close();
    }
}
