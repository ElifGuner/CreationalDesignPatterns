// See https://aka.ms/new-console-template for more information
// See https://aka.ms/new-console-template for more information

Person person1 = new Person("Elif", "Güner", Department.C, 100, 10);
//Person person2 = new Person("Hüseyin", "Güner", Department.C, 1000, 10); // constructor tekrar çalışıyor.

//Person person2 = (Person)person1.Clone();    // constructor tekrar çalışmıyor.
Person? person2 = person1.Clone() as Person;   // as operatörü tipe döndürebiliyorsa dödürür, döndüremiyorsa null olarak döndürür. Bu ytüzden nullable yaptık.
person2.Name = "Hüseyin";
person2.Salary = 1000;
Console.WriteLine();



//Concrete Prototype
class Person : ICloneable
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
    public string Surname { get; set; }
    public Department Department { get; set; }
    public int Salary { get; set; }
    public int Premium { get; set; }
/* 1. yöntem
    public Person Clone()
    {
        return (Person)base.MemberwiseClone();
    }
*/    
    public object Clone()
    {
        return base.MemberwiseClone();
    }
}

enum Department { A, B, C }