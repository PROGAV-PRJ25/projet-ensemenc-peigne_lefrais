public enum TypeTerrain
{
    SableuxAvecEau,
    DrainéHumide,
    DrainéFertile,
    BordDeMer,
    Calcaire,
    SableuxDrainé
}

public class Terrain
{
    public string Type { get; set; }
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
        bool typeOK = plante.TerrainPref == this.Type;

        bool humiditeOK = this.Humidite >= (1 - TOLERANCE) * plante.BesoinEau &&
                          this.Humidite <= (1 + TOLERANCE) * plante.BesoinEau;

        bool luminositeOK = this.Luminosite >= (1 - TOLERANCE) * plante.BesoinLum &&
                            this.Luminosite <= (1 + TOLERANCE) * plante.BesoinLum;

        bool temperatureOK = this.Temperature >= (1 - TOLERANCE) * plante.TemperaturePref &&
                             this.Temperature <= (1 + TOLERANCE) * plante.TemperaturePref;

        return typeOK && humiditeOK && luminositeOK && temperatureOK;
    }

    private bool TypeCorrespond(string terrainPref)
    {
        return (Type == TypeTerrain.SableuxAvecEau && terrainPref.Contains("source d'eau")) ||
               (Type == TypeTerrain.DrainéHumide && terrainPref.Contains("bien drainé, humide")) ||
               (Type == TypeTerrain.DrainéFertile && terrainPref.Contains("drainé, fertile")) ||
               (Type == TypeTerrain.BordDeMer && terrainPref.Contains("bord de mer")) ||
               (Type == TypeTerrain.Calcaire && terrainPref.Contains("calcaire")) ||
               (Type == TypeTerrain.SableuxDrainé && terrainPref.Contains("sableux, bien drainé"));
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
}
