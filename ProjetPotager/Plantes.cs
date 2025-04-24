public class Plante
{
    public string Nature { get; set; }
    public string SaisonSemi { get; set; }
    public string TerrainPref { get; set; }
    public double Espacement { get; set; }
    public double PlaceNecessaire { get; set; }
    public string VitesseCroissance { get; set; }
    public double BesoinEau { get; set; }
    public double BesoinLum { get; set; }
    public double TemperaturePref { get; set; }
    public string Maladie { get; set; }
    public double EspDeVie { get; set; }
    public double FruitsRecoltes { get; set; } 

    public Plante (string nature, string saisonSemi, string terrainPref, double espacement, double placeNecessaire, string vitesseCroissance, double besoinEau, double besoinLum, double temperaturePref, string maladie, double espDeVie, double fruitsRecoltes)
    {
        Nature = nature;
        SaisonSemi = saisonSemi;
        TerrainPref = terrainPref;
        Espacement = espacement;
        PlaceNecessaire = placeNecessaire;
        vitesseCroissance = VitesseCroissance;
        BesoinEau = besoinEau;
        BesoinLum = besoinLum;
        TemperaturePref = temperaturePref;
        Maladie = maladie;
        EspDeVie = espDeVie;
        FruitsRecoltes = fruitsRecoltes;
    }
}