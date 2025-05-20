public static class Actions
    {
        public static void PlanterUneGraine(List<Terrain> terrains)
        {
            Console.Clear();
            Console.WriteLine("Quelle plante souhaitez-vous planter ?");
            Console.WriteLine("1. Canne à sucre");
            Console.WriteLine("2. Menthe");
            Console.WriteLine("3. Citron vert");
            Console.WriteLine("4. Cocotier");
            Console.WriteLine("5. Cerisier");
            Console.WriteLine("6. Ananas");
            Console.Write("Votre choix : ");
            string choixPlante = Console.ReadLine();

            Plante plante = choixPlante switch
            {
                "1" => new CanneASucre(),
                "2" => new Menthe(),
                "3" => new CitronVert(),
                "4" => new Cocotier(),
                "5" => new Cerisier(),
                "6" => new Ananas(),
                _ => null
            };

            if (plante == null)
            {
                Console.WriteLine("Plante inconnue.");
                return;
            }

            Console.Clear();
            Console.WriteLine("Choisissez un terrain :");
            for (int i = 0; i < terrains.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {terrains[i]}");
            }

            Console.Write("Numéro du terrain : ");
            if (!int.TryParse(Console.ReadLine(), out int indexTerrain) || indexTerrain < 1 || indexTerrain > terrains.Count)
            {
                Console.WriteLine("Choix invalide.");
                return;
            }

            Terrain terrain = terrains[indexTerrain - 1];

            // Vérifier la compatibilité
            if (!terrain.EstAdapté(plante))
            {
                Console.WriteLine("⚠️ Attention : Le terrain n'est pas adapté à cette plante. Elle risque de ne pas survivre longtemps.");
                Console.Write("Souhaitez-vous planter malgré tout ? (o/n) : ");
                string reponse = Console.ReadLine()?.ToLower();
                if (reponse != "o")
                {
                    Console.WriteLine("Plantation annulée.");
                    return;
                }
            }

            // Vérifier la place restante
            if (terrain.SurfaceOccupée + plante.PlaceNecessaire > terrain.SurfaceTotale)
            {
                Console.WriteLine("❌ Il n'y a pas assez d'espace pour cette plante sur ce terrain.");
                return;
            }

            terrain.Plantes.Add(plante);
            Console.WriteLine($"✅ {plante.GetType().Name} plantée avec succès !");
        }
    }