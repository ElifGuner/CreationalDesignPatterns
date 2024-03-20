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


using System.Xml;

GarantiBank garanti = BankCreator.Create(BankType.Garanti) as GarantiBank;
HalkBank? halkbank = BankCreator.Create(BankType.Halkbank) as HalkBank;
VakifBank? vakifbank = BankCreator.Create(BankType.Vakifbank) as VakifBank;

GarantiBank garanti2 = BankCreator.Create(BankType.Garanti) as GarantiBank;
HalkBank? halkbank2 = BankCreator.Create(BankType.Halkbank) as HalkBank;
VakifBank? vakifbank2 = BankCreator.Create(BankType.Vakifbank) as VakifBank;

#region Abstract Product
interface IBank
{

}
#endregion

#region Concrete Products
class GarantiBank : IBank
{
    string _userCode, _password;

    GarantiBank(string userCode, string password)
    {
        _userCode = userCode;
        _password = password;
        Console.WriteLine($"{nameof(GarantiBank)} nesnesi oluşturuldu.");
    }

    static GarantiBank() => _garantiBank = new("asd", "123");
    static GarantiBank _garantiBank;
    public static GarantiBank GetInstance() => _garantiBank;
    public void ConnectGaranti()
        => Console.WriteLine($"{nameof(GarantiBank)} - connected.");

    public void SendMoney(int amount)
       => Console.WriteLine($"{amount} money sent.");
}

class HalkBank : IBank
{
    string _userCode, _password;

    HalkBank(string userCode)
    {
        _userCode = userCode;
        Console.WriteLine($"{nameof(HalkBank)} nesnesi oluşturuldu.");
    }

    static HalkBank() => _halkBank = new("elif");
    static HalkBank _halkBank;
    public static HalkBank GetInstance() => _halkBank;
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

    VakifBank(CredentialVakifBank credential, string password)
    {
        _userCode = credential.UserCode;
        _eMail = credential.Mail;
        _password = password;
        Console.WriteLine($"{nameof(VakifBank)} nesnesi oluşturuldu.");
    }

    static VakifBank() => _vakifBank = new(new() { UserCode = "elif", Mail = "elif@yahoo.com" }, "elifpwd");
    static VakifBank _vakifBank;
    public static VakifBank GetInstance() => _vakifBank;
    public bool ValidateCredential()
    {
        if (true) //validating
            return true;
        return false;
    }
    public void SendMoneyToAccountNumber(int amount, string recipientName, string accountNumber)
       => Console.WriteLine($"{amount} money sent.");
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
    static GarantiFactory() => _garantiFactory = new();
    static GarantiFactory _garantiFactory;
    public static GarantiFactory GetInstance() => _garantiFactory;
    public IBank CreateInstance()
    {
        //GarantiBank garantiBank = new GarantiBank();
        GarantiBank garantiBank = GarantiBank.GetInstance();
        garantiBank.ConnectGaranti();
        return garantiBank;
    }
}

class HalkbankFactory : IBankFactory
{
    static HalkbankFactory() => _halkbankFactory = new();
    static HalkbankFactory _halkbankFactory;
    public static HalkbankFactory GetInstance() => _halkbankFactory;
    public IBank CreateInstance()
    {

        // HalkBank halkbank = new HalkBank();
        HalkBank halkbank = HalkBank.GetInstance();
        halkbank.Password = "123";
        return halkbank;
    }
}

class VakifbankFactory : IBankFactory
{
    static VakifbankFactory() => _vakifbankFactory = new();
    static VakifbankFactory _vakifbankFactory;
    public static VakifbankFactory GetInstance() => _vakifbankFactory;
    public IBank CreateInstance()
    {
        //VakifBank vakifbank = new();
        VakifBank vakifbank = VakifBank.GetInstance();
        bool result = vakifbank.ValidateCredential();
        return vakifbank;
    }
}
#endregion

enum BankType
{
    Garanti, Halkbank, Vakifbank
}

class BankCreator
{
    public static IBank Create(BankType bankType)
    {

        IBankFactory _bankFactory = bankType switch
        {
            //BankType.Garanti => new GarantiFactory(),
            BankType.Garanti => GarantiFactory.GetInstance(),
            BankType.Halkbank => HalkbankFactory.GetInstance(),
            BankType.Vakifbank => VakifbankFactory.GetInstance()
        };
        return _bankFactory.CreateInstance();
    }
}