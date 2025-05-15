public class JeuController
{
    private Terrain terrain;
    private List<Plante> plantes;
    private Meteo meteo;
    private ModeJeu mode;

    public JeuController()
    {
        // Initialisation
        mode = ChoisirMode();
        terrain = new TerrainSableuxAvecEau();  // ou via une fabrique de terrains
        meteo = Meteo.GenererAleatoire();
        plantes = new List<Plante>
        {
            new Menthe(),
            new CanneASucre()
        };

        foreach (var p in plantes)
            terrain.AjouterPlante(p);
    }

    public void LancerPartie()
    {
        bool partieEnCours = true;

        while (partieEnCours)
        {
            Console.Clear();
            Affichage.AfficherTerrain(terrain);
            Console.WriteLine(meteo);
            Console.WriteLine("Appuie sur Espace pour effectuer une action, ESC pour quitter.");

            if (Console.KeyAvailable)
            {
                var touche = Console.ReadKey(true).Key;

                switch (touche)
                {
                    case ConsoleKey.Spacebar:
                        GererTour();
                        break;
                    case ConsoleKey.Escape:
                        partieEnCours = false;
                        break;
                }
            }

            Thread.Sleep(500); // simulation du temps qui passe
            meteo = Meteo.GenererAleatoire();
        }

        Console.WriteLine("Fin de partie !");
    }

    private void GererTour()
    {
        if (mode == ModeJeu.Classique)
        {
            // Affiche un menu : arroser, semer, récolter, observer, etc.
        }
        else if (mode == ModeJeu.Urgence)
        {
            // Gérer une urgence : intrus, tempête, etc.
        }

        foreach (var plante in terrain.Plantes)
            plante.Croître(meteo.Ensoleillement, meteo.Pluie, meteo.Temperature);
    }

    private ModeJeu ChoisirMode()
    {
        Console.WriteLine("Choisis le mode de jeu :");
        Console.WriteLine("1. Classique");
        Console.WriteLine("2. Urgence");

        var touche = Console.ReadKey(true).Key;
        return touche == ConsoleKey.D2 ? ModeJeu.Urgence : ModeJeu.Classique;
    }
}
