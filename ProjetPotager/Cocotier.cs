using Potager.Models;
public class Cocotier : Plante
{
    public Cocotier() : base("Cocotier", "Vivace", "Mars", "Bord de mer, sable pauvre, soleil, vent salé", TypeTerrain.SableuxAvecEau,
                   80, 750, "Très lente (7–10 ans)", 1460, 90, 30, "Bipolaris", 100, 35)
    { }

    public override string AffichageSymbole() => "COC";
    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 30;
}