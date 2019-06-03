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
        private int coefficient;
        private string submitorName;

        public Bets(int id, string betName, string category, DateTime date, int coefficient, string submitorName)
        {
            this.id = id;
            this.betName = betName;
            this.category = category;
            this.date = date;
            this.coefficient = coefficient;
            this.submitorName = submitorName;
        }

        public int Id { get => id; set => id = value; }
        public string BetName { get => betName; set => betName = value; }
        public string Category { get => category; set => category = value; }
        public DateTime Date { get => date; set => date = value; }
        public int Coefficient { get => coefficient; set => coefficient = value; }
        public string SubmitorName { get => submitorName; set => submitorName = value; }
    }
}