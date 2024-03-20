// See https://aka.ms/new-console-template for more information


using System.Collections.Concurrent;
using System.Xml.Serialization;

//ObjectPool<X> pools = new ObjectPool<X>();
ObjectPool<X> pool = ObjectPool<X>.GetInstance;

var t1 = Task.Run(() =>
    {
        while (true)
        {
            var x = pool.Get(() => new X());
            if (x != null)
            { 
                x.Count++;
                x.Write();
                pool.Return(x);
            }
        }   
    }   
);

var t2 = Task.Run(() =>
{
    while (true)
    {
        var x = pool.Get(() => new X());
        if (x != null)
        {
            x.Count++;
            x.Write();
            pool.Return(x);
        }
    }
}
);

await Task.WhenAll(t1, t2);

//ConcurrentBag;
/*
 * Asenkron süreçlerde kullanılan thread safe bir koleksiyondur.
 * Tüm threadler için bu koleksiyon nesnesinden bir model oluşturulmakta ve her bir iş
 * parçacığı kendisine ait model üzerinden çalışmaktadır. Böylece çakışma söz konusu olmamaktadır.
 * Her bir therad için, eklenen en sonuncu eleman elde edilir. Dolasıyla herhangi bir therad'de
 * eleman eklendiği durumlarda en sonuncu eleman istenildiği taktirde diğer thread'ler tarafından
 * son eleman olarak eklenenlerden biri rastgele alınacak ve geri gönderilecektir.
 */

//var x1 = pools.Get(() => new X()); // Eğer ki havuzda X Türünde bir nesne varsa sen bana onu getir 
//Yoksa vermiş olduğum X fonsksiyonunu çalıştır.
//                                    Bu X'i üret, bana onu geri döndür.
//x1.Count++;
//...
//pools.Return(x1); // X nesnesini havuza atıyoruz.

//var x2 = pools.Get(() => new X()); // Artık yeni nesne üretmeyecek, var olan instance'ı getirecektir.
//x2.Count++;
//pools.Return(x2);


class ObjectPool<T> where T : class
{
    //ConcurrentBag tercih edilmesinin nedeni asenkron süreçlerinde yardımcı olması
    readonly ConcurrentBag<T> _instances;
    readonly List<string> _types = new();
    //Singleton Yapma Kodu, static constructor ile
    ObjectPool() //public kaldırıldı, private yapıldı
    {
        _instances = new();
    }
    private static ObjectPool<T> _objectPool;
    static ObjectPool() //public kaldırıldı, private yapıldı
    {
        _objectPool = new ObjectPool<T>();
    }
    public static ObjectPool<T> GetInstance { get => _objectPool; }
    //**********************
    static object _o = new();
    public ConcurrentBag<T> Instances { get => _instances; }
    public T Get(Func<T>? objectGenerator = null)
    {
        lock(_o)
        {
            var state = _instances.TryTake(out T instance);
            if (!state && !_types.Any(t => t == nameof(T)))
            {
                T generatedInstance = objectGenerator();
                _types.Add(nameof(T));
                return generatedInstance;
            }
            // Havuzdan generic parametrede bildirilen türdeki nesneyi geri döndürmek.
            return instance;
        }
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
