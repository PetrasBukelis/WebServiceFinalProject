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
                result += "Id: " + b.Id + "; BetName: " + b.BetName + "; Category: " + b.Category + "; Coefficient: " + b.Coefficient + "; Date: " + b.Date + "; SubmitOrName: " + b.SubmitOrName + ";";
            }
            return result;
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
                    bets.Add(bet);
                }
                conn.Close();
            }
            catch (SqlException e) { e.ToString(); }
        }
    }
}
