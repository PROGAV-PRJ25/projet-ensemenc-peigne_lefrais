using Potager.Models;
public class Actions
{
    private List<Terrain> terrains;

    public Actions(List<Terrain> terrains)
    {
        this.terrains = terrains;
    }

    public void PlanterUneGraine(List<Terrain> terrains)
    {
        Console.Clear();
        Console.WriteLine("Quelle plante souhaitez-vous planter ?");
        Console.WriteLine("1. Canne à sucre");
        Console.WriteLine("2. Menthe");
        Console.WriteLine("3. Citron vert");
        Console.WriteLine("4. Cocotier");
        Console.WriteLine("5. Cerisier");
        Console.WriteLine("6. Ananas");
        Console.Write("Votre choix : ");
        string? choixPlante = Console.ReadLine();

        Plante? plante = choixPlante switch
        {
            "1" => new CanneASucre(),
            "2" => new Menthe(),
            "3" => new CitronVert(),
            "4" => new Cocotier(),
            "5" => new Cerisier(),
            "6" => new Ananas(),
            _ => null
        };

        if (plante == null)
        {
            Console.WriteLine("Plante inconnue.");
            return;
        }

        Console.Clear();
        Console.WriteLine("Choisissez un terrain :");
        for (int i = 0; i < terrains.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {terrains[i]}");
        }

        Console.Write("Numéro du terrain : ");
        if (!int.TryParse(Console.ReadLine(), out int indexTerrain) || indexTerrain < 1 || indexTerrain > terrains.Count)
        {
            Console.WriteLine("Choix invalide.");
            return;
        }

        Terrain terrain = terrains[indexTerrain - 1];

        // Vérifier la compatibilité
        if (!terrain.EstAdapté(plante))
        {
            Console.WriteLine("⚠️ Attention : Le terrain n'est pas adapté à cette plante. Elle risque de ne pas survivre longtemps.");
            Console.Write("Souhaitez-vous planter malgré tout ? (o/n) : ");
            string? reponse = Console.ReadLine()?.ToLower();
            if (reponse != "o")
            {
                Console.WriteLine("Plantation annulée.");
                return;
            }
        }

        // Vérifier la place restante
        if (terrain.SurfaceOccupée + plante.PlaceNecessaire > terrain.SurfaceTotale)
        {
            Console.WriteLine("❌ Il n'y a pas assez d'espace pour cette plante sur ce terrain.");
            return;
        }

        terrain.Plantes.Add(plante);
        Console.WriteLine($"✅ {plante.GetType().Name} plantée avec succès !");
        Console.WriteLine("\nAppuie sur une touche pour continuer...");
        Console.ReadKey(true);
    }

    public void Arroser(List<Terrain> terrains)
    {
        Console.Clear();
        Console.WriteLine("Choisis le terrain à arroser :");

        for (int i = 0; i < terrains.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Terrain {i + 1} (Humidité actuelle : {terrains[i].Humidite:F2})");
        }

        Console.Write("\nNuméro du terrain : ");
        if (int.TryParse(Console.ReadLine(), out int choix) && choix >= 1 && choix <= terrains.Count)
        {
            var terrain = terrains[choix - 1];
            double ancienneHumidite = terrain.Humidite;
            terrain.Humidite = Math.Min(1.0, terrain.Humidite + 0.2); // augmente de 0.2 max

            Console.WriteLine($"\nTerrain {choix} arrosé. Humidité : {ancienneHumidite:F2} → {terrain.Humidite:F2}");

            // Optionnel : effet direct sur les plantes
            foreach (var plante in terrain.Plantes)
            {
                plante.BeneficierArrosage();
            }
        }
        else
        {
            Console.WriteLine("Choix invalide.");
        }

        Console.WriteLine("\nAppuie sur une touche pour continuer.");
        Console.ReadKey(true);
    }

}