// See https://aka.ms/new-console-template for more information

Page homePage = PageCreator.Create(new HomePageFactory());
homePage.Title = "Home";

Page aboutPage = PageCreator.Create(new AboutPageFactory());
aboutPage.Title = "About";

Page contactPage = PageCreator.Create(new ContactPageFactory());
contactPage.Title = "Contact";
class Page 
{
    public string Title { get; set; }
    public IHeader Header { get; set; }
    public IBody Body { get; set; }
    public IFooter Footer { get; set; }
}

//Abstract Products
interface IHeader { }
interface IBody { }
interface IFooter { }

//Concrete products
class Header : IHeader 
{
    public Header(string text)
    {
        Console.WriteLine(text);
    }
}
class Body : IBody
{ 
   public Body(string text)
    {
        Console.WriteLine(text);
    }
}
class Footer : IFooter 
{
    public Footer(string text)
    {
        Console.WriteLine(text);
    }
}

// Abstract Factory
interface IPageFactory
{ 
    IHeader CreateHeader();
    IBody CreateBody();
    IFooter CreateFooter();
}

// Concrete Factories
class HomePageFactory : IPageFactory
{
    public IHeader CreateHeader()
    {
        return new Header("Home sayfası için Header oluşturuldu");
    }

    public IBody CreateBody()
    {
        return new Body ("Home sayfası için Body oluşturuldu");
    }

    public IFooter CreateFooter()
    {
        return new Footer("Home sayfası için Footer oluşturuldu");
    }
}

class AboutPageFactory : IPageFactory
{
    public IHeader CreateHeader()
    {
        return new Header("About sayfası için Header oluşturuldu");
    }

    public IBody CreateBody()
    {
        return new Body("About sayfası için Body oluşturuldu");
    }

    public IFooter CreateFooter()
    {
        return new Footer("About sayfası için Footer oluşturuldu");
    }
}

class ContactPageFactory : IPageFactory
{
    public IHeader CreateHeader()
    {
        return new Header("Contact sayfası için Header oluşturuldu");
    }

    public IBody CreateBody()
    {
        return new Body("Contact sayfası için Body oluşturuldu");
    }

    public IFooter CreateFooter()
    {
        return new Footer("Contact sayfası için Footer oluşturuldu");
    }
}

//Creator
class PageCreator
{
    public static Page Create(IPageFactory pageFactory)
    {
        Page page = new Page();
        page.Header = pageFactory.CreateHeader();
        page.Body = pageFactory.CreateBody();
        page.Footer = pageFactory.CreateFooter();
        return page;
    }
}