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

class MenagerKontenerowców
{
    MenagerKontenerowców()
    {
        konteneroce = new List<Kontenerowiec>();
    }
    List<Kontenerowiec> konteneroce {get; init;}

    void przenieśKontener(Kontenerowiec k, Kontenerowiec j, Kontener o)
    {
        int nrStarego = -1;
        int nrNowego = -1;
        for (int i = 0; i < konteneroce.Count; i++)
        {
            if (konteneroce[i].NumerSeryjny == k.NumerSeryjny)
            {
                nrStarego = i;
            }

            if (konteneroce[i].NumerSeryjny == j.NumerSeryjny)
            {
                nrNowego = i;
            }
        }

        if (nrStarego == -1)
        {
            Console.Write("kontenerowiec \n");
            k.WypiszInfo();
            Console.Write("\n nie znaleziony w tablicy");
        } else if (nrNowego == -1)
        {
            Console.Write("kontenerowiec \n");
            j.WypiszInfo();
            Console.Write("\n nie znaleziony w tablicy");
        }
        else
        {

            if ((konteneroce[nrNowego].AktualnaMasa() + o.MasaWłasna + o.MaksymalnyŁadunek) <=
                konteneroce[nrNowego].Masa)
            {
                konteneroce[nrStarego].UsunKontener(o);
                konteneroce[nrNowego].DodajKontener(o);
            }
            else
            {
                Console.Write("kontenerowiec \n");
                konteneroce[nrNowego].WypiszInfo();
                Console.Write("\n nie ma wystarczająco miejsca by przyjąć kontener\n");
                o.WypiszInfo();
                
            }
        }
    }
}
class Kontenerowiec
{
    Kontenerowiec(double predkość, int ilość, double masa)
    {
        Kontenery = new Kontener[ilość];
        Predkość = predkość;
        Masa = masa;
        Ilość = ilość;
        index = 0;
    }

    private static int _nrKontenerowca = 0;
    private string _numer_seryjny;
    public string NumerSeryjny
    {
        get { return _numer_seryjny; }
        init { _numer_seryjny = "KON-OWIEC-"+(++_nrKontenerowca);}
    }


    private int index;
    public Kontener[] Kontenery { get; init; }
    double Predkość { get; init; }
    int Ilość { get; init; }
    public double Masa { get; init; }

    public double AktualnaMasa()
    {
        double masaŁączna = 0;
        foreach (var konte in Kontenery)
        {
            if (konte != null)
            {
                masaŁączna += konte.MasaWłasna + konte.MaksymalnyŁadunek;
            }
        }

        return masaŁączna;
    }

    public void DodajKontener(Kontener kontener)
    {

        if ((AktualnaMasa() + kontener.MasaWłasna + kontener.MaksymalnyŁadunek) <= Masa)
        {
            if (index >= Ilość)
            {
                Kontenery[index++] = kontener;
            }
            else
            {
                Console.Write("Kontener: " + kontener.NumerSeryjny +
                              " nie mieści się na staku ze wsgledu na maksymalną ilośc elementów");
            }
        }
        else
        {
            Console.Write("Kontener: " + kontener.NumerSeryjny + " nie mieści się na staku ze wsgledu na masę");
        }
    }

    public void DodajListeKontenerów(Kontener[] kontener)
    {
        foreach (var kon in kontener)
        {
            DodajKontener(kon);
        }
    }

    public void UsunKontener(Kontener kontener)
    {
        for (int i = 0; i < Kontenery.Length; i++)
        {
            if (Kontenery[i] != null)
            {
                if (Kontenery[i].NumerSeryjny == kontener.NumerSeryjny)
                {
                    Kontenery[i] = Kontenery[index--];
                }
            }
            else
            {
                Console.Write("Kontener: "+kontener.NumerSeryjny+" nie znaleziony na tym kontenerowcu");
            }
        }
    }

    public void ZaładujKontener(Kontener kontener, double MasaŁadunku)
    {
        for (int i = 0; i < Kontenery.Length; i++)
        {
            if (Kontenery[i] != null)
            {
                if (Kontenery[i].NumerSeryjny == kontener.NumerSeryjny)
                {
                    Kontenery[i].ZaladujŁadunek(MasaŁadunku);
                }
            }
            else
            {
                Console.Write("Kontener: "+kontener.NumerSeryjny+" nie znaleziony na tym kontenerowcu");
            }
        }
    }

    public void RozładujKontener(Kontener kontener)
    {
        for (int i = 0; i < Kontenery.Length; i++)
        {
            if (Kontenery[i] != null)
            {
                if (Kontenery[i].NumerSeryjny == kontener.NumerSeryjny)
                {
                    Kontenery[i].OpruźnijŁadunek();
                }
            }
            else
            {
                Console.Write("Kontener: "+kontener.NumerSeryjny+" nie znaleziony na tym kontenerowcu");
            }
        }
    }
    public void ZamieńKontener(Kontener kontener, Kontener nowyKontener)
    {
        for (int i = 0; i < Kontenery.Length; i++)
        {
            if (Kontenery[i] != null)
            {
                if (Kontenery[i].NumerSeryjny == kontener.NumerSeryjny)
                {
                    Kontenery[i] = nowyKontener;
                }
            }
            else
            {
                Console.Write("Kontener: "+kontener.NumerSeryjny+" nie znaleziony na tym kontenerowcu");
            }
        }
    }

    public void WypiszInfo()
    {
        Console.Write("Prędkość kontenerowca: "+Predkość+
                      "\n Maksymalna masa kontenerów na statku:"+Masa+
                      "\n Maksymalna ilość kontenerow na statku "+Ilość);
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
    
    public void WypiszInfo()
    {
        Console.Write("Numer seryjny: "+NumerSeryjny+
                      "\n Wysokość: "+Wysokość+
                      "\n Głębokość: "+Głębokość+
                      "\n Masa własna: "+MasaWłasna+
                      "\n MaksymalnyŁadunek: "+MaksymalnyŁadunek+
                      "\n Obecny ładunek: "+MasaŁadunku);
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
    public double MaksymalnyŁadunek { get; init; }
    private string _numer_seryjny;
    public string NumerSeryjny
    {
        get { return _numer_seryjny; }
        init { _numer_seryjny = "KON-"+value+"-"+(++_nrKonteneru);}
    }


    public void OpruźnijŁadunek()
    {
        this.MasaŁadunku = 0;
    }

    public void ZaladujŁadunek(double masa)
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

    public void WypiszInfo()
    {
        base.WypiszInfo();
        Console.Write("\n Czy ładunek należy do bezpiecznych: "+Bezpieczne);
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
    public void WypiszInfo()
    {
        base.WypiszInfo();
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
    public void WypiszInfo()
    {
        base.WypiszInfo();
        Console.Write("\n Typ ładunku: "+Typ.Nazwa+
                      "\n Temperatura kontenera: "+Temperatura);
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
