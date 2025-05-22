namespace Potager.Models
{
    public enum TypeTerrain
    {
        SableuxAvecEau,
        DraineHumide,
        DraineFertile,
        BordDeMer,
        Calcaire,
        SableuxDraine
    }

    public class Terrain
    {
        public TypeTerrain Type { get; set; }
        public double Humidite { get; set; }       // entre 0 et 1
        public double Luminosite { get; set; }     // en heures/jour
        public double Temperature { get; set; }    // en °C
        public double SurfaceTotale { get; set; }  // en m²

        public List<Plante> Plantes { get; set; } = new List<Plante>();

        public double SurfaceOccupée => Plantes.Sum(p => p.PlaceNecessaire);

        private const double TOLERANCE = 0.2; // ±20% de marge autour des besoins de la plante

        public Terrain() { }

        public bool EstAdapté(Plante plante)
        {
            bool typeOK = TypeCorrespond(plante.TerrainPref);

            bool humiditeOK = this.Humidite >= (1 - TOLERANCE) * plante.BesoinEau &&
                              this.Humidite <= (1 + TOLERANCE) * plante.BesoinEau;

            bool luminositeOK = this.Luminosite >= (1 - TOLERANCE) * plante.BesoinLum &&
                                this.Luminosite <= (1 + TOLERANCE) * plante.BesoinLum;

            bool temperatureOK = this.Temperature >= (1 - TOLERANCE) * plante.TemperaturePref &&
                                 this.Temperature <= (1 + TOLERANCE) * plante.TemperaturePref;

            Console.WriteLine($"Type OK: {typeOK}, Humidité OK: {humiditeOK}, Luminosité OK: {luminositeOK}, Température OK: {temperatureOK}");

            return typeOK && humiditeOK && luminositeOK && temperatureOK;
        }

        // private bool TypeCorrespond(string terrainPref)
        // {
        //     return (Type == TypeTerrain.SableuxAvecEau && terrainPref.Contains("source d'eau")) ||
        //            (Type == TypeTerrain.DraineHumide && terrainPref.Contains("bien drainé, humide")) ||
        //            (Type == TypeTerrain.DraineFertile && terrainPref.Contains("drainé, fertile")) ||
        //            (Type == TypeTerrain.BordDeMer && terrainPref.Contains("bord de mer")) ||
        //            (Type == TypeTerrain.Calcaire && terrainPref.Contains("calcaire")) ||
        //            (Type == TypeTerrain.SableuxDraine && terrainPref.Contains("sableux, bien drainé"));
        // }

        private bool TypeCorrespond(TerrainPref terrainPref)
        {
            return Type.ToString() == terrainPref.ToString();
        }

        public bool AjouterPlante(Plante plante)
        {
            if (EstAdapté(plante) && (SurfaceOccupée + plante.PlaceNecessaire <= SurfaceTotale))
            {
                Plantes.Add(plante);
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Terrain : {Type}\n" +
                   $"Humidité : {Humidite * 100}%\n" +
                   $"Luminosité : {Luminosite} h/jour\n" +
                   $"Température moyenne : {Temperature}°C\n" +
                   $"Surface utilisée : {SurfaceOccupée} / {SurfaceTotale} m²\n" +
                   $"Plantes présentes : {Plantes.Count}";
        }

    public static List<Terrain> CreerTousLesTerrains()
    {
        return new List<Terrain>
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
    }
    }

}
