using Potager.Models;

public static class AffichageTerrainDetail
{
    public static void Afficher(Terrain terrain, int numeroTerrain)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"📋 Détail du Terrain {numeroTerrain}\n");

        if (!terrain.Plantes.Any())
        {
            Console.WriteLine("Aucune plante n'est présente sur ce terrain.");
        }
        else
        {
            foreach (var plante in terrain.Plantes)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"🌱 {plante.Nom} ({plante.GetType().Name})");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"- Croissance actuelle : {plante.CroissanceActuelle}");
                Console.WriteLine($"- État : {(plante.EstVivante ? "🌿 Vivante" : "💀 Morte")}");
                Console.WriteLine($"- Maladie : {(string.IsNullOrEmpty(plante.Maladie) ? "Aucune" : plante.Maladie)}");
                Console.WriteLine($"- Peut être récoltée : {(plante.PeutEtreRecoltee() ? "✅ Oui" : "❌ Non")}");
                Console.WriteLine();
            }
        }

        Console.WriteLine("\nAppuie sur une touche pour revenir.");
        Console.ReadKey(true);
    }
}