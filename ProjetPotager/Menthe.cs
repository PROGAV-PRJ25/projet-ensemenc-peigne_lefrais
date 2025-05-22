public class Menthe : Plante
{
    public Menthe() : base("Menthe", "Vivace", "Avril", "Sol bien drainé, humide", TypeTerrain.DraineHumide,
                   35, 50, "Rapide (quelques jours)", 1000, 70, 20, "Rouille, mildiou", 4, 15) {}

    public override string AffichageSymbole() => "MEN";
    public override bool PeutEtreRecoltee() => CroissanceActuelle >= 2;
}