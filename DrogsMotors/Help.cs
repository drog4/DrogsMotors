
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace DrogsMotors
{
    class Help
    {
        static string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
        public static string LastName;
        public static string role;

        public static bool Log(string l, string p)
        {
            bool isAdmin = false;
            SqlConnection sql = new SqlConnection(connectionStr);
            sql.Open();
            SqlCommand command = new SqlCommand("(SELECT Login, Password, FirstName, SurName  FROM Admin)", sql);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string login = reader.GetValue(0).ToString();
                    string pwd = reader.GetValue(1).ToString();

                    if (l == login && PasswordHelper.VerifyPassword(p, pwd))
                    {
                        isAdmin = true;

                    }
                }

            }
            return isAdmin;
        }

        public static List<string> Name(string l, string p)
        {
            List<string> name = new List<string>();
            SqlConnection sql = new SqlConnection(connectionStr);
            sql.Open();
            SqlCommand command = new SqlCommand("(SELECT Login, Password, FirstName, SurName, LastName FROM Admin)", sql);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string login = reader.GetValue(0).ToString();
                    string pwd = reader.GetValue(1).ToString();
                    string n1 = reader.GetValue(2).ToString();
                    string n2 = reader.GetValue(3).ToString();
                    LastName = reader.GetValue(4).ToString();

                    if (l == login && PasswordHelper.VerifyPassword(p, pwd))
                    {
                        name.Add(n1);
                        name.Add(n2);
                        break;

                    }
                }

            }
            return name;
        }
    }
}
