using Potager.Models;

public static class AffichageTerrainDetail
{
    public static void Afficher(Terrain terrain, int numeroTerrain)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;


        Console.WriteLine("┌───────────────────────────────┐");

        string titre = $"📋 Détail du Terrain {numeroTerrain}";
        Console.WriteLine($"│ {titre.PadRight(29)} │");

        Console.WriteLine("└───────────────────────────────┘\n");


        if (!terrain.Plantes.Any())
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Aucune plante n'est présente sur ce terrain.\n");
        }
        else
        {
            int index = 1;
            foreach (var plante in terrain.Plantes)
            {
                string icone = ObtenirEmoji(plante);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{icone} Plante #{index++} : {plante.Nom} ({plante.GetType().Name})");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"  - Croissance actuelle : {plante.CroissanceActuelle}");

                Console.ForegroundColor = plante.EstVivante ? ConsoleColor.Green : ConsoleColor.DarkRed;
                Console.WriteLine($"  - État : {(plante.EstVivante ? "🌿 Vivante" : "💀 Morte")}");

                Console.ForegroundColor = string.IsNullOrEmpty(plante.Maladie) ? ConsoleColor.Gray : ConsoleColor.Red;
                Console.WriteLine($"  - Maladie : {(string.IsNullOrEmpty(plante.Maladie) ? "Aucune" : plante.Maladie)}");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  - Peut être récoltée : {(plante.PeutEtreRecoltee() ? "✅ Oui" : "❌ Non")}");
                Console.WriteLine();
            }
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Appuie sur une touche pour revenir.");
        Console.ReadKey(true);
    }
    private static string ObtenirEmoji(Plante plante)
    {
        return plante switch
        {
            Cocotier => "🌴",
            Ananas => "🍍",
            Menthe => "🌿",
            CitronVert => "🍋",
            Cerisier => "🍒",
            CanneASucre => "🍬",
            _ => "🌱" // Plante en général
        };
    }
}
