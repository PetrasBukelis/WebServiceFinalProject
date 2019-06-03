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
        private string result;

        public string AllBets()
        {
            FillData();
            foreach(Bets b in bets)
            {
                result += "Id: " + b.Id + "; BetName: " + b.BetName + "; Category: " + b.Category + "; Date: " + b.Date + "; Coefficient (%): " + b.Coefficient + "; SubmitorName: " + b.SubmitorName + ";";
            }
            return result;
        }

        public string AddBets(string betName, string category, int coefficient, string submitorName)
        {
            DateTime date = DateTime.Today;
            InsertData(betName, category, date, coefficient, submitorName);
            FillData();
            foreach (Bets b in bets)
            {
                result += "Id: " + b.Id + "; BetName: " + b.BetName + "; Category: " + b.Category + "; Date: " + b.Date + "; Coefficient (%): " + b.Coefficient + "; SubmitorName: " + b.SubmitorName + ";";
            }
            return result;
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
