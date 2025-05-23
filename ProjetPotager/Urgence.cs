using System;
using System.Collections.Generic;

public class Urgence
{
    public Plante PlanteEnDanger { get; private set; }
    public bool EstActive { get; private set; }

    private static Random random = new Random();

    public void ActiverUrgence(List<Plante> plantes)
    {
        if (plantes == null || plantes.Count == 0)
            return;

        int index = random.Next(plantes.Count);
        PlanteEnDanger = plantes[index];
        EstActive = true;

        // Appliquer une maladie
        string[] maladiesPossibles = { "mildiou", "rouille", "gommose", "bipolaris", "fusariose" };
        PlanteEnDanger.Maladie = maladiesPossibles[random.Next(maladiesPossibles.Length)];
    }

    public void AfficherUrgence()
    {
        if (PlanteEnDanger != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n🚨 ALERTE URGENCE 🚨\nLa plante {PlanteEnDanger.Nom} est menacée par {PlanteEnDanger.Maladie} !");
            Console.ResetColor();
        }
    }

    public void SoignerPlante()
    {
        if (PlanteEnDanger != null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Vous avez soigné {PlanteEnDanger.Nom} de la maladie {PlanteEnDanger.Maladie} !");
            Console.ResetColor();

            PlanteEnDanger.Maladie = null;
            EstActive = false;
            PlanteEnDanger = null;
        }
    }
}
