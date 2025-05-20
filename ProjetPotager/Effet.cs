public abstract class Effet
{
    public string Nom { get; set; }

    // Méthode abstraite pour appliquer l'effet à une plante
    public abstract void AppliquerEffet(Plante plante);

    // Maladies
    public class Maladie : Effet
    {
        public Maladie(string nom)
        {
            Nom = nom;
        }

        public override void AppliquerEffet(Plante plante)
        {
            if (!string.IsNullOrEmpty(plante.Maladie))
            {
                // La plante est déjà malade, on peut éventuellement cumuler ou ignorer
                return;
            }
            plante.Maladie = Nom;
            plante.EspDeVie -= 1; // Diminue vie en fonction (ajuste si besoin)
            Console.WriteLine($"{plante.Nom} est maintenant infectée par {Nom}.");
        }
    }

    // Puceron : un obstacle vivant
    public class Puceron : Effet
    {
        public Puceron()
        {
            Nom = "Puceron";
        }

        public override void AppliquerEffet(Plante plante)
        {
            // Si la plante n'a pas déjà de pucerons, on ajoute
            if (!plante.Maladie.Contains("Puceron"))
            {
                plante.Maladie += (string.IsNullOrEmpty(plante.Maladie) ? "" : ", ") + "Puceron";
                plante.EspDeVie -= 0.5; // Un impact léger
                Console.WriteLine($"{plante.Nom} est infestée de pucerons.");
            }
        }
    }

    // Coccinelle : bonne fée qui nettoie les pucerons
    public class Coccinelle : Effet
    {
        public Coccinelle()
        {
            Nom = "Coccinelle";
        }

        public override void AppliquerEffet(Plante plante)
        {
            if (plante.Maladie.Contains("Puceron"))
            {
                // Retirer "Puceron" de la chaîne Maladie
                var maladies = plante.Maladie.Split(", ").ToList();
                maladies.Remove("Puceron");
                plante.Maladie = string.Join(", ", maladies);
                plante.EspDeVie += 0.5; // Plante soulagée
                Console.WriteLine($"{plante.Nom} est débarrassée des pucerons par la coccinelle.");
            }
            else
            {
                Console.WriteLine($"{plante.Nom} n'a pas de pucerons à combattre.");
            }
        }
    }

}