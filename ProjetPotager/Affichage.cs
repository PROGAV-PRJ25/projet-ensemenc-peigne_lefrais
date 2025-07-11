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
    private Urgence urgence;



    public Affichage()
    {
        meteo = new Meteo();

        // Utiliser la méthode statique pour créer les terrains variés
        tousLesTerrains = Terrain.CreerTousLesTerrains();

        urgence = new Urgence();



        actions = new Actions(tousLesTerrains);

        // Position initiale du joueur : sur Home (ligne des bâtiments)
        joueurY = batimentY;
        joueurX = homeX;
    }

    public void LancerPartie()
    {
        AffichageAccueil.Afficher();
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

        // Affichage des 3 bâtiments

        // Maison
        Console.SetCursorPosition(homeX, batimentY);
        Console.ForegroundColor = ConsoleColor.Green;
        if (joueurY == batimentY && joueurX == homeX)
            Console.Write($">🏠Home<");
        else
            Console.Write("🏠Home");

        // Cabanon
        Console.SetCursorPosition(cabanonX, batimentY);
        Console.ForegroundColor = ConsoleColor.Yellow;
        if (joueurY == batimentY && joueurX == cabanonX)
            Console.Write($">🛠️Cabanon<");
        else
            Console.Write("🛠️Cabanon");

        // Grange
        Console.SetCursorPosition(grangeX, batimentY);
        Console.ForegroundColor = ConsoleColor.Cyan;
        if (joueurY == batimentY && joueurX == grangeX)
            Console.Write($">🏚️ Grange<");
        else
            Console.Write("🏚️ Grange");

        // Affichage des terrains
        for (int terrainIndex = 0; terrainIndex < TERRAIN_COLS * TERRAIN_ROWS; terrainIndex++)
        {
            int row = terrainIndex / TERRAIN_COLS;
            int col = terrainIndex % TERRAIN_COLS;

            int startX = col * (GRID_SIZE * CELL_WIDTH + 4);
            int startY = 2 + row * (GRID_SIZE + 3);

            AfficherTerrainGraphique(startX, startY, terrainIndex + 1);
        }

        // Affichage du joueur -> plus fun avec un petit emoji de jardinier (malheureusement la jardinière ne fonctionnait pas...)
        Console.SetCursorPosition(joueurX, joueurY);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("👨");

        // Affichage de la météo
        Console.SetCursorPosition(0, TERRAIN_ROWS * (GRID_SIZE + 3));
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(meteo.ToString());

        // Instructions de jeu 
        Console.SetCursorPosition(0, TERRAIN_ROWS * (GRID_SIZE + 3) + 2);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Déplace-toi avec les flèches. Entrée = action, Espace = accès rapide cabanon, ECHAP = quitter."); 

        if (urgence.EstActive)
        {
            urgence.AfficherUrgence();
        }

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
                Console.WriteLine("Une nouvelle semaine est passée...");

                // Générer une semaine de météo
                var meteoSemaine = meteo.GenererMeteoSemaine(); // liste de 7 meteo

                // Faire avancer la simulation de 7 jours sans pause
                for (int jour = 0; jour < 7; jour++)
                {
                    var meteoDuJour = meteoSemaine[jour];
                    actions.PasserUnJour();  // méthode qui fait avancer la simulation d’un jour
                }

                // Mettre à jour la météo moyenne hebdo
                meteo.MettreAJourMoyenneHebdomadaire(meteoSemaine);

                // Affichage avec arrondi à 2 apres la virgule
                Console.WriteLine("\nNouvelle météo hebdomadaire :");
                Console.WriteLine(
                    $"Météo du jour : " +
                    $"{Math.Round(meteo.Ensoleillement, 2)}h de soleil, " +
                    $"{Math.Round(meteo.Pluie, 2)}mm de pluie, " +
                    $"{Math.Round(meteo.Temperature, 2)}°C."
                );

                Console.WriteLine("\nAppuie sur une touche pour reprendre.");
                Console.ReadKey(true);
            }
            else if (joueurX == cabanonX)
            {
                Console.Clear();
                Console.WriteLine("Bienvenue dans le cabanon 🛠️ !");
                actions.PlanterUneGraine();
            }
            else if (joueurX == grangeX)
            {
                Console.Clear();
                Console.WriteLine("Bienvenue dans la grange 🏚️ !");
                if (urgence.EstActive)
                {
                    Console.WriteLine("\nUne plante est malade !");
                    urgence.AfficherUrgence();
                    Console.WriteLine("\nSouhaitez-vous la soigner ? (O/N)");

                    var input = Console.ReadKey(true).Key;
                    if (input == ConsoleKey.O)
                    {
                        urgence.SoignerPlante();
                        Console.WriteLine("Appuie sur une touche pour continuer.");
                        Console.ReadKey(true);
                    }
                }
                Console.WriteLine("Inventaire des plantes disponibles :");
                Console.WriteLine("- Menthe\n- Citron vert\n- Ananas\n- Cerisier\n- Cocotier\n- Canne à sucre\n");
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
        else
        {
            // Le joueur n'est pas sur la ligne des bâtiments donc peut être sur un terrain
            int index = ObtenirIndexTerrainDepuisPosition(joueurX, joueurY);
            if (index != -1)
            {
                var terrain = tousLesTerrains[index];
                AffichageTerrainDetail.Afficher(terrain, index + 1);
            }
            else
            {
                // afficher un message si pas sur un terrain mais appuie sur entrer
                Console.WriteLine("Pas sur un terrain valide.");
                Console.ReadKey(true);
            }
        }
    }

    private int ObtenirIndexTerrainDepuisPosition(int x, int y)
    {
        for (int i = 0; i < TERRAIN_COLS * TERRAIN_ROWS; i++)
        {
            int row = i / TERRAIN_COLS;
            int col = i % TERRAIN_COLS;

            int startX = col * (GRID_SIZE * CELL_WIDTH + 4);
            int startY = 2 + row * (GRID_SIZE + 3);

            int endX = startX + GRID_SIZE * CELL_WIDTH;
            int endY = startY + GRID_SIZE;

            if (x >= startX && x <= endX && y >= startY && y <= endY)
            {
                return i;
            }
        }

        return -1;
    }

    private void GererTour()
    {
            // menu d'actions à faire dans le potager 
            Console.Clear();
            Console.WriteLine("Actions possibles :");
            Console.WriteLine("1. Arroser");
            Console.WriteLine("2. Planter");
            Console.WriteLine("3. Récolter");
            Console.WriteLine("4. Soigner ma plante");
            Console.WriteLine("Autre : revenir");

            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.D1)
            {
                actions.Arroser(tousLesTerrains);
                Thread.Sleep(1000);
            }
            else if (key == ConsoleKey.D2)
            {
                actions.PlanterUneGraine();
                Thread.Sleep(1000);
            }
            else if (key == ConsoleKey.D3)
            {
                actions.Recolter(tousLesTerrains);
                Thread.Sleep(1000);
            }

            else if (key == ConsoleKey.D4)
            {
            urgence.SoignerPlante();
            Thread.Sleep(1000);
            }

        // Lancer une urgence avec une probabilité de 1 chance sur 3
        Random rand = new Random();
        if (!urgence.EstActive && rand.Next(3) == 0)
        {
            // Extraire toutes les plantes en vie des terrains
            var plantesVives = tousLesTerrains.SelectMany(t => t.Plantes)
                                              .Where(p => p.EstVivante) 
                                              .ToList();

            if (plantesVives.Count > 0)
                urgence.ActiverUrgence(plantesVives);
        }

        // Faire grandir les plantes selon la météo
        foreach (var plante in tousLesTerrains.SelectMany(t => t.Plantes))
        {
            plante.Croitre(meteo.Ensoleillement, meteo.Pluie, meteo.Temperature);
        }
    }
}
