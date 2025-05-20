class Program
{
    static void Main(string[] args)
    {
        // Pour tester l'affichage uniquement :
        Affichage.LancerTestAffichage();

        // Pour lancer le jeu :
        var controller = new JeuController();
        controller.LancerPartie();  // ← boucle ici
    }
}
