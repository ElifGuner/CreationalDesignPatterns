// See https://aka.ms/new-console-template for more information
// Concrete Builder sınıflarının nesnelerini Factory Method ile nasıl üretebiliriz, bunu inceliyoruz.
// Geliştirici hangi builder sınıfını kullanacaksa, onunla ilgili instance'ın üretim sorumluluğuna hala sahip.
// Aynı nesne grubundan olan nesnelerin üretimini Factory Method ile gerçekleştiriyoruz.
// Build metodunun içine verilen nesneleri Factory yöntemi kullanarak üreteceğiz. switch - case yapısıyla.

/* Factory'siz

ArabaDirector director = new();
Araba opel = director.Build(new OpelBuilder());
opel.ToString();
Araba mercedes = director.Build(new MercedesBuilder());
mercedes.ToString();
Araba bmw = director.Build(new BMWBuilder());
bmw.ToString();
*/
// Factory yöntemi ile

ArabaDirector director = new();
Araba opel = director.Build(BuilderCreator.Create(BuilderType.Opel));
opel.ToString();
Araba mercedes = director.Build(BuilderCreator.Create(BuilderType.Mercedes));
mercedes.ToString();
Araba bmw = director.Build(BuilderCreator.Create(BuilderType.BMW));
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

#region Abstract Class ile abstract builderın tasarlanması

// Abstract Builder
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

// Concrete Builder & Product
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

// Concrete Builder & Product
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

// Concrete Builder & Product
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
#region Factory yöntemi için eklenenler
// Creator
class BuilderCreator
{
    public static ArabaBuilder Create(BuilderType builderType)
    {
        return builderType switch
        {
            BuilderType.Opel => new OpelBuilder(),
            BuilderType.Mercedes => new MercedesBuilder(),
            BuilderType.BMW => new BMWBuilder()
        };
    }
}

enum BuilderType
{ 
    Opel, Mercedes, BMW
}


#endregion