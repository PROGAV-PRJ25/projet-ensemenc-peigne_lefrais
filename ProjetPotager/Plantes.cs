using Potager.Models;

    public enum TerrainPref
    {
        SableuxAvecEau,
        DraineHumide,
        DraineFertile,
        BordDeMer,
        Calcaire,
        SableuxDraine
    }
public abstract class Plante
{
    public string Nom { get; set; }
    public string Nature { get; set; }
    public string SaisonSemi { get; set; }
    public TerrainPref TerrainPref { get; set; }
    public double Espacement { get; set; }
    public double PlaceNecessaire { get; set; }
    public string VitesseCroissance { get; set; }
    public double BesoinEau { get; set; }
    public double BesoinLum { get; set; }
    public double TemperaturePref { get; set; }
    public string Maladie { get; set; }
    public double EspDeVie { get; set; }
    public double FruitsRecoltes { get; set; }
    public double CroissanceActuelle { get; set; }
    public bool EstVivante { get; set; } = true;

    public Plante(string nom, string nature, string saisonSemi, TerrainPref terrainPref,
                  double espacement, double placeNecessaire, string vitesseCroissance,
                  double besoinEau, double besoinLum, double temperaturePref,
                  string maladie, double espDeVie, double fruitsRecoltes)
    {
        Nom = nom;
        Nature = nature;
        SaisonSemi = saisonSemi;
        TerrainPref = terrainPref;
        Espacement = espacement;
        PlaceNecessaire = placeNecessaire;
        VitesseCroissance = vitesseCroissance;
        BesoinEau = besoinEau;
        BesoinLum = besoinLum;
        TemperaturePref = temperaturePref;
        Maladie = maladie;
        EspDeVie = espDeVie;
        FruitsRecoltes = fruitsRecoltes;
    }

    public abstract string AffichageSymbole();
    public abstract bool PeutEtreRecoltee();

    public virtual void Croitre(double lumiere, double eau, double temperature)
    {
        if (!EstVivante)
        {
            Console.WriteLine($"{Nom} est déjà morte.");
            return;
        }

        int conditionsRemplies = 0;
        if (Math.Abs(lumiere - BesoinLum) <= BesoinLum * 0.5) conditionsRemplies++;
        if (Math.Abs(eau - BesoinEau) <= BesoinEau * 0.5) conditionsRemplies++;
        if (Math.Abs(temperature - TemperaturePref) <= TemperaturePref * 0.5) conditionsRemplies++;

        if (conditionsRemplies < 2)
        {
            EstVivante = false;
            Console.WriteLine($"💀 {Nom} est morte à cause de mauvaises conditions !");
        }
        else
        {
            CroissanceActuelle += conditionsRemplies;
            Console.WriteLine($"🌿 {Nom} pousse bien ! Croissance actuelle : {CroissanceActuelle}");
        }
    }

    public virtual void Planter()
    {
        Console.WriteLine($"🌱 {Nom} a été plantée !");
        CroissanceActuelle = 0;
        EstVivante = true;
    }

    public virtual void Arroser()
    {
        if (!EstVivante)
        {
            Console.WriteLine($"💀 {Nom} est morte et ne peut pas être arrosée...");
            return;
        }

        Console.WriteLine($"💧 {Nom} a été arrosée !");
        CroissanceActuelle += 1;
    }

    public virtual void TraiterContreMaladie()
    {
        if (!EstVivante)
        {
            Console.WriteLine($"💀 {Nom} est morte et ne peut pas être traitée...");
            return;
        }

        if (!string.IsNullOrEmpty(Maladie))
        {
            Console.WriteLine($"🧪 {Nom} a été traitée contre {Maladie} !");
            Maladie = ""; // guérie
        }
        else
        {
            Console.WriteLine($"✅ {Nom} n'est pas malade.");
        }
    }

    public virtual bool Recolter()
    {
        if (!EstVivante)
        {
            Console.WriteLine($"💀 {Nom} est morte. Pas de récolte possible.");
            return false;
        }

        if (PeutEtreRecoltee())
        {
            Console.WriteLine($"🍒 {Nom} a été récoltée ! Quantité : {FruitsRecoltes}");
            return true;
        }
        else
        {
            Console.WriteLine($"⏳ {Nom} n'est pas encore prête à être récoltée.");
            return false;
        }
    }

    public virtual void AfficherInfos()
    {
        Console.WriteLine($"Nom: {Nom}\nNature: {Nature}\nSaison Semis: {SaisonSemi}\nTerrain: {TerrainPref}\n" +
                          $"Espace: {Espacement}cm, Surface: {PlaceNecessaire}cm²\nCroissance: {VitesseCroissance}\n" +
                          $"Besoins - Eau: {BesoinEau}, Lumière: {BesoinLum}, Température: {TemperaturePref}°C\n" +
                          $"Maladie: {Maladie}\nEspérance de vie: {EspDeVie} ans\nFruits/Récolte: {FruitsRecoltes}");
    }
}


 // Classes dérivées pour chaque type de plante avec comportements spécifiques dans les autres fichiers 
