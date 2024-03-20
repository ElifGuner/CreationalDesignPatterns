// See https://aka.ms/new-console-template for more information

// Creator, üretim sorumluluğunu alt factorylere bırakıyor.


A? a = ProductCreator.GetInstance(ProductType.A) as A;
a.run();

B? b = ProductCreator.GetInstance(ProductType.B) as B;
b.run();

#region Abstract Product
interface IProduct
{
    void run();
}
#endregion

#region Concrete Products
class A : IProduct
{
    public A()
    {
        Console.WriteLine($"{nameof(A)} nesnesi üretildi");
    }
    public void run()
    {
        throw new NotImplementedException();
    }
}

class B : IProduct
{
    public B()
    {
        Console.WriteLine($"{nameof(B)} nesnesi üretildi");
    }
    public void run()
    {
        throw new NotImplementedException();
    }
}

class C : IProduct
{
    public C()
    {
        Console.WriteLine($"{nameof(C)} nesnesi üretildi");
    }
    public void run()
    {
        throw new NotImplementedException();
    }
}
#endregion

enum ProductType
{
    A, B, C
}

#region Abstract Factory
interface IFactory
{
    IProduct CreateProduct();
}

#endregion

#region Factories

class AFactory : IFactory
{
    public IProduct CreateProduct()
    {
        A a = new A();
        return a;
    }
}

class BFactory : IFactory
{
    public IProduct CreateProduct()
    {
        B b = new B();
        return b;
    }
}

class CFactory : IFactory
{
    public IProduct CreateProduct()
    {
        C c = new C();
        return c;
    }
}

#endregion
#region Creator
class ProductCreator
{
    public static IProduct GetInstance(ProductType productType)
    {
        #region Factory Design Pattern
        // Burada nesne üretme sorumluluğunu alt factory sınıflarına bırakıyoruz.
        //IProduct _product = null;
        //switch (productType)
        //{
        //    case ProductType.A:
        //        _product = new A();
        //        //...
        //        break;
        //    case ProductType.B:
        //        _product = new B();
        //        //...
        //        break;
        //    case ProductType.C:
        //        _product = new C();
        //        //...
        //        break;
        //}
        //return _product;
        #endregion
        #region Factory Method Design Pattern
        IFactory _factory = productType switch
        {
            ProductType.A => new AFactory(),
            ProductType.B => new BFactory(),
            ProductType.C => new CFactory(),
        };
        return _factory.CreateProduct();
        #endregion
    }
}

#endregion