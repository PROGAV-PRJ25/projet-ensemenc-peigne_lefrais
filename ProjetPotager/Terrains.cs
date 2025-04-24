public class Terrain {
    public string Type{get;set;}
    public double Humidite{get;set;}
    public double Luminosite{get;set;}

    public Terrain(){
        
    }


//vérifie si le terrain est adapté à une plante donnée
    public bool EstAdapté(Plante plante){
        return plante.TerrainPref==this.Type;
    }


    // Ajoute une plante si le terrain est adapté
    public bool AjouterPlante(Plante plante)
    {
        if (EstAdapté(plante))
        {
            Plantes.Add(plante);
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"Terrain : {Type}\n" +
               $"Humidité : {Humidite * 100}%\n" +
               $"Luminosité : {Luminosite} h/jour\n" +
               $"Plantes présentes : {Plantes.Count}";
    }
}