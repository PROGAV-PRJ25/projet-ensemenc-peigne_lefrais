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

                                                                                
                                                                                                                        
                                                                                                                        
                                                                                                        ..             
                                                                                                        -++            
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
        Console.WriteLine("Appuyez sur une touche pour entrer dans le jeu...");
        Console.WriteLine();
        Console.ReadKey();
        Console.Clear();
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
        Console.WriteLine("🌱 Bienvenue dans le jeu du potager !\n");
        Console.WriteLine("Utilisez les flèches du clavier pour vous déplacer.\n");
        Console.WriteLine("Appuyez sur Entrer pour interagir avec un batiment ou un terrain : \n - 🏠 Home : rentrer dormir\n - 🛠️ Cabanon : pour agir dans le potager\n - 🏚️ Grange : pour voir l'inventaire des plantes, ainsi que vos récoltes\n");
        Console.WriteLine("Appuyez sur Échap pour quitter le jeu.\n\n");
        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();

    }
}