using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bets4You
{
    public class ComboCoef
    {
        private List<int> BetIndexes;
        private double SumCoef;
        private string submitorName;

        public ComboCoef(List<int> betindex, double sumcoef, string submitorName)
        {
            this.BetIndexes = betindex;
            this.SumCoef = sumcoef;
            this.submitorName = submitorName;
        }
        public List<int> BetIndex { get => BetIndexes; set => BetIndexes = value; }
        public double SumCoeff { get => SumCoef; set => SumCoef = value; }
        public string SubmitorName { get => submitorName; set => submitorName = value; }
    }
}