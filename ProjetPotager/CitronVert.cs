using Potager.Models;
public class CitronVert : Plante
{
    public CitronVert() : base("Citron vert", "Vivace", "Mai", "Drainé, fertile", TypeTerrain.DraineFertile,
                   400, 1700, "Lente (45 cm/an)", 1000, 80, 25, "Gommose, mildiou", 40, 150)
    { }

    public override string AffichageSymbole() => "CIV";
    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 15;
}