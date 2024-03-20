// See https://aka.ms/new-console-template for more information
var t1 = Task.Run(() =>
{
    Example.GetInstance();
});
var t2 = Task.Run(() =>
{
    Example.GetInstance();
});

await Task.WhenAll(t1, t2);

var t3 = Task.Run(() =>
{
    Example.GetInstance();
});
var t4 = Task.Run(() =>
{
    Example.GetInstance();
});

await Task.WhenAll(t3, t4);

class Example
{
    private Example()
    {
        Console.WriteLine($"{nameof(Example)} nesnesi üretildi");
    }

    static Example _example;
    static object _obj = new object();
    public static Example GetInstance()
    {
        lock (_obj)  // kaç tane asenkron property çağırırsa çağırsın,
                     // ilk çağrılan için kilitlenecek ve nesne oluşturacak.
        {
            if (_example == null)
                _example = new Example();
        }
        return _example;
    }

    // static contructor yöntemiyle de yapılabilir:
    static Example()
    {
        _example = new Example();
    }
    //GetInstance içindeki aşağıdaki alan commentlenir:

        //lock(_obj)  
        //{ 
        //    if (_example == null)
        //         _example = new Example();
        //    }
        //}
}
