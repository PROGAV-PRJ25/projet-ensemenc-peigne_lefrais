public class PlantePlacee
{
    public Plante Plante { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public PlantePlacee(Plante plante, int x, int y)
    {
        Plante = plante;
        X = x;
        Y = y;
    }
}
