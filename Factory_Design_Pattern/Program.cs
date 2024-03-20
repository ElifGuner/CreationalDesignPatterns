// See https://aka.ms/new-console-template for more information

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
    public void run()
    {
        throw new NotImplementedException();
    }
}

class B : IProduct
{
    public void run()
    {
        throw new NotImplementedException();
    }
}

class C : IProduct
{
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

#region Creattor
class ProductCreator
{
    public static IProduct GetInstance(ProductType productType)
    {
        IProduct _product = null;
        switch (productType) 
        {
            case ProductType.A:
                _product = new A();
                //...
                break; 
            case ProductType.B:
                _product = new B();
                //...
                break;
            case ProductType.C:
                _product = new C();
                //...
                break;
        }
        return _product;
    }
}

#endregion