using Potager.Models;
class Program
{
    static void Main(string[] args)
    {
        // la liste des terrains
        List<Terrain> tousLesTerrains = new List<Terrain>
        {
            new Terrain
            {
                Type = TypeTerrain.SableuxAvecEau,
                Humidite = 0.8,
                Luminosite = 8,
                Temperature = 25,
                SurfaceTotale = 20
            },
            new Terrain
            {
                Type = TypeTerrain.DraineHumide,
                Humidite = 0.6,
                Luminosite = 7,
                Temperature = 22,
                SurfaceTotale = 18
            },
            new Terrain
            {
                Type = TypeTerrain.DraineFertile,
                Humidite = 0.5,
                Luminosite = 9,
                Temperature = 23,
                SurfaceTotale = 25
            },
            new Terrain
            {
                Type = TypeTerrain.BordDeMer,
                Humidite = 0.7,
                Luminosite = 10,
                Temperature = 27,
                SurfaceTotale = 15
            },
            new Terrain
            {
                Type = TypeTerrain.Calcaire,
                Humidite = 0.4,
                Luminosite = 6,
                Temperature = 20,
                SurfaceTotale = 12
            },
            new Terrain
            {
                Type = TypeTerrain.SableuxDraine,
                Humidite = 0.3,
                Luminosite = 9,
                Temperature = 26,
                SurfaceTotale = 16
            }
        };

        // instanciation de la classe Actions 
        Actions actions = new Actions(tousLesTerrains);

        // instanciation de l'affichage pour lancer la boucle de jeu
        Affichage affichage = new Affichage();

        // Lancer l'affichage 
        affichage.LancerPartie();
    }
}
