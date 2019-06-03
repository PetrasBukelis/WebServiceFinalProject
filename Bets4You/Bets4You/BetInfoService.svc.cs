using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Bets4You
{
    public class BetInfoService : IBetInfoService
    {
        private List<Bets> bets = new List<Bets>();
        private List<ComboCoef> coefficients = new List<ComboCoef>();
        private string result;

        public string AllBets()
        {
            FillData();
            foreach(Bets b in bets)
            {
                result += "Id: " + b.Id + "; BetName: " + b.BetName + "; Category: " + b.Category + "; Coefficient: " + b.Coefficient + "; Date: " + b.Date + "; SubmitOrName: " + b.SubmitOrName + ";";
            }
            return result;
        }

        public string AddBets(int id, string betName, string category,double coefficient,string submitorname, string password)
        {
            if (password == "PasswordSubmit")
            {
                DateTime date = DateTime.Today;
                InsertData(id, betName, category, coefficient, date.ToString(), submitorname);
                result = "Id: " + id + "; BetName: " + betName + "; Category: " + category + "; Coefficient: " + coefficient + "; Date: " + date + "; SubmitOrName: " + submitorname + ";";
                return result;

            }
            else
                return "Password incorrect";
        }

        public string SubmitCoff(int bet1, int bet2, int bet3, int bet4, int bet5, string submitorname, string password)
        {
            if (password == "PasswordSubmit")
            {
                double combocoef = 1;
                foreach (Bets b in bets)
                {
                    if(b.Id == bet1 || b.Id == bet2 || b.Id == bet3 || b.Id == bet4 || b.Id == bet5)
                    {
                        combocoef = combocoef * b.Coefficient;
                    }
                }
                List<int> indexes = new List<int>();
                indexes.Add(bet1);
                indexes.Add(bet2);
                indexes.Add(bet3);
                indexes.Add(bet4);
                indexes.Add(bet5);
                ComboCoef combo = new ComboCoef(indexes, combocoef, submitorname);
                coefficients.Add(combo);
                return combo.SubmitorName + " " + combo.SumCoeff;
            }
            else
                return "Password incorrect";
           
        }

        public void InsertData(int id, string BetName,string Category,double Coefficient,string date, string submitorname)
        {

            string connString = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            string idstring = id.ToString();
            string coefficient = Coefficient.ToString();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "Insert into Bets (BetName,Category,date,coefficient,submitorname) values" +
                " ("+ BetName + ", " + Category + ", " + date + ", " + coefficient + ", " + submitorname+")";
            command.ExecuteNonQuery();
            conn.Close();
        }

        public void FillData()
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select * from Bets";
                SqlDataReader rd = command.ExecuteReader();
                while (rd.Read())
                {
                    Bets bet = new Bets(Convert.ToInt32(rd[0].ToString().TrimEnd()), rd[1].ToString().TrimEnd(), rd[2].ToString().TrimEnd(), Convert.ToDateTime(rd[3].ToString().TrimEnd()), Convert.ToDouble(rd[4].ToString().TrimEnd()), rd[5].ToString().TrimEnd());
                    if(bet.Date < DateTime.Today)
                    {
                        bets.Add(bet);
                    }
                }
                conn.Close();
            }
            catch (SqlException e) { e.ToString(); }
        }
    }
}
