using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class Connect_SQL
    {
        public static DataTable Query(string str, params SqlParameter[] sp)
        {
            SetupConnection.connection.Open();
            SqlDataAdapter da = null;
            SqlCommand sc = null;
            if (str.Contains(" "))
            {
                da = new SqlDataAdapter(str, SetupConnection.connection);
            }
            else
            {
                sc = new SqlCommand(str, SetupConnection.connection);
                sc.CommandType = CommandType.StoredProcedure;
                if (sp.Length > 0)
                    foreach (SqlParameter so in sp)
                    {
                        sc.Parameters.Add(so);
                    }
                da = new SqlDataAdapter(sc);
            }
            DataTable dt = new DataTable();
            da.Fill(dt);
            SetupConnection.connection.Close();
            return dt;
        }
        public static void NonQuery(string str, params SqlParameter[] sp)
        {
            SetupConnection.connection.Open();
            SqlCommand sc = new SqlCommand(str, SetupConnection.connection);
            if (str.Contains(" "))
            {
                sc.CommandType = CommandType.Text;
            }
            else
            {
                sc.CommandType = CommandType.StoredProcedure;
                if (sp.Length > 0)
                    foreach (SqlParameter so in sp)
                    {
                        sc.Parameters.Add(so);
                    }
            }
            sc.ExecuteNonQuery();
            SetupConnection.connection.Close();
        }
    }
}