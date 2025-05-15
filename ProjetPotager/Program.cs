class Affichage
    {
        const int GRID_SIZE = 5; // Taille d'une cellule
        const int WIDTH = 20; // Largeur du terrain
        const int HEIGHT = 10; // Hauteur du terrain
        static int playerX = 2; // Position initiale du joueur
        static int playerY = 2; // Position initiale du joueur

        static void Main(string[] args)
        {
            // Configuration des couleurs
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            // Boucle principale du jeu
            while (true)
            {
                // Affichage du terrain
                DisplayTerrain();

                // Attente de l'action du joueur
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (playerY > 0) playerY--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (playerY < HEIGHT - 1) playerY++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (playerX > 0) playerX--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (playerX < WIDTH - 1) playerX++;
                        break;
                    case ConsoleKey.Escape:
                        return; // Quitter le jeu
                }

                // Affichage de l'interface à chaque mouvement
                Console.Clear();
            }
        }

        // Méthode pour afficher le terrain avec les maisons, plantes, et le joueur
        static void DisplayTerrain()
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    // Affichage du terrain (case vide ou maison)
                    if (x == 0 && y == 0)
                    {
                        SetColor(ConsoleColor.Green);
                        Console.Write("[Home]"); // Maison "Home"
                    }
                    else if (x == 5 && y == 0)
                    {
                        SetColor(ConsoleColor.Yellow);
                        Console.Write("[Cabanon]"); // Maison "Cabanon"
                    }
                    else if (x == 10 && y == 0)
                    {
                        SetColor(ConsoleColor.Cyan);
                        Console.Write("[Grange]"); // Maison "Grange"
                    }
                    else if (x == playerX && y == playerY)
                    {
                        SetColor(ConsoleColor.Red);
                        Console.Write("[Joueur]"); // Le joueur représenté par "P"
                    }
                    else if ((x + y) % 2 == 0)
                    {
                        SetColor(ConsoleColor.Green);
                        Console.Write("[ ]"); // Terrain de culture (cases vertes)
                    }
                    else
                    {
                        SetColor(ConsoleColor.DarkGreen);
                        Console.Write("[ ]"); // Autres terrains
                    }
                }
                Console.WriteLine();
            }

            // Instructions
            Console.SetCursorPosition(0, HEIGHT + 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Utilise les flèches pour te déplacer.");
            Console.WriteLine("Appuie sur ESC pour quitter.");
        }

        // Méthode pour changer la couleur de texte dans la console
        static void SetColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }
