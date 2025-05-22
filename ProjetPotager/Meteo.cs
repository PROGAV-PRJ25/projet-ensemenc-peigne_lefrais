using Potager.Models;
public class Meteo
{
    public double Ensoleillement { get; private set; } // en heures par jour
    public double Pluie { get; private set; }          // en mm
    public double Temperature { get; private set; }     // en °C

    private static Random rand = new Random();

    public Meteo()
    {
        GenererConditions();
    }

    public void GenererConditions()
    {
        // On génère plus de beau temps que de mauvais :
        // 70% de chance d'avoir du soleil, 30% mauvais temps

        bool beauTemps = rand.NextDouble() < 0.7;

        if (beauTemps)
        {
            Ensoleillement = rand.Next(7, 13); // 7 à 12 h de soleil
            Pluie = rand.Next(0, 5);           // très peu de pluie
        }
        else
        {
            Ensoleillement = rand.Next(2, 7);  // 2 à 6 h
            Pluie = rand.Next(5, 21);          // 5 à 20 mm de pluie
        }

        Temperature = rand.Next(10, 36);        // de 10 à 35 °C
    }

    public override string ToString()
    {
        return $"Météo du jour : {Ensoleillement}h de soleil, {Pluie}mm de pluie, {Temperature}°C.";
    }
}