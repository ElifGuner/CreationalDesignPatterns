// See https://aka.ms/new-console-template for more information

Person person1 = new Person("Elif", "Güner", Department.C, 100, 10);
//Person person2 = new Person("Hüseyin", "Güner", Department.C, 1000, 10); // constructor tekrar çalışıyor.

Person person2 = person1.Clone();    // constructor tekrar çalışmıyor.
person2.Name = "Hüseyin";
Console.WriteLine(person2.Name);
//Abstract Prototype
interface IPersonCloneable
{
   Person Clone();
}

//Concrete Prototype
class Person : IPersonCloneable
{
    public Person(string name, string surname, Department department, int salary, int premium)
    { 
        Name = name;
        Surname = surname;
        Department = department;
        Salary = salary;
        Premium = premium;
        Console.WriteLine("Person nesnesi oluşturuldu...");
    }

    public string Name { get; set; }
    public string Surname { get; set;}
    public Department Department { get; set;}
    public int Salary { get; set;}
    public int Premium { get; set;}

    public Person Clone()
    {
        return (Person)base.MemberwiseClone();
    }
}

enum Department {A, B, C }