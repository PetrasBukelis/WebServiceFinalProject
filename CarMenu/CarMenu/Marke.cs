using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMenu
{
    class Marke
    {
        private int ID;
        private string pavadinimas;
        private List<Automobilis> automobiliai;

        public Marke(int ID, string pavadinimas)
        {
            this.ID = ID;
            this.pavadinimas = pavadinimas;
            automobiliai = new List<Automobilis>();
        }

        public int GetID()
        {
            return ID;
        }

        public string GetPavadinimas()
        {
            return pavadinimas;
        }

        public void AddAuto(Automobilis auto)
        {
            automobiliai.Add(auto);
        }

        public List<Automobilis> GetAllAutos()
        {
            return automobiliai;
        }
    }
}
