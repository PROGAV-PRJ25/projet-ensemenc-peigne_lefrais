using Potager.Models;
public class Cocotier : Plante
{
    public Cocotier() : base("Cocotier", "Fruit", "Été", TypeTerrain.BordDeMer,
        75, 10, "Lente", 80, 11, 30, 4, 3.0) { }  // 3 m²

    public override string AffichageSymbole() => "🥥";

    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 50;
}