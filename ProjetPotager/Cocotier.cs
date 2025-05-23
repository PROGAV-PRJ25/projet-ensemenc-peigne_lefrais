using Potager.Models;
public class Cocotier : Plante
{
    public Cocotier() : base("Cocotier", "Fruit", "Été", TerrainPref.BordDeMer,
        75, 10, "Lente", 80, 11, 30, "Charançon", 4, 3.0) { }  // 3 m²

    public override string AffichageSymbole() => "🥥";

    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 50;
}