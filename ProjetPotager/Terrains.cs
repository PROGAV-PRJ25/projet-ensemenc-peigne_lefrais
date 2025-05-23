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
        public double Humidite { get; set; }       // en pourcentage
        public double Luminosite { get; set; }     // en heures/jour
        public double Temperature { get; set; }    // en °C
        public double SurfaceTotale { get; set; }  // en m²

        public List<Plante> Plantes { get; set; } = new List<Plante>();

        public double SurfaceOccupee => Plantes.Sum(p => p.PlaceNecessaire);

        private const double TOLERANCE = 0.5; // ±50%

        public Terrain() { }

        public bool EstAdapté(Plante plante)
        {
            double besoinEauNorm = plante.BesoinEau;

            bool typeOK = this.Type == plante.TerrainPref;

            double humiditeMin = besoinEauNorm * (1 - TOLERANCE);
            double humiditeMax = besoinEauNorm * (1 + TOLERANCE);
            bool humiditeOK = this.Humidite >= humiditeMin && this.Humidite <= humiditeMax;

            double lumiereMin = plante.BesoinLum * (1 - TOLERANCE);
            double lumiereMax = plante.BesoinLum * (1 + TOLERANCE);
            bool lumiereOK = this.Luminosite >= lumiereMin && this.Luminosite <= lumiereMax;

            double temperatureMin = plante.TemperaturePref * (1 - TOLERANCE);
            double temperatureMax = plante.TemperaturePref * (1 + TOLERANCE);
            bool temperatureOK = this.Temperature >= temperatureMin && this.Temperature <= temperatureMax;

            return typeOK && humiditeOK && lumiereOK && temperatureOK;
        }



        public string AjouterPlante(Plante plante)
        {
            if (SurfaceOccupee + plante.PlaceNecessaire > SurfaceTotale)
                return "❌ Il n'y a pas assez d'espace pour cette plante sur ce terrain.";

            Plantes.Add(plante);
            return $"✅ {plante.Nom} plantée avec succès !";
        }

        public override string ToString()
        {
            return $"Terrain : {Type}\n" +
                   $"Humidité : {Humidite:F1}%\n" +
                   $"Luminosité : {Luminosite} h/jour\n" +
                   $"Température moyenne : {Temperature}°C\n" +
                   $"Surface utilisée : {SurfaceOccupee:F2} / {SurfaceTotale} m²\n" +
                   $"Plantes présentes : {Plantes.Count}";
        }

        public static List<Terrain> CreerTousLesTerrains()
        {
            return new List<Terrain>
            {
                new Terrain
                {
                    Type = TypeTerrain.SableuxAvecEau,
                    Humidite = 100,
                    Luminosite = 8,
                    Temperature = 26,
                    SurfaceTotale = 20
                },
                new Terrain
                {
                    Type = TypeTerrain.DraineHumide,
                    Humidite = 65,
                    Luminosite = 7.5,
                    Temperature = 23,
                    SurfaceTotale = 18
                },
                new Terrain
                {
                    Type = TypeTerrain.DraineFertile,
                    Humidite = 55,
                    Luminosite = 9,
                    Temperature = 21,
                    SurfaceTotale = 25
                },
                new Terrain
                {
                    Type = TypeTerrain.BordDeMer,
                    Humidite = 70,
                    Luminosite = 10,
                    Temperature = 28,
                    SurfaceTotale = 15
                },
                new Terrain
                {
                    Type = TypeTerrain.Calcaire,
                    Humidite = 45,
                    Luminosite = 6.5,
                    Temperature = 20,
                    SurfaceTotale = 12
                },
                new Terrain
                {
                    Type = TypeTerrain.SableuxDraine,
                    Humidite = 60,
                    Luminosite = 8.5,
                    Temperature = 24,
                    SurfaceTotale = 16
                }
            };
        }
    }
}
