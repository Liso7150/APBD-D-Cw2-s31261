using System.Runtime.CompilerServices;

namespace Kontenery;

class Program
{
    static void Main(string[] args)
    {
        Produnkt[] produnkty = [
            new Produnkt("Bananas", 13.3),
            new Produnkt("Chocolade", 18),
            new Produnkt("Fish", 2),
            new Produnkt("Meat", -15),
            new Produnkt("Ice cream", -18),
            new Produnkt("Frozen pizza", -30),
            new Produnkt("Cheese", 7.3),
            new Produnkt("Susages", 5),
            new Produnkt("Butter", 20.5),
            new Produnkt("Eggs", 19),
        ];
    }
}

abstract class Kontener
{
    static int _nrKonteneru = 0;
    public Kontener(double wysokość, double głękokość, double masa, double max)
    {
        Wysokość = wysokość;
        Głębokość = głękokość;
        MasaWłasna = masa;
        MaksymalnyŁadunek = max;
    }
    
    protected double _masa_ładunku;
    public double MasaŁadunku
    {
        get
        {
            return _masa_ładunku;
        }
        set
        {
            if (value <= MaksymalnyŁadunek)
            {
                _masa_ładunku = value;
            }
            else
            {
                Console.Write(value + " jest większe niż " + MaksymalnyŁadunek);
            }
        }
    }

    public double Wysokość { get; init; }
     public double Głębokość { get; init; }
    public double MasaWłasna { get; init; }
    private string _numer_seryjny;
    public double MaksymalnyŁadunek { get; init; }
    protected string NumerSeryjny
    {
        get { return _numer_seryjny; }
        init { _numer_seryjny = "KON-"+value+"-"+(++_nrKonteneru);}
    }


    void OpruźnijŁadunek()
    {
        this.MasaŁadunku = 0;
    }

    void ZaladujŁadunek(int masa)
    {
        this.MasaŁadunku = masa;
    }
}

interface IHazardNotifier
{
    void sendHazardNotifier();
}
class KontenerNaPłyny : Kontener, IHazardNotifier
{

    KontenerNaPłyny(double wysokość, double głękokość, double masa, double max, bool bezpieczne) : base(wysokość, głękokość, masa, max)
    {
        NumerSeryjny = "L";
        Bezpieczne = bezpieczne;
    }
    bool Bezpieczne { get; }

    public void sendHazardNotifier()
    {
        Console.Write("niepeczna sytuacja w kontenerze"+this.NumerSeryjny);
    }
    
    public double MasaŁadunku
    {
        get
        {
            return _masa_ładunku;
        }
        init
        {
            if((Bezpieczne && value <= MaksymalnyŁadunek*0.9) || (!Bezpieczne && value <= MaksymalnyŁadunek*0.5))
            {
                _masa_ładunku = value;
            }
            else
            {
                Console.Write(value + " jest większe niż " + (Bezpieczne
                    ? "90% od " + MaksymalnyŁadunek + " czyli od " + MaksymalnyŁadunek * 0.9
                    : "50% od " + MaksymalnyŁadunek + " czyli od " + MaksymalnyŁadunek * 0.5));
                if (!Bezpieczne)
                {
                    this.sendHazardNotifier();
                }
            }
        }
    }
}

class KontenerNaGaz : Kontener, IHazardNotifier
{
    public KontenerNaGaz(double wysokość, double głękokość, double masa, double max) : base(wysokość, głękokość, masa, max)
    {
        NumerSeryjny = "G";
    }
    
    public void sendHazardNotifier()
    {
        Console.Write("niepeczna sytuacja w kontenerze"+this.NumerSeryjny);
    }

    public void OpruźnijŁadunek()
    {
        MasaŁadunku = MasaŁadunku * 0.05;
    }
    
    public double MasaŁadunku
    {
        get
        {
            return _masa_ładunku;
        }
        set
        {
                _masa_ładunku = value;
                
                if (_masa_ładunku > MaksymalnyŁadunek)
                {
                    this.sendHazardNotifier();
                }
        }
    }
        
}

class KontenerChłodniczy : Kontener, IHazardNotifier
{
    public KontenerChłodniczy(double wysokość, double głękokość, double masa, double max, Produnkt produnkt) : base(wysokość, głękokość, masa, max)
    {
        NumerSeryjny = "C";
        Typ =  produnkt;
    }
    
    public void sendHazardNotifier()
    {
        Console.Write("niepeczna sytuacja w kontenerze"+this.NumerSeryjny);
    }

    private Produnkt Typ { get; init; }
    private double _temperatura;

    double Temperatura
    {
        get { return _temperatura; }
        set
        {
            if (value >= Typ.Temperatura)
            {
                _temperatura = value;
            }
            else
            {
                Console.Write(Typ.Nazwa + " nie może być przetrzymywana w temperaturze " + value +
                              "\n Minimalna temperatura dla "+Typ.Nazwa+" to "+Typ.Temperatura);
            }
        }
    }
}

class Produnkt
{
    public Produnkt(string nazwa, double temperatura)
    {
        Nazwa = nazwa;
        Temperatura = temperatura;
    }
    
    public string Nazwa { get; init; }
    public double Temperatura { get; init; }
    
}
