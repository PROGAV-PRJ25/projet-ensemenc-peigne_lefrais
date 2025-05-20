using Potager.Models;
public class Affichage
{
    const int GRID_SIZE = 5; // Taille d'une grille (5x5 cases)
    const int TERRAIN_COLS = 3;
    const int TERRAIN_ROWS = 2;
    const int CELL_WIDTH = 3; // Largeur visuelle d'une cellule
    static int joueurX = 0;
    static int joueurY = 0;

    // Coordonnées des bâtiments
    const int homeX = 4;
    const int cabanonX = 18;
    const int grangeX = 34;
    const int batimentY = 0; // ligne des 3 bâtiments

    const int homeWidth = 6;     // "🏠Home"
    const int cabanonWidth = 9;  // "🛠️Cabanon"
    const int grangeWidth = 9;   // "🏚️Grange"
    static Meteo meteo = new Meteo();

    public static void LancerTestAffichage()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;
        Console.Clear();

        bool playing = true;

        // Initialiser joueur sur la ligne bâtiments, sur home par défaut
        joueurY = batimentY;
        joueurX = homeX;

        while (playing)
        {
            AfficherJardin();

            var key = Console.ReadKey(true).Key;

            int maxX = TERRAIN_COLS * (GRID_SIZE * CELL_WIDTH + 4) - 1;
            int maxY = TERRAIN_ROWS * (GRID_SIZE + 3) + 1;

            if (joueurY == batimentY)
            {
                // Si le joueur n'est pas sur une des 3 positions, le placer sur la plus proche
                if (joueurX != homeX && joueurX != cabanonX && joueurX != grangeX)
                {
                    joueurX = SautBatiment(joueurX);
                }

                // déplacement mot par mot sur la ligne bâtiments
                if (key == ConsoleKey.LeftArrow)
                {
                    if (joueurX == cabanonX) joueurX = homeX;
                    else if (joueurX == grangeX) joueurX = cabanonX;
                    // si on est déjà à homeX, ne pas bouger
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    if (joueurX == homeX) joueurX = cabanonX;
                    else if (joueurX == cabanonX) joueurX = grangeX;
                    // si on est déjà à grangeX, ne pas bouger
                }
                else if (key == ConsoleKey.UpArrow && joueurY > 0)
                {
                    joueurY--;
                }
                else if (key == ConsoleKey.DownArrow && joueurY < maxY)
                {
                    joueurY++;
                }
            }
            else
            {
                // déplacement case par case en dehors ligne bâtiments
                if (key == ConsoleKey.UpArrow && joueurY > 0) joueurY--;
                else if (key == ConsoleKey.DownArrow && joueurY < maxY) joueurY++;
                else if (key == ConsoleKey.LeftArrow && joueurX > 0) joueurX--;
                else if (key == ConsoleKey.RightArrow && joueurX < maxX) joueurX++;
            }

            if (key == ConsoleKey.Enter) HandleInteraction();
            else if (key == ConsoleKey.Escape) playing = false;

            Console.Clear();
        }
    }

    static int SautBatiment(int x)
    {
        // renvoie la coordonnée x parmi homeX, cabanonX, grangeX la plus proche de x
        int distHome = Math.Abs(x - homeX);
        int distCabanon = Math.Abs(x - cabanonX);
        int distGrange = Math.Abs(x - grangeX);

        int minDist = Math.Min(distHome, Math.Min(distCabanon, distGrange));

        if (minDist == distHome) return homeX;
        else if (minDist == distCabanon) return cabanonX;
        else return grangeX;
    }

    static void AfficherJardin()
    {
        Console.ForegroundColor = ConsoleColor.White;

        // Affichage bâtiments

        // HOME
        Console.SetCursorPosition(homeX, batimentY);
        Console.ForegroundColor = ConsoleColor.Green;
        if (joueurY == batimentY && joueurX == homeX)
            Console.Write($">🏠Home<");
        else
            Console.Write("🏠Home");

        // CABANON
        Console.SetCursorPosition(cabanonX, batimentY);
        Console.ForegroundColor = ConsoleColor.Yellow;
        if (joueurY == batimentY && joueurX == cabanonX)
            Console.Write($">🛠️Cabanon<");
        else
            Console.Write("🛠️Cabanon");

        // GRANGE
        Console.SetCursorPosition(grangeX, batimentY);
        Console.ForegroundColor = ConsoleColor.Cyan;
        if (joueurY == batimentY && joueurX == grangeX)
            Console.Write($">🏚️ Grange<");
        else
            Console.Write("🏚️ Grange");

        // Affichage terrains
        for (int terrainIndex = 0; terrainIndex < TERRAIN_COLS * TERRAIN_ROWS; terrainIndex++)
        {
            int row = terrainIndex / TERRAIN_COLS;
            int col = terrainIndex % TERRAIN_COLS;

            int startX = col * (GRID_SIZE * CELL_WIDTH + 4);
            int startY = 2 + row * (GRID_SIZE + 3);

            AfficherTerrain(startX, startY, terrainIndex + 1);
        }

        // Affichage joueur
        Console.SetCursorPosition(joueurX, joueurY);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("👨");

        // Affichage météo
        Console.SetCursorPosition(0, TERRAIN_ROWS * (GRID_SIZE + 3));
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(meteo.ToString());

        // Instructions
        Console.SetCursorPosition(0, TERRAIN_ROWS * (GRID_SIZE + 3) + 2);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Déplace-toi avec les flèches. Entrée = action, ESC = quitter.");
    }

    static void AfficherTerrain(int offsetX, int offsetY, int terrainNumber)
    {
        for (int y = 0; y < GRID_SIZE; y++)
        {
            for (int x = 0; x < GRID_SIZE; x++)
            {
                int dessinX = offsetX + x * CELL_WIDTH;
                int dessinY = offsetY + y;

                Console.SetCursorPosition(dessinX, dessinY);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("[ ]");
            }
        }

        // Nom du terrain
        Console.SetCursorPosition(offsetX + (GRID_SIZE * CELL_WIDTH / 2) - 4, offsetY + GRID_SIZE);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"Terrain {terrainNumber}");
    }

    static void HandleInteraction()
    {
        if (joueurY == batimentY)
        {
            if (joueurX == homeX)
            {
                Console.Clear();
                Console.WriteLine("Tu es rentré à la maison 🏠. Bonne nuit !");
                Console.Clear();
                Console.WriteLine("Tu es rentré à la maison 🏠. Bonne nuit !");
                Console.WriteLine("Un nouveau jour commence...");
                meteo.GenererConditions();
                Console.WriteLine("\nNouvelle météo :");
                Console.WriteLine(meteo.ToString());
                Console.WriteLine("\nAppuie sur une touche pour reprendre.");
                Console.ReadKey(true);
            }
            else if (joueurX == cabanonX)
            {
                Console.Clear();
                Console.WriteLine("Bienvenue dans le cabanon 🛠️ !");
                Console.WriteLine("1. Planter une graine\n2. Arroser une plante\n3. Retour");
                Console.ReadKey(true);
            }
            else if (joueurX == grangeX)
            {
                Console.Clear();
                Console.WriteLine("Bienvenue dans la grange 🏚️ !");
                Console.WriteLine("Inventaire des plantes disponibles :");
                Console.WriteLine("- Menthe\n- Citron vert\n- Ananas\n- Cerisier\n- Cocotier\n- Canne à sucre");
                Console.WriteLine("\nPlantes plantées : (à compléter)");
                Console.ReadKey(true);
                Console.WriteLine("\nAppuie sur une touche pour revenir.");
                Console.ReadKey(true);
            }
        }
    }
    public static void AfficherTerrain(Terrain terrain)
    {
        Console.WriteLine("Terrain :");
        Console.WriteLine($" - Type : {terrain.Type}");
        Console.WriteLine($" - Humidité : {terrain.Humidite}");
        Console.WriteLine($" - Luminosité : {terrain.Luminosite}");
        Console.WriteLine($" - Température : {terrain.Temperature}");
        Console.WriteLine($" - Surface : {terrain.SurfaceTotale}");

        Console.WriteLine("\nPlantes présentes :");
        foreach (var plante in terrain.Plantes)
        {
            Console.WriteLine($" - {plante.GetType().Name} (Croissance : {plante.VitesseCroissance:F2})");
        }
        Console.WriteLine();
    }
}