using Potager.Models;
public class Menthe : Plante
{
    public Menthe() : base("Menthe", "Herbe", "Printemps", TerrainPref.DraineHumide,
        65, 7, "Rapide", 60, 5, 25, "Oïdium", 2, 0.5) { }  // 0.5 m²

    public override string AffichageSymbole() => "🌿";

    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 2;
}