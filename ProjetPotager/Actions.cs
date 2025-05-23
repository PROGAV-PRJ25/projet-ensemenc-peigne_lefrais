using Potager.Models;
public class Actions
{
    private List<Terrain> terrains;

    public Actions(List<Terrain> terrains)
    {
        this.terrains = terrains;
    }

    public void PlanterUneGraine()
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
        Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                             🔍 COMPARAISON TERRAIN / PLANTE                                ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════════╝");

        Console.WriteLine($"\n🌱 Plante choisie : {plante.Nom}");
        Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║ Terrain préféré : {plante.TerrainPref,-15} | Eau : {plante.BesoinEau,-4}mm | Lumière : {plante.BesoinLum,-4}h | Température : {plante.TemperaturePref}°C                                  ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝\n");

        Console.WriteLine("╔════╦══════════════════════════════╦════════════╦══════════════╦═════════════════╦═════════════════════╦════════════════════╗");
        Console.WriteLine("║ N° ║ Type de terrain              ║ Humidité   ║ Lumière      ║ Température     ║ Surface utilisée    ║ Plantes            ║");
        Console.WriteLine("╠════╬══════════════════════════════╬════════════╬══════════════╬═════════════════╬═════════════════════╬════════════════════╣");

        for (int i = 0; i < terrains.Count; i++)
        {
            var t = terrains[i];
            bool estAdapte = t.EstAdapté(plante);
            string icone = estAdapte ? "✅" : "❌";

            // Couleur selon compatibilité
            Console.ForegroundColor = estAdapte ? ConsoleColor.Green : ConsoleColor.Red;

            Console.WriteLine($"║ {i + 1,-2} {icone}{t.Type,-18}           ║ {t.Humidite * 100,6:F1}%    ║ {t.Luminosite,8}h    ║ {t.Temperature,10}°C    ║ {t.SurfaceOccupee,4} / {t.SurfaceTotale,-9}    ║ {t.Plantes.Count,6} plante(s)   ║");

            Console.ResetColor(); // On revient à la couleur normale
        }

        Console.WriteLine("╚════╩══════════════════════════════╩════════════╩══════════════╩═════════════════╩═════════════════════╩════════════════════╝");
        Console.Write("\nNuméro du terrain : ");

        if (!int.TryParse(Console.ReadLine(), out int indexTerrain) || indexTerrain < 1 || indexTerrain > terrains.Count)
        {
            Console.WriteLine("Choix invalide.");
            return;
        }

        Terrain terrain = terrains[indexTerrain - 1];

        // Si le terrain n’est pas adapté, on affiche juste un avertissement
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

        // On essaie quand même d'ajouter la plante (vérifie la surface uniquement)
        string resultat = terrain.AjouterPlante(plante);
        Console.WriteLine(resultat);

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

    public void Recolter(List<Terrain> terrains)
    {
        Console.Clear();
        Console.WriteLine("Récolte en cours...\n");

        int nbPlantesRecoltees = 0;

        foreach (var terrain in terrains)
        {
            var plantesMatures = terrain.Plantes.Where(p => p.EstMature).ToList();

            foreach (var plante in plantesMatures)
            {
                Console.WriteLine($"✔️ {plante.GetType().Name} récoltée sur Terrain {terrains.IndexOf(terrain) + 1}.");
                terrain.Plantes.Remove(plante); // Retirer la plante récoltée
                nbPlantesRecoltees++;
                // Ici tu pourrais l'ajouter à un inventaire si tu en as un
            }
        }

        if (nbPlantesRecoltees == 0)
        {
            Console.WriteLine("Aucune plante n'était prête à être récoltée.");
        }

        Console.WriteLine("\nAppuie sur une touche pour continuer.");
        Console.ReadKey(true);
    }

    public void PasserUnJour()
    {

        // Par exemple, tu peux mettre à jour les plantes dans tous les terrains :
        foreach (var terrain in terrains)
        {
            foreach (var plante in terrain.Plantes)
            {
                // Passer les conditions actuelles du terrain aux plantes
                plante.Croitre(terrain.Luminosite, terrain.Humidite, terrain.Temperature);
            }
        }
    }

}