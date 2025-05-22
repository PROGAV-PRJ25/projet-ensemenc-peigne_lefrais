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

    private List<Terrain> tousLesTerrains;
    private Actions actions;
    private Meteo meteo;
    private ModeJeu mode;

    public enum ModeJeu
    {
        Classique,
        Urgence
    }

    public Affichage()
    {
        // Initialisation des terrains, plantes et météo
        meteo = new Meteo();
        mode = ChoisirMode();

        // Création d'exemple de terrains (6 terrains, 3 colonnes x 2 lignes)
        tousLesTerrains = new List<Terrain>();

        for (int i = 0; i < TERRAIN_COLS * TERRAIN_ROWS; i++)
        {
            var terrain = new Terrain
            {
                Type = TypeTerrain.SableuxAvecEau,
                Humidite = 0.8,
                Luminosite = 10,
                Temperature = 25,
                SurfaceTotale = 200
            };
            tousLesTerrains.Add(terrain);
        }

        actions = new Actions(tousLesTerrains);

        // Position initiale du joueur : sur Home (ligne bâtiments)
        joueurY = batimentY;
        joueurX = homeX;
    }

    public void LancerPartie()
    {
        bool partieEnCours = true;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;

        while (partieEnCours)
        {
            AfficherJardin();

            if (Console.KeyAvailable)
            {
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

                    if (key == ConsoleKey.LeftArrow)
                    {
                        if (joueurX == cabanonX) joueurX = homeX;
                        else if (joueurX == grangeX) joueurX = cabanonX;
                        // sinon ne bouge pas
                    }
                    else if (key == ConsoleKey.RightArrow)
                    {
                        if (joueurX == homeX) joueurX = cabanonX;
                        else if (joueurX == cabanonX) joueurX = grangeX;
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
                    // déplacement case par case
                    if (key == ConsoleKey.UpArrow && joueurY > 0) joueurY--;
                    else if (key == ConsoleKey.DownArrow && joueurY < maxY) joueurY++;
                    else if (key == ConsoleKey.LeftArrow && joueurX > 0) joueurX--;
                    else if (key == ConsoleKey.RightArrow && joueurX < maxX) joueurX++;
                }

                if (key == ConsoleKey.Enter)
                {
                    HandleInteraction();
                }
                else if (key == ConsoleKey.Escape)
                {
                    partieEnCours = false;
                }
                else if (key == ConsoleKey.Spacebar)
                {
                    GererTour();
                }
            }

            Thread.Sleep(100);
            meteo.GenererConditions();
            Console.Clear();
        }

        Console.WriteLine("Fin de partie !");
    }

    static int SautBatiment(int x)
    {
        int distHome = Math.Abs(x - homeX);
        int distCabanon = Math.Abs(x - cabanonX);
        int distGrange = Math.Abs(x - grangeX);

        int minDist = Math.Min(distHome, Math.Min(distCabanon, distGrange));

        if (minDist == distHome) return homeX;
        else if (minDist == distCabanon) return cabanonX;
        else return grangeX;
    }

    void AfficherJardin()
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

            AfficherTerrainGraphique(startX, startY, terrainIndex + 1);
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
        Console.WriteLine("Déplace-toi avec les flèches. Entrée = action, Espace = tour, ESC = quitter.");
    }

    static void AfficherTerrainGraphique(int offsetX, int offsetY, int terrainNumber)
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

    void HandleInteraction()
    {
        if (joueurY == batimentY)
        {
            if (joueurX == homeX)
            {
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
                actions.PlanterUneGraine(tousLesTerrains);
            }
            else if (joueurX == grangeX)
            {
                Console.Clear();
                Console.WriteLine("Bienvenue dans la grange 🏚️ !");
                Console.WriteLine("Inventaire des plantes disponibles :");
                Console.WriteLine("- Menthe\n- Citron vert\n- Ananas\n- Cerisier\n- Cocotier\n- Canne à sucre");
                Console.WriteLine("Plantes plantées :");
                foreach (var terrain in tousLesTerrains)
                {
                    if (terrain.Plantes.Any())
                    {
                        Console.WriteLine($"\n- Terrain {tousLesTerrains.IndexOf(terrain) + 1} :");
                        foreach (var plante in terrain.Plantes)
                        {
                            Console.WriteLine($"  • {plante.GetType().Name} (Croissance : {plante.VitesseCroissance:F2})");
                        }
                    }
                }
                Console.WriteLine("\nAppuie sur une touche pour revenir.");
                Console.ReadKey(true);
            }
        }
    }

    private void GererTour()
    {
        if (mode == ModeJeu.Classique)
        {
            // Exemple de menu simplifié (à étendre)
            Console.Clear();
            Console.WriteLine("Menu du tour :");
            Console.WriteLine("1. Arroser");
            Console.WriteLine("2. Semer");
            Console.WriteLine("3. Récolter");
            Console.WriteLine("4. Traiter");
            Console.WriteLine("Autre : revenir");

            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.D1)
            {
                Console.WriteLine("Arroser sélectionné (fonction non implémentée).");
                Thread.Sleep(1000);
            }
            else if (key == ConsoleKey.D2)
            {
                actions.PlanterUneGraine(tousLesTerrains);
                Thread.Sleep(1000);
            }
            else if (key == ConsoleKey.D3)
            {
                Console.WriteLine("Récolter sélectionné (fonction non implémentée).");
                Thread.Sleep(1000);
            }
            else if (key == ConsoleKey.D4)
            {
                Console.WriteLine("Traiter sélectionné (fonction non implémentée).");
                Thread.Sleep(1000);
            }
        }
        else if (mode == ModeJeu.Urgence)
        {
            Console.WriteLine("Gestion d'urgence (non implémentée).");
            Thread.Sleep(1000);
        }

        // Faire croître les plantes selon la météo
        foreach (var plante in tousLesTerrains.SelectMany(t => t.Plantes))
        {
            plante.Croitre(meteo.Ensoleillement, meteo.Pluie, meteo.Temperature);
        }
    }

    private ModeJeu ChoisirMode()
    {
        Console.Clear();
        Console.WriteLine("Choisissez un mode de jeu :");
        Console.WriteLine("1 - Mode Classique");
        Console.WriteLine("2 - Mode Urgence");
        ConsoleKey key;
        do
        {
            key = Console.ReadKey(true).Key;
        } while (key != ConsoleKey.D1 && key != ConsoleKey.D2);

        if (key == ConsoleKey.D1) return ModeJeu.Classique;
        else return ModeJeu.Urgence;
    }
}
