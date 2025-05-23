using Potager.Models;
public class Ananas : Plante
{
    public Ananas() : base("Ananas", "Fruit", "Été", TypeTerrain.SableuxDraine,
        70, 8, "Moyenne", 70, 8, 27, "Pourriture", 3, 1.8) { }  // 1.8 m²


    public override string AffichageSymbole() => "🍍";

    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 10;
}