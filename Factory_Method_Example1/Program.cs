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


GarantiBank garanti = BankCreator.Create(BankType.Garanti) as GarantiBank;
HalkBank? halkbank = BankCreator.Create(BankType.Halkbank) as HalkBank;
VakifBank? vakifbank = BankCreator.Create(BankType.Vakifbank) as VakifBank;

GarantiBank garanti2 = BankCreator.Create(BankType.Garanti) as GarantiBank;
HalkBank? halkbank2 = BankCreator.Create(BankType.Halkbank) as HalkBank;
VakifBank? vakifbank2= BankCreator.Create(BankType.Vakifbank) as VakifBank;

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
        Console.WriteLine($"{nameof(GarantiBank)} nesnesi oluşturuldu.");
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
        Console.WriteLine($"{nameof(HalkBank)} nesnesi oluşturuldu.");
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
    string _userCode, _eMail,  _password;

    public VakifBank(CredentialVakifBank credential, string password)
    {
        _userCode = credential.UserCode;
        _eMail = credential.Mail;
        _password = password;
        Console.WriteLine($"{nameof(VakifBank)} nesnesi oluşturuldu.");
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
#endregion

#region Abstract Factory
interface IBankFactory
{
    public IBank CreateInstance();

}
#endregion

#region Concrete Factories
class GarantiFactory: IBankFactory
{
    public IBank CreateInstance()
    {

        GarantiBank garantiBank = new GarantiBank("asd", "123");
        garantiBank.ConnectGaranti();
        return garantiBank;
    }
}

class HalkbankFactory : IBankFactory
{
    public IBank CreateInstance()
    {

        HalkBank halkbank = new HalkBank("elif");
        halkbank.Password = "123";
        return halkbank;
    }
}

class VakifbankFactory : IBankFactory
{
    public IBank CreateInstance()
    {
        VakifBank vakifbank = new(new() { UserCode = "elif", Mail = "elif@yahoo.com" }, "elifpwd");
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
            BankType.Garanti => new GarantiFactory(),
            BankType.Halkbank => new HalkbankFactory(),
            BankType.Vakifbank => new VakifbankFactory()
        };
       return  _bankFactory.CreateInstance();
    }
}