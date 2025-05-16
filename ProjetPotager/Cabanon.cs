public class Cabanon
{
    private Terrain[,] terrains;

    public Cabanon(Terrain[,] terrains)
    {
        this.terrains = terrains;
    }

    public void PlanterUnePlante()
    {
        Console.Clear();
        Console.WriteLine("Quelle plante souhaitez-vous planter ?");
        Console.WriteLine("1 - Canne à sucre");
        Console.WriteLine("2 - Menthe");
        Console.WriteLine("3 - Citron vert");
        Console.WriteLine("4 - Cocotier");
        Console.WriteLine("5 - Cerisier");
        Console.WriteLine("6 - Ananas");

        int choix = int.Parse(Console.ReadLine() ?? "0");
        Plante? plante = choix switch
        {
            1 => new CanneASucre(),
            2 => new Menthe(),
            3 => new CitronVert(),
            4 => new Cocotier(),
            5 => new Cerisier(),
            6 => new Ananas(),
            _ => null
        };

        if (plante == null)
        {
            Console.WriteLine("Choix invalide.");
            return;
        }

        Console.WriteLine("Choisissez la position du terrain où planter (x y) :");
        int x = int.Parse(Console.ReadLine() ?? "0");
        int y = int.Parse(Console.ReadLine() ?? "0");

        if (x < 0 || y < 0 || x >= terrains.GetLength(0) || y >= terrains.GetLength(1))
        {
            Console.WriteLine("Coordonnées invalides.");
            return;
        }

        Terrain terrainChoisi = terrains[x, y];

        if (terrainChoisi.AjouterPlante(plante))
        {
            Console.WriteLine("✅ Plante ajoutée avec succès !");
        }
        else
        {
            Console.WriteLine("❌ Le terrain n'est pas adapté ou est trop petit.");
        }

        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }
}