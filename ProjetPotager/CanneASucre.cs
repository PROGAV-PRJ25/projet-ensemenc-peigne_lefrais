using Potager.Models;
public class CanneASucre : Plante
{
    public CanneASucre() : base("Canne à sucre", "Vivace", "Septembre", TerrainPref.SableuxAvecEau,
                   120, 20, "Lente (1 an)", 1500, 100, 16, "Mildiou, rouille, gommose", 10, 180)
    { }

    public override string AffichageSymbole() => "CQS";
    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 10;
}