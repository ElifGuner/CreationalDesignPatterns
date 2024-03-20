// See https://aka.ms/new-console-template for more information


YemekDirector director = new();
Yemek sulu = director.YemekYap(new SuluYemekBuilder());
sulu.tarifiGoster();
Yemek etli = director.YemekYap(new EtliYemekBuilder());
etli.tarifiGoster();
Yemek sebzeli = director.YemekYap(new SebzeliYemekBuilder());
sebzeli.tarifiGoster();


enum YemekTipi
{ 
    Sulu, Etli, Sebzeli
}
class Yemek
{
    public YemekTipi YemekTipi { get; set; }
    public string YemekAdi { get; set; }
    public int TuzOranı { get; set; }

    public void tarifiGoster()
    {
        Console.WriteLine($" Yemek tipi : {YemekTipi} -> Yemek Adi: {YemekAdi} -> Tuz Orani {TuzOranı}");
    }
}

//Abstract Builder

abstract class YemekBuilder
{
    protected Yemek _yemek;

    public YemekBuilder()
        => _yemek = new Yemek();

    public Yemek Yemek { get => _yemek; }
    //public Yemek Yemek { get; set; }
    public abstract YemekBuilder SetYemekTipi(); // Bunların hepsini public abstract ile işaretledim.
    public abstract YemekBuilder SetYemekAdi();
    public abstract YemekBuilder SetTuzOrani();
}

// Concrete Builder
class SuluYemekBuilder : YemekBuilder
{
    public override YemekBuilder SetTuzOrani()
    {
        Yemek.TuzOranı = 3;
        return this;
    }

    public override YemekBuilder SetYemekAdi()
    {
        Yemek.YemekAdi = "Sulu Yemek";
        return this;
    }

    public override YemekBuilder SetYemekTipi()
    {
        Yemek.YemekTipi = YemekTipi.Sulu;
        return this;
    }
}

// Concrete Builder
class EtliYemekBuilder : YemekBuilder
{ 
    public override YemekBuilder SetTuzOrani()
    {
        Yemek.TuzOranı = 5;
        return this;
    }

    public override YemekBuilder SetYemekAdi()
    {
        Yemek.YemekAdi = "Etli Yemek";
        return this;
    }

    public override YemekBuilder SetYemekTipi()
    {
        Yemek.YemekTipi = YemekTipi.Etli;
        return this;
    }
}

// Concrete Builder
class SebzeliYemekBuilder : YemekBuilder
{
    public override YemekBuilder SetTuzOrani()
    {
        Yemek.TuzOranı = 1;
        return this;
    }

    public override YemekBuilder SetYemekAdi()
    {
        Yemek.YemekAdi = "Sebzeli Yemek";
        return this;
    }

    public override YemekBuilder SetYemekTipi()
    {
        Yemek.YemekTipi = YemekTipi.Sebzeli;
        return this;
    }
}

// Director

class YemekDirector
{
    public Yemek YemekYap(YemekBuilder yemekBuilder)
    {
         yemekBuilder.SetYemekTipi()
            .SetYemekAdi()
            .SetTuzOrani();
        return yemekBuilder.Yemek;
    }
}