// See https://aka.ms/new-console-template for more information

Example ex1 = Example.GetInstance;
Example ex2 = Example.GetInstance;
class Example
{
    private Example()
    {
        Console.WriteLine($"{nameof(Example)} nesnesi üretildi");
    }

    static Example _example;

    public static Example GetInstance
    {
        get
        {
            #region 1. yöntem
            //if (_example == null)
            //    _example = new Example();
            //return _example;
            #endregion
            #region 2. yöntem
            return _example;
            #endregion
        }
    }
    #region 2. yöntem
    static Example()
    {
        _example = new Example();
    }
    #endregion

}