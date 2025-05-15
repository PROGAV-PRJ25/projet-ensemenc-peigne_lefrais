class Affichage
{
    const int GRID_SIZE = 5; // Taille d'une grille (5x5 cases)
    const int TERRAIN_COLS = 3;
    const int TERRAIN_ROWS = 2;
    const int CELL_WIDTH = 3; // Largeur visuelle d'une cellule
    static int playerX = 0;
    static int playerY = 0;

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;
        Console.Clear();

        while (true)
        {
            DisplayWorld();

            // Déplacement du joueur
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (playerY > 0) playerY--;
                    break;
                case ConsoleKey.DownArrow:
                    if (playerY < (TERRAIN_ROWS * (GRID_SIZE + 2)) - 1) playerY++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (playerX > 0) playerX--;
                    break;
                case ConsoleKey.RightArrow:
                    if (playerX < (TERRAIN_COLS * (GRID_SIZE * CELL_WIDTH + 4)) - 1) playerX++;
                    break;
                case ConsoleKey.Escape:
                    return;
            }

            Console.Clear();
        }
    }

    static void DisplayWorld()
    {
        Console.ForegroundColor = ConsoleColor.White;

        // 🏠 Affichage des maisons centrées en haut
        Console.SetCursorPosition(0, 0);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" [Home]     [Cabanon]     [Grange]");

        // 🌱 Affichage des 6 terrains (2 lignes x 3 colonnes)
        for (int terrainIndex = 0; terrainIndex < 6; terrainIndex++)
        {
            int row = terrainIndex / TERRAIN_COLS;
            int col = terrainIndex % TERRAIN_COLS;

            int startX = col * (GRID_SIZE * CELL_WIDTH + 4);
            int startY = 2 + row * (GRID_SIZE + 3);

            DrawTerrain(startX, startY, terrainIndex + 1);
        }

        // 👤 Affichage du joueur
        Console.SetCursorPosition(playerX, playerY);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("P");

        // ℹ️ Instructions
        Console.SetCursorPosition(0, TERRAIN_ROWS * (GRID_SIZE + 3) + 2);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Déplace-toi avec les flèches directionnelles. ESC pour quitter.");
    }

    static void DrawTerrain(int offsetX, int offsetY, int terrainNumber)
    {
        for (int y = 0; y < GRID_SIZE; y++)
        {
            for (int x = 0; x < GRID_SIZE; x++)
            {
                int drawX = offsetX + x * CELL_WIDTH;
                int drawY = offsetY + y;

                Console.SetCursorPosition(drawX, drawY);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("[ ]");
            }
        }

        // Nom du terrain
        Console.SetCursorPosition(offsetX + (GRID_SIZE * CELL_WIDTH / 2) - 4, offsetY + GRID_SIZE);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"Terrain {terrainNumber}");
    }
}