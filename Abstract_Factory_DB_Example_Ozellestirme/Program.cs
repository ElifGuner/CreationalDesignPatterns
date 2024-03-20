// See https://aka.ms/new-console-template for more information

//Abstract Product
using System.Data;
using System.Data.Common;

DatabaseCreator creator = new DatabaseCreator();
Database mysql = creator.Create(new MySqlDatabaseFactory());
Database mssql = creator.Create(new MsSqlDatabaseFactory());
enum ConnectionState
{
    Open, Close
}

abstract class Connection
{
    public abstract bool Connect();
    public abstract bool Disconnect();
    public abstract ConnectionState State { get; set; }
}

abstract class Command
{
    public abstract void Execute(string query);
}

class MsSqlConnection : Connection
{
    public override ConnectionState State { get; set; }

    public override bool Connect()
    {
        Console.WriteLine("MsSqlConnection bağlantısı gerçekleştirildi.");
        State = ConnectionState.Open;
        return true;
    }

    public override bool Disconnect()
    {
        Console.WriteLine("MsSqlConnection bağlantısı koparıldı.");
        State = ConnectionState.Close;
        return true;
    }
}

class MySqlConnection : Connection
{
    public override ConnectionState State { get; set; }

    public override bool Connect()
    {
        Console.WriteLine("MySqlConnection bağlantısı gerçekleştirildi.");
        State = ConnectionState.Open;
        return true;
    }

    public override bool Disconnect()
    {
        Console.WriteLine("MySqlConnection bağlantısı koparıldı.");
        State = ConnectionState.Close;
        return true;
    }
}

class MsSqlCommand1 : Command
{
    public override void Execute(string query)
    {
        Console.WriteLine(query);
    }
}
class MySqlCommand : Command
{
    public override void Execute(string query)
    {
        Console.WriteLine(query);
    }
}

abstract class DatabaseFactory
{ 
    public abstract Connection CreateConnection();
    public abstract Command CreateCommand();
}

class MsSqlDatabaseFactory : DatabaseFactory
{
    public override Command CreateCommand()
    {
        MsSqlCommand1 command = new();
        return command;
    }

    public override Connection CreateConnection()
    {
        MsSqlConnection connection = new();
        //connection.ConnectionString = "MSSQL Connection String";
        return connection;
    }
}

class MySqlDatabaseFactory : DatabaseFactory
{
    public override Command CreateCommand()
    {
        MySqlCommand command = new();
        return command;
    }

    public override Connection CreateConnection()
    {
        MySqlConnection connection = new();
        //connection.ConnectionString = "MSSQL Connection String";
        return connection;
    }
}

class Database
{ 
    public Connection Connection { get; set; }
    public Command Command { get; set; }
}
class DatabaseCreator
{
    public Database Create(DatabaseFactory databaseFactory)
    {
        return new Database()
        {
            Command = databaseFactory.CreateCommand(),
            Connection = databaseFactory.CreateConnection()
        };
    }
}