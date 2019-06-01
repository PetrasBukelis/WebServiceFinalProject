using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMenu
{
    class Automobilis
    {
        private int ID;
        private string modelis;
        private int metai;
        private int rida;
        private double kaina;
        private string papildomaInfo;

        public Automobilis(int ID, string modelis, int metai, int rida, double kaina, string papildomaInfo)
        {
            this.ID = ID;
            this.modelis = modelis;
            this.metai = metai;
            this.rida = rida;
            this.kaina = kaina;
            this.papildomaInfo = papildomaInfo;
        }

        public int GetID()
        {
            return ID;
        }

        public string GetModelis()
        {
            return modelis;
        }

        public int GetMetai()
        {
            return metai;
        }

        public int GetRida()
        {
            return rida;
        }

        public double GetKaina()
        {
            return kaina;
        }

        public string GetInfo()
        {
            return papildomaInfo;
        }
    }
}
