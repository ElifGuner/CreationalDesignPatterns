// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using System.Data;

//Database mySql = new Database();
//mySql.Connection = new Connection();
//mySql.Connection.ConnectionString = "...";
//mySql.Command = new Command();

//var result = mySql.Connection.Connect();
//if (result && mySql.Connection.State == ConnectionState.Open)
//{
//    mySql.Command.Execute("select * from ...");
//}

//mySql.Connection.Disconnect();


//Database oracle = new Database();
//oracle.Connection = new Connection();
//oracle.Connection.ConnectionString = "...";
//oracle.Command = new Command();

DatabaseCreator creator = new DatabaseCreator();
Database database = creator.CreateDatabase(new OracleDatabaseFactory());
Database database2 = creator.CreateDatabase(new MsSqlDatabaseFactory());
enum DatabaseType
{ 
    Oracle,
    MsSql,
    MySql,
    PostgreSql
}

class Database
{
    public DatabaseType Type { get; set; }
    public AbstractConnection Connection { get; set; }  
    public AbstractCommand Command { get; set; }
}

enum ConnectionState
{
    Open, Close
}

//Abstract Product
abstract class AbstractConnection
{
    public abstract string ConnectionString { get; set; }
    public abstract ConnectionState State { get; set; }
    public abstract bool Connect();
    public abstract bool Disconnect();
}

//Abstract Product
abstract class AbstractCommand
{
    public abstract void Execute(string query);
}

//Concrete Product
class Connection : AbstractConnection
{
    string _connectionString;

    public Connection (string connectionString)
    {
        _connectionString = connectionString;
    }

    public Connection()
    {
        
    }

    public override string ConnectionString { get => _connectionString; set => _connectionString = value; }
    public override ConnectionState State { get; set; }
    public override bool Connect()
    {
        //... bağlantı işlemleri yürütülüyor
        return true;
    }

    public override bool Disconnect()
    {
        //... bağlantı işlemleri yürütülüyor
        return true;
    }
}

//Concrete Product
class Command : AbstractCommand
{
    public override void Execute(string query)
    { 
        //...execute
    }
}

//Abstract Factory
abstract class DatabaseFactory
{ 
    public abstract AbstractConnection CreateConnection();
    public abstract AbstractCommand CreateCommand();
}

//Concrete Factory
class MsSqlDatabaseFactory : DatabaseFactory
{
    public override AbstractConnection CreateConnection()
    { 
        Connection connection = new Connection();
        connection.ConnectionString = "MSSQL Connection String";
        return connection;
    }
    public override AbstractCommand CreateCommand()
    {
        Command command = new();
        return command;
    }
}

//Concrete Factory
class OracleDatabaseFactory : DatabaseFactory
{
    public override AbstractConnection CreateConnection()
    {
        Connection connection = new Connection();
        connection.ConnectionString = "Oracle Connection String";
        return connection;
    }
    public override AbstractCommand CreateCommand()
    {
        Command command = new();
        return command;
    }
}

//Concrete Factory
class MySqlDatabaseFactory : DatabaseFactory
{
    public override AbstractConnection CreateConnection()
    {
        Connection connection = new Connection();
        connection.ConnectionString = "MySql Connection String";
        return connection;
    }
    public override AbstractCommand CreateCommand()
    {
        Command command = new();
        return command;
    }
}

//Concrete Factory
class PostgreSqlDatabaseFactory : DatabaseFactory
{
    public override AbstractConnection CreateConnection()
    {
        Connection connection = new Connection();
        connection.ConnectionString = "Postgresql Connection String";
        return connection;
    }
    public override AbstractCommand CreateCommand()
    {
        Command command = new();
        return command;
    }
}

//Creator
class DatabaseCreator
{
    AbstractConnection _connection;
    AbstractCommand _command;

    public Database CreateDatabase(DatabaseFactory databaseFactory)
    {
        Database db = new Database();
        _connection = databaseFactory.CreateConnection();
        _command = databaseFactory.CreateCommand();

        return new()
        {
            Command = _command,
            Connection = _connection,
            Type = (DatabaseType)Enum.Parse(typeof(DatabaseType), databaseFactory.GetType().Name.Replace("DatabaseFactory", ""))
        };
    }
}
