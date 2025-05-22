using Potager.Models;
public class Ananas : Plante
{
    public Ananas() : base("Ananas", "Vivace", "Juin", TerrainPref.SableuxDraine,
                   75, 100, "Lente (2 ans)", 900, 80, 26, "Fusariose", 5, 1)
    { }

    public override string AffichageSymbole() => "ANA";
    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 10;
}