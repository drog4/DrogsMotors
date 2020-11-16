using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DrogsMotors
{
    public class Clients
    {
        public int Id { get; set; }
        public string surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }


        public Clients()
        {
        }

        public Clients(int Id, string SurName, string Name, string Patronymic, string Address, string Phone)
        {
            this.Id = Id;
            this.surname = SurName;
            this.Name = Name;
            this.Patronymic = Patronymic;
            this.Address = Address;
            this.Phone = Phone;
        }

        public static void AddClient(string pt, string SurName, string Name, string Patronymic, string Address, string Phone)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            sql.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Clients]  ([Surname],[name],[patronymic],[City],[phone])  VALUES (@Surname,@Name,@Patronymic,@Address,@Phone)", sql);
            command.Parameters.AddWithValue("@Surname", SurName);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Patronymic", Patronymic);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            bool res = command.ExecuteNonQuery() == 1;
            sql.Close();
            Log.AddLog(pt, "Добавление клиента " + SurName, "Клиенты", DateTime.Now.ToString());
        }

        public static List<Clients> GetAllClients()
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            List<Clients> Client = new List<Clients>();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from [Clients]", sql);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Client.Add(new Clients(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5)
                       ));
                }
            }
            sql.Close();
            return Client;
        }

        public static void Update(string pt, Clients c)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            sql.Open();
            SqlCommand command = new SqlCommand("update [Clients] SET Surname = @Surname,name = @Name,patronymic = @Patronymic,City = @Address,phone= @Phone where id = @ID", sql);
            command.Parameters.AddWithValue("@Surname", c.surname);
            command.Parameters.AddWithValue("@Name", c.Name);
            command.Parameters.AddWithValue("@Patronymic", c.Patronymic);
            command.Parameters.AddWithValue("@Address", c.Address);
            command.Parameters.AddWithValue("@Phone", c.Phone);
            command.Parameters.AddWithValue("@ID", c.Id);
            bool res = command.ExecuteNonQuery() == 1;
            sql.Close();
            Log.AddLog(pt, "Измеение данных клиента " + c.surname, "Клиенты", DateTime.Now.ToString());
        }

        public static Clients GetClient(int id)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True"; ;
            SqlConnection sql = new SqlConnection(connectionStr);
            Clients u = new Clients();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from [Clients]  where id=@idu", sql);
            command.Parameters.AddWithValue("@Idu", id);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    u = new Clients(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5));

                }
            }
            sql.Close();
            return u;

        }

        public static Clients GetClient(string sn)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            Clients u = new Clients();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from [Clients]  where Surname=@srnm", sql);
            command.Parameters.AddWithValue("@srnm", sn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    u = new Clients(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5));

                }
            }
            sql.Close();
            return u;

        }

        public static void Delete(Clients u, string pt)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            sql.Open();
            using (SqlCommand command = new SqlCommand("delete [Clients] where id = '" + u.Id + "'", sql))
            {
                command.ExecuteNonQuery();
            }
            Log.AddLog(pt, " Удаление клиента " + u.surname, "Клиенты", DateTime.Now.ToString());
            sql.Close();
        }
    }
}
