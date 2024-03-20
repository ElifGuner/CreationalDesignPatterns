// See https://aka.ms/new-console-template for more information


using System.Collections.Concurrent;

//ObjectPool<X> pools = new ObjectPool<X>();
ObjectPool<X> pools = ObjectPool<X>.GetInstance;
var x1 = pools.Get(() => new X()); // Eğer ki havuzda X Türünde bir nesne varsa sen bana onu getir 
                                   // Yoksa vermiş olduğum X fonsksiyonunu çalıştır.
                                   // Bu X'i üret, bana onu geri döndür.
x1.Count++;
//...
pools.Return(x1); // X nesnesini havuza atıyoruz.

var x2 = pools.Get(() => new X()); // Artık yeni nesne üretmeyecek, var olan instance'ı getirecektir.
x2.Count++;
pools.Return(x2);


class ObjectPool<T> where T : class
{
    //ConcurrentBag tercih edilmesinin nedeni asenkron süreçlerinde yardımcı olması
    readonly ConcurrentBag<T> _instances;

    //Singleton Yapma Kodu, static constructor ile
    ObjectPool() //public kaldırıldı, private yapıldı
    {
        _instances = new();
    }
    private static ObjectPool<T> _objectPool;
    static ObjectPool() //public kaldırıldı, private yapıldı
    {
        _objectPool = new ObjectPool<T> ();
    }
     public static ObjectPool<T> GetInstance { get => _objectPool; }
    //**********************
    public ConcurrentBag<T> Instances { get => _instances; }
    public T Get(Func<T>? objectGenerator = null)
    {
        // Havuzdan generic parametrede bildirilen türdeki nesneyi geri döndürmek.
        return _instances.TryTake(out T instance) ? instance : objectGenerator();
    }

    public void Return(T instance)
    {
        // Havuzdan ödeünç alınan nesneyi geri iade etmek.
        _instances.Add(instance);
    }
}

class X
{
    public int Count { get; set; }
    public void Write()
        => Console.WriteLine(Count);

    public X()
        => Console.WriteLine("X üretim maliyeti...");
    ~X()
     => Console.WriteLine("X imha maliyeti...");
}
