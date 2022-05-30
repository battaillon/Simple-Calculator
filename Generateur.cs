using MyApp;

namespace Generateurs
{
     class Generateur
    {
        public int Id
        { set; get; }

        public string Name
        { get; set; }

        public int Stock
        { set; get; }

        public Etat Etat
        { set; get; }

        // Contructeur sans paramètre
        public Generateur()
        {
            Id = 0;
            Name = "";
            Stock = 0;
            Etat = Etat.RUPTURE;
        }

        // Contructeur avec paramètre(s)
        public Generateur(int id, string name, int stock, Etat etat)
        {
            Id = id;
            Name = name;
            Stock = stock;
            Etat = etat;
        }

        override public string ToString()
        {
            return "Id: " + Id + ", Name: " + Name + ", Stock: " + Stock + ", Etat: " +Etat;
        }
    }
}