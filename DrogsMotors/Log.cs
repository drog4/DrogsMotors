using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DrogsMotors

{
    class Log
    {
        public int IdWrite { get; set; }
        public string LastNameUser { get; set; }
        public string Act { get; set; }
        public string TableAct { get; set; }
        public string DateAct { get; set; }

        public Log() { }
        public Log(int IdWrite, string LastNameUser, string Act, string TableAct, string DataAct)
        {
            this.IdWrite = IdWrite;
            this.LastNameUser = LastNameUser;
            this.Act = Act;
            this.TableAct = TableAct;
            this.DateAct = DataAct;
        }

        public static void AddLog(string LastNameUser, string Act, string TableAct, string DataAct)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            sql.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Logs]  ([LastNameUser],[Act],[TableAct],[DataAct])  VALUES (@LastName, @Act, @TableAct, @DataAct)", sql);
            command.Parameters.AddWithValue("@LastName", LastNameUser);
            command.Parameters.AddWithValue("@Act", Act);
            command.Parameters.AddWithValue("@TableAct", TableAct);
            command.Parameters.AddWithValue("@DataAct", DataAct);
            bool res = command.ExecuteNonQuery() == 1;
            sql.Close();
        }

        public static List<Log> GetAllLog()
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            List<Log> log = new List<Log>();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from Logs", sql);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    log.Add(new Log(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4)
                        ));
                }
            }
            sql.Close();
            return log;
        }
    }
}