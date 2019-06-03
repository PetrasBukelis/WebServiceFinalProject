﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bets4You
{
    public class ComboCoef
    {
        private List<int> betIndexes;
        private double sumCoef;
        private string submitorName;

        public ComboCoef(List<int> betindex, double sumcoef, string submitorName)
        {
            this.betIndexes = betindex;
            this.sumCoef = sumcoef;
            this.submitorName = submitorName;
        }
        public List<int> BetIndex { get => betIndexes; set => betIndexes = value; }
        public double SumCoeff { get => sumCoef; set => sumCoef = value; }
        public string SubmitorName { get => submitorName; set => submitorName = value; }
    }
}