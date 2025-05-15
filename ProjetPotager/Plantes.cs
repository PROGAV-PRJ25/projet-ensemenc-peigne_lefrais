public abstract class Plante
{
    public string Nom { get; set; }
    public string Nature { get; set; } // annuelle ou vivace 
    public string SaisonSemi { get; set; }
    public string TerrainPref { get; set; } // nature des sols 
    public TypeTerrain TerrainCompatible { get; set; } // nouveau champ structuré
    public double Espacement { get; set; } // espace entre deux plants 
    public double PlaceNecessaire { get; set; } // surface nécessaire et occupée 
    public string VitesseCroissance { get; set; }
    public double BesoinEau { get; set; }
    public double BesoinLum { get; set; }
    public double TemperaturePref { get; set; }
    public string Maladie { get; set; }
    public double EspDeVie { get; set; }
    public double FruitsRecoltes { get; set; } 
    public double CroissanceActuelle { get; set; } 
    ublic bool EstVivante { get; set; } = true;

    public Plante (string nom, string nature, string saisonSemi, string terrainPref, TypeTerrain terrainCompatible, double espacement, double placeNecessaire, string vitesseCroissance, double besoinEau, double besoinLum, double temperaturePref, string maladie, double espDeVie, double fruitsRecoltes)
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

    // public abstract void AfficherEtat(); // méthode abstraites à redéfinir dans les classes filles (plantes)
    
    // Affichage simplifié pour console (symbole ou code couleur)
    public abstract string AffichageSymbole();

     // Simule une étape de croissance selon conditions météo
    public abstract void Croître(double lumiere, double eau, double temperature);

    // Vérifie si la plante est prête à être récoltée
    public abstract bool PeutEtreRecoltee();

    // Affiche les infos détaillées de la plante
    public virtual void AfficherInfos()
    {
        Console.WriteLine($"Nom: {Nom}\nNature: {Nature}\nSaison Semis: {SaisonSemi}\nTerrain: {TerrainPref}\n" +
                            $"Espace: {Espacement}cm, Surface: {PlaceNecessaire}cm²\nCroissance: {VitesseCroissance}\n" +
                            $"Besoins - Eau: {BesoinEau}, Lumière: {BesoinLum}, Température: {TemperaturePref}°C\n" +
                            $"Maladie: {Maladie}\nEspérance de vie: {EspDeVie} ans\nFruits/Récolte: {FruitsRecoltes}");
    }
    
    // public virtual void Croissance(double lumiere, double eau, double temperature)
    // {
    //     // Exemple de logique simplifiée
    //     if (Math.Abs(temperature - TemperaturePref) > 10 || eau < BesoinEau / 2 || lumiere < BesoinLum / 2)
    //     {
    //         Console.WriteLine($"{Nom} pousse mal ou meurt...");
    //     }
    //     else
    //     {
    //         Console.WriteLine($"{Nom} pousse bien !");
    //     }
    // }
 
}

public override void Croître(double lumiere, double eau, double temperature)
{
    int conditionsRemplies = 0;
    if (Math.Abs(lumiere - BesoinLum) <= BesoinLum * 0.5) conditionsRemplies++;
    if (Math.Abs(eau - BesoinEau) <= BesoinEau * 0.5) conditionsRemplies++;
    if (Math.Abs(temperature - TemperaturePref) <= TemperaturePref * 0.5) conditionsRemplies++;

    if (conditionsRemplies < 2)
    {
        EstVivante = false;
        Console.WriteLine($"{Nom} est morte !");
    }
    else
    {
        CroissanceActuelle += conditionsRemplies;
        Console.WriteLine($"{Nom} pousse bien ! Croissance actuelle : {CroissanceActuelle}");
    }
}

 // Classes dérivées pour chaque type de plante avec comportements spécifiques
public class CanneASucre : Plante
{
    public CanneASucre() : base("Canne à sucre", "Vivace", "Septembre", "Sableux à côté d’une source d'eau", TypeTerrain.SableuxAvecEau,
                   120, 20, "Lente (1 an)", 1500, 100, 16, "Mildiou, rouille, gommose", 10, 180)
    {
    }

    public override string AffichageSymbole() => "CQS";

    public override void Croître(double lumiere, double eau, double temperature)
    {
        // Logique spécifique à la canne à sucre
    }

    public override bool PeutEtreRecoltee()
    {
        // Détermine si la canne est mature
        return true; // placeholder
    }
    }

    public class Menthe : Plante
    {

    public Menthe() : base("Menthe", "Vivace", "Avril", "Sol bien drainé, humide",TypeTerrain.DraineHumide,
                35, 50, "Rapide (quelques jours)", 1000, 70, 20, "Rouille, mildiou", 4, 15)
    {
    }

    public override string AffichageSymbole() => "MEN";
    public override void Croître(double lumiere, double eau, double temperature) { }
    public override bool PeutEtreRecoltee() => true;
}

    
public class CitronVert : Plante
{
    public CitronVert() : base("Citron vert", "Vivace", "Mai", "Drainé, fertile", TypeTerrain.DraineFertile,
                400, 1700, "Lente (45 cm/an)", 1000, 80, 25, "Gommose, mildiou", 40, 150)
{
}

    public override string AffichageSymbole() => "CIV";
    public override void Croître(double lumiere, double eau, double temperature) { }
    public override bool PeutEtreRecoltee() => true;
}


public class Cocotier : Plante
{
    public Cocotier() : base("Cocotier", "Vivace", "Mars", "Bord de mer, sable pauvre, soleil, vent salé", TypeTerrain.SableuxAvecEau,
                   80, 750, "Très lente (7–10 ans)", 1460, 90, 30, "Bipolaris", 100, 35)
    {
    }

    public override string AffichageSymbole() => "COC";
    public override void Croître(double lumiere, double eau, double temperature) { }
    public override bool PeutEtreRecoltee() => true;
}

    
public class Cerisier : Plante
{
       public Cerisier() : base("Cerisier", "Vivace", "Février", "Terrains calcaires", TypeTerrain.Calcaire,
                 500, 1000, "Moyenne/rapide", 1300, 100, 20, "Champignons", 50, 300)
    {
    }

    public override string AffichageSymbole() => "CER";
    public override void Croître(double lumiere, double eau, double temperature) { }
    public override bool PeutEtreRecoltee() => true;
}

    
public class Ananas : Plante
{
    public Ananas() : base("Ananas", "Vivace", "Juin", "Sableux, bien drainé", TypeTerrain.SableuxDraine, 
                   75, 100, "Lente (2 ans)", 900, 80, 26, "Fusariose", 5, 1)
    {
    }

    public override string AffichageSymbole() => "ANA";
    public override void Croître(double lumiere, double eau, double temperature) { }
    public override bool PeutEtreRecoltee() => true;
}

