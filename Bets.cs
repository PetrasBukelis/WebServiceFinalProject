using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bets4You
{
    public class Bets
    {
        private int id;
        private string betName;
        private string category;
        private DateTime date;
        private double coefficient;
        private string submitOrName;

        public Bets(int id, string betName, string category, DateTime date, double coefficient, string submitOrName)
        {
            this.BetName = betName;
            this.category = category;
            this.date = date;
            this.coefficient = coefficient;
            this.submitOrName = submitOrName;
        }

        public int Id { get => id; set => id = value; }
        public string BetName { get => betName; set => betName = value; }
        public string Category { get => category; set => category = value; }
        public DateTime Date { get => date; set => date = value; }
        public double Coefficient { get => coefficient; set => coefficient = value; }
        public string SubmitOrName { get => submitOrName; set => submitOrName = value; }
    }
}