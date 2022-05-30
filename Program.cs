// Application console de gestion des stocks dont l'operateur est capade de : consulter les produits disponibles, décrémenter les stocks,mettre a jour l’état du stock. 


using System;
using Generateurs;

namespace MyApp
{
    public enum Etat
    {
        DISPONIBLE,
        RUPTURE,
        REAPPROVISIONNEMENT
    }
    class Program
    {     
        private const int espaceNomProduit = 56;
        private const int espaceStock = 14;
        private const int espaceEtat = 30;

        static void Main(String[] args)
        {
            int n = 1;
            List<Generateur> produits = loadGenerateurIntoList("./produits.txt");
            do{
                affichage(produits);

                // l'operateur saisie un numero
                var la = Console.ReadKey();
                
                int GenerateurIdToUpdateStock;
                bool temoin = int.TryParse(la.KeyChar.ToString(), out GenerateurIdToUpdateStock);
                if (temoin == true) {
                    
                    foreach(var p in produits)
                    {
                        if(p.Id == GenerateurIdToUpdateStock)
                        {
                            p.Stock--;
                            if (p.Stock <= 0) p.Stock = 0;
                            p.Etat = etatExact(p.Stock);
                        }
                    }
                    if (GenerateurIdToUpdateStock == 0)
                    {
                        Environment.Exit(0);
                    }
                } 
                Console.Clear();
            } while(n != 0);
        } 

        
        static List<int> registerAvailableGenerateurIds(List<Generateur> Generateur)
        {
            var ids = new List<int>();

            foreach (var item in Generateur)
            {
                ids.Add(item.Id);
            }
            return ids;
        }

        
        static List<Generateur> loadGenerateurIntoList(string filePath)
        {
            var lineGenerateurArray = File.ReadAllLines(filePath).ToList();
            var Generateur = new List<Generateur>();

            int id;
            int stock;
            string name;
            Etat etat;
            string[] GenerateurDataArray;

            foreach( var line in lineGenerateurArray)
            {
                GenerateurDataArray = line.Split("/");

                int.TryParse(GenerateurDataArray[0], out id);
                name = GenerateurDataArray[1];

                int.TryParse(GenerateurDataArray[2], out stock);
                etat = etatExact(stock);

                Generateur p = new Generateur(id, name, stock, etat);
                Generateur.Add(p);
            }
            return Generateur;
        }
        
        
        static string matchEtatToString(Etat etat)
        {
            string state;

            switch (etat)
            {
                case Etat.DISPONIBLE:
                    state = "Disponible";
                break;
                case Etat.RUPTURE:
                    state = "Rupture";
                break;
                case Etat.REAPPROVISIONNEMENT:
                    state = "Réapprovisionnement";
                break;
                default: state = "Rupture";
                break;
            }
            return state;
        }

        
        static Etat etatExact(int quantite)
        {
            Etat state = Etat.RUPTURE;
            
            if (quantite > 5) state = Etat.DISPONIBLE;
            if (quantite <= 5) state = Etat.REAPPROVISIONNEMENT;
            if (quantite <= 0) state = Etat.RUPTURE;
            
            return state;
        }

        // affichage des produits 
        static void affichage(List<Generateur> produits)
        {
            Console.WriteLine("\n");
            Console.WriteLine("+---+--------------------------------------------+------------+-------------------------+");
            Console.WriteLine("| # | Produit(s)                                 |  Stock(s)  |           Etat          |");
            Console.WriteLine("+---+--------------------------------------------+------------+-------------------------+");

            foreach (var item in produits)
            {
                string id = "";
                if(item.Id<10) {
                    id ="| "+item.Id+" | ";
                }else {
                    id ="|"+item.Id+" | ";
                }
                Console.Write(id);  
                
                Console.Write(item.Name);
                afficherEspaceNFois(espaceNomProduit - item.Name.Length - 1);
                Console.Write("| ");
                
                string qteStr = item.Stock.ToString();
                Console.Write(qteStr+" ");
                afficherEspaceNFois(espaceStock - qteStr.Length);
                Console.Write("| ");
                string etatStock = matchEtatToString(item.Etat);
                Console.Write(etatStock);
                afficherEspaceNFois(espaceEtat - etatStock.Length);
                Console.Write("|\n");
            }
            Console.WriteLine("+---+--------------------------------------------+------------+-------------------------+");
        }

       
        static void afficherEspaceNFois(int n)
        {
            for (int i = 0; i<n; i++) Console.Write(" ");
        }
	}
}