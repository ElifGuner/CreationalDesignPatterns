// See https://aka.ms/new-console-template for more information
/*
//Mercedes
Araba mercedes = new();
mercedes.KM = 0;
mercedes.Marka = "Mercedes";
mercedes.Model = "...";
mercedes.Vites = true;

//BMW
Araba bmw = new();
bmw.KM = 10;
bmw.Marka = "BMW";
bmw.Model = "...";
bmw.Vites = false;
*/
// sisteme istediğimiz Builder'ı katabiliriz. Open-Closed principle'a uygun.


ArabaDirector director = new();
Araba opel = director.Build(new OpelBuilder());
opel.ToString();
Araba mercedes = director.Build(new MercedesBuilder());
mercedes.ToString();
Araba bmw = director.Build(new BMWBuilder());
bmw.ToString();


//Product
class Araba
{ 
    public string Marka { get; set; }
    public string Model { get; set; }
    public double KM { get; set; }
    public bool Vites { get; set; }

    public override string ToString()
    {
        Console.WriteLine($"{Marka} marka araba {Model} modelinde {KM} kilometrede {Vites} vites olarak üretilmiştir.");

        return base.ToString();

    }
}


# region Interface ile abstract builderın tasarlanması
// Abstract Builder
//interface IArabaBuilder
//{
//    Araba Araba { get; }
//    /* 
//     * Fluent pattern'a uygun bir yapı oluşturmak için geri dönüş değerlerini 
//     * IArabaBuilder türünden yaparız. Böylece fonk.ları birbiri ardına çağırabiliriz.
//    void SetKM();
//    void SetMarka();
//    void SetModel();
//    void SetVites();
//    */
//    IArabaBuilder SetKM();
//    IArabaBuilder SetMarka();
//    IArabaBuilder SetModel();
//    IArabaBuilder SetVites();
//}

//// Concrete Builder
//class OpelBuilder : IArabaBuilder
//{
//    public Araba Araba { get; }

//    public OpelBuilder()
//        => Araba = new();
//    public IArabaBuilder SetKM()
//    {
//        Araba.KM = 0;
//        return this;
//    }

//    public IArabaBuilder SetMarka()
//    {
//        Araba.Marka = "...";
//        return this;
//    }

//    public IArabaBuilder SetModel()
//    {
//        Araba.Model = "Opel";
//        return this;
//    }

//    public IArabaBuilder SetVites()
//    {
//        Araba.Vites = true;
//        return this;
//    }
//}

//// Concrete Builder
//class MercedesBuilder : IArabaBuilder
//{
//    public Araba Araba { get; }

//    public MercedesBuilder()
//        => Araba = new();

//    public IArabaBuilder SetKM()
//    {
//        Araba.KM = 100;
//        return this;
//    }

//    public IArabaBuilder SetMarka()
//    {
//        Araba.Marka = "Mercedes";
//        return this;
//    }

//    public IArabaBuilder SetModel()
//    {
//        Araba.Model = "xyz";
//        return this;
//    }

//    public IArabaBuilder SetVites()
//    {
//        Araba.Vites = false;
//        return this;
//    }
//}

//// Concrete Builder
//class BMWBuilder : IArabaBuilder
//{
//    public Araba Araba { get; }

//    public BMWBuilder()
//         => Araba = new();

//    public IArabaBuilder SetKM()
//    {
//        Araba.KM = 10;
//        return this;
//    }

//    public IArabaBuilder SetMarka()
//    {
//        Araba.Marka = "BMW";
//        return this;
//    }

//    public IArabaBuilder SetModel()
//    {
//        Araba.Model = "xyz";
//        return this;
//    }

//    public IArabaBuilder SetVites()
//    {
//        Araba.Vites = false;
//        return this;
//    }
//}

////Director
//class ArabaDirector
//{
//    public Araba Build(IArabaBuilder arabaBuilder)
//    {
//        //arabaBuilder.SetMarka();
//        //arabaBuilder.SetKM();
//        //arabaBuilder.SetModel();
//        //arabaBuilder.SetVites();

//        //Fluent pattern
//        arabaBuilder.SetMarka()
//            .SetModel()
//            .SetKM()
//            .SetVites();
//        return arabaBuilder.Araba;
//    }
//}
#endregion

// Interface yerine abstract class kullanırsa Builder sınıflarındaki Araba 
//Property'sini lüzümsuz yere tanımlamak zorunda kalmayız.

#region Abstract Class ile abstract builderın tasarlanması

abstract class ArabaBuilder
{
    protected Araba araba;
    public Araba Araba { get => araba; }  // Bu property'i artık zoraki implement ettirmeyeceğim. public diyerek dışarıdan erişilebilir hale getiriyorum.
                                          // araba field'ını encapsulate ettim
    public ArabaBuilder()   // Bunuda concrete classlardan buraya aldım.
        => araba = new();
    /* 
     * Fluent pattern'a uygun bir yapı oluşturmak için geri dönüş değerlerini 
     * IArabaBuilder türünden yaparız. Böylece fonk.ları birbiri ardına çağırabiliriz.
    void SetKM();
    void SetMarka();
    void SetModel();
    void SetVites();
    */
    public abstract ArabaBuilder SetKM(); // Bunların hepsini public abstract ile işaretledim.
    public abstract ArabaBuilder SetMarka();
    public abstract ArabaBuilder SetModel();
    public abstract ArabaBuilder SetVites();
}

// Concrete Builder
class OpelBuilder : ArabaBuilder
{
    public override ArabaBuilder SetKM()
    {
        Araba.KM = 0;
        return this;
    }

    public override ArabaBuilder SetMarka()
    {
        Araba.Marka = "Opel";
        return this;
    }

    public override ArabaBuilder SetModel()
    {
        Araba.Model = "...";
        return this;
    }

    public override ArabaBuilder SetVites()
    {
        Araba.Vites = true;
        return this;
    }
}

// Concrete Builder
class MercedesBuilder : ArabaBuilder
{
    public override ArabaBuilder SetKM()
    {
        Araba.KM = 100;
        return this;
    }

    public override ArabaBuilder SetMarka()
    {
        Araba.Marka = "Mercedes";
        return this;
    }

    public override ArabaBuilder SetModel()
    {
        Araba.Model = "xyz";
        return this;
    }

    public override ArabaBuilder SetVites()
    {
        Araba.Vites = false;
        return this;
    }
}

// Concrete Builder
class BMWBuilder : ArabaBuilder
{
    public override ArabaBuilder SetKM()
    {
        Araba.KM = 10;
        return this;
    }

    public override ArabaBuilder SetMarka()
    {
        Araba.Marka = "BMW";
        return this;
    }

    public override ArabaBuilder SetModel()
    {
        Araba.Model = "xyz";
        return this;
    }

    public override ArabaBuilder SetVites()
    {
        Araba.Vites = false;
        return this;
    }
}

//Director
class ArabaDirector
{
    public Araba Build(ArabaBuilder arabaBuilder)
    {
        //arabaBuilder.SetMarka();
        //arabaBuilder.SetKM();
        //arabaBuilder.SetModel();
        //arabaBuilder.SetVites();

        //Fluent pattern
        arabaBuilder.SetMarka()
            .SetModel()
            .SetKM()
            .SetVites();
        return arabaBuilder.Araba;
    }
}

#endregion