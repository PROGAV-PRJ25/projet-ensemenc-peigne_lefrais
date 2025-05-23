using Potager.Models;
public static class AffichageAccueil
{
    public static void Afficher()
    {
        Console.Clear();
        Console.WriteLine(@"


            ______  _                                                                   _                    _       _                            
            | ___ \(_)                                                                 | |                  (_)     (_)                           
            | |_/ / _   ___  _ __  __   __  ___  _ __   _   _   ___      ___  _ __     | |      ___   _   _  _  ___  _   __ _  _ __   _ __    ___ 
            | ___ \| | / _ \| '_ \ \ \ / / / _ \| '_ \ | | | | / _ \    / _ \| '_ \    | |     / _ \ | | | || |/ __|| | / _` || '_ \ | '_ \  / _ \
            | |_/ /| ||  __/| | | | \ V / |  __/| | | || |_| ||  __/   |  __/| | | |   | |____| (_) || |_| || |\__ \| || (_| || | | || | | ||  __/
            \____/ |_| \___||_| |_|  \_/   \___||_| |_| \__,_| \___|    \___||_| |_|   \_____/ \___/  \__,_||_||___/|_| \__,_||_| |_||_| |_| \___|
                                                                                                                                  
                                                                                                                                
                                                      __  _                   _                             _          _                                   __  
                                                     / / | |     ___   _  _  (_)  ___  ___   _ _      ___  | |_     _ | |  ___   __ _   _ _    _ _    ___  \ \ 
                                                    | |  | |__  / _ \ | || | | | (_-< / _ \ | ' \    / -_) |  _|   | || | / -_) / _` | | ' \  | ' \  / -_)  | |
                                                    | |  |____| \___/  \_,_| |_| /__/ \___/ |_||_|   \___|  \__|    \__/  \___| \__,_| |_||_| |_||_| \___|  | |
                                                     \_\                                                                                                   /_/                           
                                                                                                                                                                                       
        ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"                                                                                                               
                                                                                                        ..-++            
                                                                                                        :=+-           
                                                                                                        .=.           
                                                                                                    *+++*..            
                                                                                                    .===*:..===*+.    
                                                                                                        .-=++++.     
                                                                                                .+=====-.  ..:-..       
                                                                                                ..+++*====.. ...++++++. 
                                                                                                .++++*==..=+++*===.  
                                                                                                    .....=++=====.    
                                                                            ++-...                   .+*+:-..:..       
                                                                            .==*+++.   .-====.         :=-+            
                                                                            .====++..===+++-             +.           
                                                                ...            ......=+++++.              *.           
                                            .:                .++=.                =....                 *.           
                                            =..              .=+                   *                  .#####.         
                                            ###*.             =###..             .######.           ..*#########:.     
                                        ..*##########..    ..#########*#..   ..-#############.  ..####################= 
                                            :.    :           .:    .:            .:     ..             :.       :.    
                                                                                                                                                                                                                                            
        


        
        ");
        Console.ResetColor();
        Console.WriteLine("Appuyez sur une touche pour entrer dans le jeu...");
        Console.WriteLine();
        Console.ReadKey();
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"
                 _____             _    _                      _                         _                             
                |  __ \           | |  (_)                    | |                       | |                            
                | |  \/  ___  ___ | |_  _   ___   _ __      __| |  ___     _ __    ___  | |_   __ _   __ _   ___  _ __ 
                | | __  / _ \/ __|| __|| | / _ \ |  _ \    / _  | / _ \   |  _ \  / _ \ | __| / _  | / _  | / _ \|  __|
                | |_\ \|  __/\__ \| |_ | || (_) || | | |  | (_| ||  __/   | |_) || (_) || |_ | (_| || (_| ||  __/| |   
                 \____/ \___||___/ \__||_| \___/ |_| |_|   \__,_| \___|   |  __/  \___/  \__| \__ _| \__, | \___||_|   
                                                                          | |                         __/ |            
                                                                          |_|                        |___/     
                        
                        
                        ");

        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Ce jeu est un simulateur de potager. Vous pouvez planter des végétaux utiles à la confection de cocktails,\net effectuer diverses actions dans le potager pour les maintenir en vie.");
        Console.WriteLine("Attention aux maladies qui peuvent surgir aléatoirement et ainsi dégrader la qualité de vos plantes...\n");
        Console.ResetColor();

        Console.WriteLine("➡️ Utilisez les flèches du clavier pour vous déplacer.");
        Console.WriteLine("➡️ Appuyez sur Entrer pour interagir avec un bâtiment :");
        Console.WriteLine("   - 🏠 Home    : dormir (une semaine passe)");
        Console.WriteLine("   - 🛠️ Cabanon : agir dans le potager");
        Console.WriteLine("   - 🏚️ Grange  : voir l'inventaire des plantes et vos récoltes\n");

        Console.WriteLine("➡️ Appuyez sur Entrer une fois sur un terrain pour l'afficher en détails.");
        Console.WriteLine("➡️ Appuyez sur Échap pour quitter le jeu.\n");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Appuyez sur une touche pour commencer...");

        Console.ResetColor();
        Console.ReadKey();
    }
}