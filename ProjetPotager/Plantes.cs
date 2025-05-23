using Potager.Models;

    // public enum TerrainPref
    // {
    //     SableuxAvecEau,
    //     DraineHumide,
    //     DraineFertile,
    //     BordDeMer,
    //     Calcaire,
    //     SableuxDraine
    // }
public abstract class Plante
{
    public string Nom { get; set; }
    public string Nature { get; set; }
    public string SaisonSemi { get; set; }
    public TypeTerrain TerrainPref { get; set; }
    public double Espacement { get; set; }
    public double PlaceNecessaire { get; set; }
    public string VitesseCroissance { get; set; }
    public double BesoinEau { get; set; } // en % humidité
    public double BesoinLum { get; set; } // heures/j
    public double TemperaturePref { get; set; } // en °C
    public string Maladie { get; set; }
    public double EspDeVie { get; set; }
    public double FruitsRecoltes { get; set; }
    public double CroissanceActuelle { get; set; } = 0;
    public double SeuilMaturite { get; set; } = 10; //valeur que l'on pourrait rendre spécifique pour chaque plante 
    public bool EstMature { get; set; } = false;
    public bool EstVivante { get; set; } = true;

    public Plante(string nom, string nature, string saisonSemi, TypeTerrain terrainPref,
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
            
            if (!EstMature && CroissanceActuelle >= SeuilMaturite)
            {
                EstMature = true;
                Console.WriteLine($"🎉 {Nom} est maintenant mature et prête à être récoltée !");
            }
        }
        
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


    public virtual void AfficherInfos()
    {
        Console.WriteLine($"Nom: {Nom}\nNature: {Nature}\nSaison Semis: {SaisonSemi}\nTerrain: {TerrainPref}\n" +
                          $"Espace: {Espacement}cm, Surface: {PlaceNecessaire}cm²\nCroissance: {VitesseCroissance}\n" +
                          $"Besoins - Eau: {BesoinEau}, Lumière: {BesoinLum}, Température: {TemperaturePref}°C\n" +
                          $"Maladie: {Maladie}\nEspérance de vie: {EspDeVie} ans\nFruits/Récolte: {FruitsRecoltes}");
    }

    public virtual void BeneficierArrosage()
    {
        if (!EstVivante)
        {
            Console.WriteLine($"💀 {Nom} est morte et ne peut pas bénéficier de l'arrosage.");
            return;
        }

        // Amélioration de la croissance grâce à l'arrosage
        CroissanceActuelle += 2;  // par exemple on booste la croissance de 2 unités

        // Réduction du risque ou guérison partielle de maladie (si maladie non vide)
        if (!string.IsNullOrEmpty(Maladie))
        {
            // Ici on peut imaginer que l'arrosage soigne un peu la plante
            Console.WriteLine($"💧 {Nom} bénéficie d'un bon arrosage, la maladie {Maladie} est moins agressive.");
            // Par simplicité, on peut choisir de diminuer le risque ou réduire la maladie
            // Exemple : on supprime la maladie 50% du temps (à ajuster selon la logique souhaitée)
            if (new Random().NextDouble() > 0.5)
            {
                Maladie = "";
                Console.WriteLine($"✅ {Nom} est guérie grâce à l'arrosage !");
            }
        }
        else
        {
            Console.WriteLine($"💧 {Nom} bénéficie pleinement de l'arrosage.");
        }
    }

}


// Classes dérivées pour chaque type de plante avec comportements spécifiques dans les autres fichiers 
