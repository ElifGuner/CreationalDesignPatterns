// See https://aka.ms/new-console-template for more information

//GarantiBank garantiBank = new GarantiBank("asd","123");
//garantiBank.ConnectGaranti();

//HalkBank halkbank = new HalkBank("elif");
//halkbank.Password = "123";

//VakifBank vakifbank = new(new (){ UserCode="elif" , Mail="elif@yahoo.com" }, "elifpwd");
//bool result = vakifbank.ValidateCredential();
//if (result)
//{
//    //...
//}


using System.Reflection;

GarantiBank garanti = BankCreator.Create(BankType.Garanti) as GarantiBank;
HalkBank? halkbank = BankCreator.Create(BankType.HalkBank) as HalkBank;
VakifBank? vakifbank = BankCreator.Create(BankType.VakifBank) as VakifBank;

GarantiBank garanti2 = BankCreator.Create(BankType.Garanti) as GarantiBank;
HalkBank? halkbank2 = BankCreator.Create(BankType.HalkBank) as HalkBank;
VakifBank? vakifbank2 = BankCreator.Create(BankType.VakifBank) as VakifBank;

#region Abstract Product
interface IBank
{

}
#endregion

#region Concrete Products
class GarantiBank : IBank
{
    string _userCode, _password;

    public GarantiBank(string userCode, string password)
    {
        _userCode = userCode;
        _password = password;
    }

    public void ConnectGaranti()
        => Console.WriteLine($"{nameof(GarantiBank)} - connected.");

    public void SendMoney(int amount)
       => Console.WriteLine($"{amount} money sent.");
}

class HalkBank : IBank
{
    string _userCode, _password;

    public HalkBank(string userCode)
    {
        _userCode = userCode;
    }

    public string Password { set => _password = value; }

    public void Send(int amount, string accountNumber)
       => Console.WriteLine($"{amount} money sent.");
}

class CredentialVakifBank
{
    public string UserCode { get; set; }
    public string Mail { get; set; }
}
class VakifBank : IBank
{
    string _userCode, _eMail, _password;

    public VakifBank(CredentialVakifBank credential, string password)
    {
        _userCode = credential.UserCode;
        _eMail = credential.Mail;
        _password = password;
    }

    public bool ValidateCredential()
    {
        if (true) //validating
            return true;
        return false;
    }
    public void SendMoneyToAccountNumber(int amount, string recipientName, string accountNumber)
       => Console.WriteLine($"{amount} money sent.");
}

class Isbank : IBank
{

}
#endregion

#region Abstract Factory
interface IBankFactory
{
    public IBank CreateInstance();

}
#endregion

#region Concrete Factories
class GarantiFactory : IBankFactory
{
    public IBank CreateInstance()
    {

        GarantiBank garantiBank = new GarantiBank("asd", "123");
        garantiBank.ConnectGaranti();
        return garantiBank;
    }
}

class HalkBankFactory : IBankFactory
{
    public IBank CreateInstance()
    {

        HalkBank halkbank = new HalkBank("elif");
        halkbank.Password = "123";
        return halkbank;
    }
}

class VakifBankFactory : IBankFactory
{
    public IBank CreateInstance()
    {
        VakifBank vakifbank = new(new() { UserCode = "elif", Mail = "elif@yahoo.com" }, "elifpwd");
        bool result = vakifbank.ValidateCredential();
        return vakifbank;
    }
}

class IsbankFactory : IBankFactory
{
    public IBank CreateInstance()
    {
        return new Isbank();
    }
}


#endregion

enum BankType
{
    Garanti, HalkBank, VakifBank, Isbank
}

class BankCreator
{
    public static IBank Create(BankType bankType)
    {

        //IBankFactory _bankFactory = bankType switch
        //{
        //    BankType.Garanti => new GarantiFactory(),
        //    BankType.HalkBank => new HalkbankFactory(),
        //    BankType.VakifBank => new VakifbankFactory(),
        //    BankType.Isbank => new IsbankFactory()
        //};
        //return _bankFactory.CreateInstance();

        string factory = $"{bankType.ToString()}Factory";
        Type? type = Assembly.GetExecutingAssembly().GetType(factory);
        IBankFactory? bankFactory = Activator.CreateInstance(type) as IBankFactory; // parametredeki ismin type'ından nesne instance'ı oluşturur. object olarak üretildiği için cast edilir.
        return bankFactory.CreateInstance();
        // Bunu yaparsak yeni factory eklendiğinde creator sınıfını tekrar güncellemek zorunda kalmıyoruz.
    }
}
// yeni product eklşenince yeni factory eklenmek zorunda kalır. Creater yardımcı sınıfı da değiştirilmek zorunda kalınır.