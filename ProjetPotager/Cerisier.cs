using Potager.Models;
public class Cerisier : Plante
{
    public Cerisier() : base("Cerisier", "Vivace", "Février", "Terrains calcaires", TypeTerrain.Calcaire,
                   500, 1000, "Moyenne/rapide", 1300, 100, 20, "Champignons", 50, 300)
    { }

    public override string AffichageSymbole() => "CER";
    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 20;
}