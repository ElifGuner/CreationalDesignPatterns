// See https://aka.ms/new-console-template for more information

//key-value şeklinde sınırlı sayıda nesne oluşturulur.

using System.Runtime.CompilerServices;

//var msSql = Database.GetInstance("MSSQL");
//var oracle = Database.GetInstance("Oracle");
//var mongoDB = Database.GetInstance("MongoDB");
// ya da 
var msSql = Database.GetInstance("MSSQL", "...");
var oracle = Database.GetInstance("Oracle", "...");
var mongoDB = Database.GetInstance("MongoDB", "...");

var msSql2 = Database.GetInstance("MSSQL");
var oracle2 = Database.GetInstance("Oracle");
var mongoDB2 = Database.GetInstance("MongoDB");
public class Database
{ 
    private Database()
    {
        Console.WriteLine($"{nameof(Database)} nesnesi üretildi");
    }

    static Dictionary<string, Database> _databases;

    //burda property olmaz, metod olmalı. Çünkü key parametresi alacak.
    
    public static Database GetInstance(string key)
    { 
        if (!_databases.ContainsKey(key))
            _databases[key] = new Database();
        return _databases[key];
    }

    string connectionString;
    public static Database GetInstance(string key, string connectionString)
    {
        Database database = GetInstance(key);
        database.ConnectionString(connectionString);
        return database;
    }

    public void Connect()
    {
        Console.WriteLine("Bağlantı sağlandı");
    }

    public void Disconnect()
    {
        Console.WriteLine("Bağlantı koparıldı");
    }
    public void ConnectionString(string connectionString)
    { 
    
    }
}
