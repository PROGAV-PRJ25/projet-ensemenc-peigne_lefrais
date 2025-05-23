using Potager.Models;
public class Cerisier : Plante
{
    public Cerisier() : base("Cerisier", "Fruit", "Printemps", TypeTerrain.DraineFertile,
        55, 9, "Moyenne", 100, 9, 24, "Chancre", 4, 2.5) { }  // 2.5 m²

    public override string AffichageSymbole() => "🍒";

    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 40;
}