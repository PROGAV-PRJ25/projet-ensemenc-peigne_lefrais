using Potager.Models;
public class CanneASucre : Plante
{
    public CanneASucre() : base("Canne à sucre", "Sucre", "Été", TypeTerrain.SableuxAvecEau,
        70, 8, "Moyenne", 120, 10, 28, "Rouille", 3, 2.0) { }  // 2 m²

    public override string AffichageSymbole() => "🌾";

    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 5;
}