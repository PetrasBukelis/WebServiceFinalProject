using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;
namespace Bets4You
{
    public class BetInfoService : IBetInfoService
    {
        private static List<Bets> bets = new List<Bets>();
        private static List<ComboCoef> coefficients = new List<ComboCoef>();
        private string result;

        public string AllBets()
        {
            bets.Clear();
            FillData();
            foreach(Bets b in bets)
            {
                result += "Id: " + b.Id + "; BetName: " + b.BetName + "; Category: " + b.Category + "; Date: " + b.Date + "; Coefficient (%): " + b.Coefficient + "; SubmitorName: " + b.SubmitorName + ";";
            }
            return result + '\n';
        }

        public string AddBets(string betName, string category, int coefficient, string submitorName, string password, DateTime date)
        {
            bets.Clear();
            if (password == "PasswordSubmit")
            {
                InsertData(betName, category, date, coefficient, submitorName);
                FillData();
                foreach (Bets b in bets)
                {
                    result += "Id: " + b.Id + "; BetName: " + b.BetName + "; Category: " + b.Category + "; Date: " + b.Date + "; Coefficient (%): " + b.Coefficient + "; SubmitorName: " + b.SubmitorName + ";";
                } 
                return result;
            }
            else
                return "Password incorrect";
        }
       

        public string AllCoff()
        {
            string temp = "";
            foreach(ComboCoef combo in coefficients)
            {
                StringBuilder sb = new StringBuilder();
                foreach(int c in combo.BetIndex)
                {
                    sb.Append(c + ",");
                }
                temp += "Index of bets: " + sb + " Submitor: " + combo.SubmitorName + " Coefficient: " + combo.SumCoeff + "\n";
            }
            return temp;
        }
        public string CalcBet(int betId, int money, string reg)
        {
            string temp = "";
            foreach (Bets bet in bets)
            {
                if(bet.Id == betId)
                {
                    RegionInfo regionInfo = new RegionInfo(reg);
                    temp = "Index of bet: " + betId + " possible winning: " + bet.Coefficient*money/100 + " " + regionInfo.CurrencySymbol;
                }
            }
            return temp;
        }

        public string BestCoff()
        {
            //UpToDate();
            string submitname = "";
            int maxcof = 0;
            foreach(ComboCoef combo in coefficients)
            {
                if(combo.SumCoeff > maxcof)
                {
                    maxcof = combo.SumCoeff;
                    submitname = combo.SubmitorName;
                }
            }
            return submitname + " " + maxcof;
        }

        private void UpToDate()
        {
            foreach(ComboCoef combo in coefficients)
            {
                foreach(int b in combo.BetIndex)
                {
                    if(bets[b].Date < DateTime.Today)
                    {
                        bets.Remove(bets[b]);
                        coefficients.Remove(combo);
                        break;
                    }
                }
            }
        }

        public string SubmitCoff(int bet1, int bet2, int bet3, int bet4, int bet5, string submitorName, string password)
        {
            if (password == "PasswordSubmit")
            {
                int combocoef = 1;
                foreach (Bets b in bets)
                {
                    if (b.Id == bet1 || b.Id == bet2 || b.Id == bet3 || b.Id == bet4 || b.Id == bet5)
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
                ComboCoef combo = new ComboCoef(indexes, combocoef, submitorName);
                coefficients.Add(combo);
                return combo.SubmitorName + " " + combo.SumCoeff;
            }
            else
                return "Password incorrect";

        }


        public void InsertData(string betName, string category, DateTime date, int coefficient, string submitorName)
        {
            string connString = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "insert into Bets (BetName, Category, Date, Coefficient, SubmitorName) values ('"+ betName + "', '" + category + "', '" + date + "', '" + coefficient + "', '" + submitorName + "')";
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
                    Bets bet = new Bets(Convert.ToInt32(rd["Id"].ToString().TrimEnd()), rd["BetName"].ToString().TrimEnd(), rd["Category"].ToString().TrimEnd(), Convert.ToDateTime(rd["Date"].ToString().TrimEnd()), Convert.ToInt32(rd["Coefficient"].ToString().TrimEnd()), rd["SubmitorName"].ToString().TrimEnd());
                    bets.Add(bet);
                }
                conn.Close();
            }
            catch (SqlException e) { e.ToString(); }
        }
        
    }
}
