using System.Reflection.Metadata.Ecma335;

namespace Singleton2_WebAPI_DBService_Example.Services
{
    public class DatabaseService
    {

        private DatabaseService()
        {
            Console.WriteLine($"{nameof(DatabaseService)} nesnesi üretildi");
        }

        static DatabaseService _databaseService;

        public static DatabaseService GetInstance
        {
            get
            {
                if (_databaseService == null)
                    _databaseService = new DatabaseService();
                return _databaseService;
            }
        }

        public int Count { get; set; }
        public bool Connect()
        {
            Count++;
            Console.WriteLine("Bağlantı sağlandı");
            return true;
        }

        public bool Disconnect()
        {
            Console.WriteLine("Bağlantı kopraıldı");
            return true;
        }
    }  
}
