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
        Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║ Terrain préféré : {plante.TerrainPref,-15} | Eau : {plante.BesoinEau,-4}mm | Lumière : {plante.BesoinLum,-4}h | Température : {plante.TemperaturePref}°C ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝\n");

        Console.WriteLine("╔════╦════════════════════╦════════╦══════════╦══════════════╦══════════════╦══════════════╗");
        Console.WriteLine("║ N°  ║ Type de terrain     ║ Humidité ║ Lumière  ║ Température ║ Surface utilisée ║ Plantes   ║");
        Console.WriteLine("╠════╬════════════════════╬════════╬══════════╬══════════════╬══════════════╬══════════════╣");

        for (int i = 0; i < terrains.Count; i++)
        {
            var t = terrains[i];
            bool estAdapte = t.EstAdapté(plante);
            string icone = estAdapte ? "✅" : "❌";

            // Couleur selon compatibilité
            Console.ForegroundColor = estAdapte ? ConsoleColor.Green : ConsoleColor.Red;

            Console.WriteLine($"║ {i + 1,-2} {icone} ║ {t.Type,-18} ║ {t.Humidite * 100,6:F1}% ║ {t.Luminosite,8}h ║ {t.Temperature,10}°C ║ {t.SurfaceOccupée,4} / {t.SurfaceTotale,-9} ║ {t.Plantes.Count,6} plante(s) ║");

            Console.ResetColor(); // On revient à la couleur normale
        }

        Console.WriteLine("╚════╩════════════════════╩════════╩══════════╩══════════════╩══════════════╩══════════════╝");
        Console.Write("\nNuméro du terrain : ");

        if (!int.TryParse(Console.ReadLine(), out int indexTerrain) || indexTerrain < 1 || indexTerrain > terrains.Count)
        {
            Console.WriteLine("Choix invalide.");
            return;
        }

        Terrain terrain = terrains[indexTerrain - 1];

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

        if (terrain.SurfaceOccupée + plante.PlaceNecessaire > terrain.SurfaceTotale)
        {
            Console.WriteLine("❌ Il n'y a pas assez d'espace pour cette plante sur ce terrain.");
            return;
        }

        terrain.Plantes.Add(plante);
        Console.WriteLine($"✅ {plante.GetType().Name} plantée avec succès !");
    }
}