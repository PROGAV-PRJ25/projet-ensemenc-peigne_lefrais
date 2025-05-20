using Potager.Models;

public abstract class Plante
{
    public string Nom { get; set; }
    public string Nature { get; set; }
    public string SaisonSemi { get; set; }
    public string TerrainPref { get; set; }
    public TypeTerrain TerrainCompatible { get; set; }
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

    public Plante(string nom, string nature, string saisonSemi, string terrainPref, TypeTerrain terrainCompatible,
                  double espacement, double placeNecessaire, string vitesseCroissance,
                  double besoinEau, double besoinLum, double temperaturePref,
                  string maladie, double espDeVie, double fruitsRecoltes)
    {
        Nom = nom;
        Nature = nature;
        SaisonSemi = saisonSemi;
        TerrainPref = terrainPref;
        TerrainCompatible = terrainCompatible;
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


 // Classes dérivées pour chaque type de plante avec comportements spécifiques
public class CanneASucre : Plante
{
    public CanneASucre() : base("Canne à sucre", "Vivace", "Septembre", "Sableux à côté d’une source d'eau", TypeTerrain.SableuxAvecEau,
                   120, 20, "Lente (1 an)", 1500, 100, 16, "Mildiou, rouille, gommose", 10, 180) {}

    public override string AffichageSymbole() => "CQS";
    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 10;
}

public class Menthe : Plante
{
    public Menthe() : base("Menthe", "Vivace", "Avril", "Sol bien drainé, humide", TypeTerrain.DraineHumide,
                   35, 50, "Rapide (quelques jours)", 1000, 70, 20, "Rouille, mildiou", 4, 15) {}

    public override string AffichageSymbole() => "MEN";
    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 2;
}

public class CitronVert : Plante
{
    public CitronVert() : base("Citron vert", "Vivace", "Mai", "Drainé, fertile", TypeTerrain.DraineFertile,
                   400, 1700, "Lente (45 cm/an)", 1000, 80, 25, "Gommose, mildiou", 40, 150) {}

    public override string AffichageSymbole() => "CIV";
    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 15;
}

public class Cocotier : Plante
{
    public Cocotier() : base("Cocotier", "Vivace", "Mars", "Bord de mer, sable pauvre, soleil, vent salé", TypeTerrain.SableuxAvecEau,
                   80, 750, "Très lente (7–10 ans)", 1460, 90, 30, "Bipolaris", 100, 35) {}

    public override string AffichageSymbole() => "COC";
    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 30;
}

public class Cerisier : Plante
{
    public Cerisier() : base("Cerisier", "Vivace", "Février", "Terrains calcaires", TypeTerrain.Calcaire,
                   500, 1000, "Moyenne/rapide", 1300, 100, 20, "Champignons", 50, 300) {}

    public override string AffichageSymbole() => "CER";
    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 20;
}

public class Ananas : Plante
{
    public Ananas() : base("Ananas", "Vivace", "Juin", "Sableux, bien drainé", TypeTerrain.SableuxDraine,
                   75, 100, "Lente (2 ans)", 900, 80, 26, "Fusariose", 5, 1) {}

    public override string AffichageSymbole() => "ANA";
    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 10;
}