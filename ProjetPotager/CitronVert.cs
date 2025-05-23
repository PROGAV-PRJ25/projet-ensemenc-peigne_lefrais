using Potager.Models;
public class CitronVert : Plante
{
    public CitronVert() : base("Citron vert", "Fruit", "Printemps", TerrainPref.DraineHumide,
        60, 6, "Moyenne", 90, 7, 26, "Pucerons", 3, 1.5) { }  // 1.5 m²

    public override string AffichageSymbole() => "🍋";

    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 30;
}