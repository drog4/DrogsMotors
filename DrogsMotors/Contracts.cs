using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DrogsMotors
{
    public class Contracts
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string DataOpen { get; set; }
        public int IdCar { get; set; }
        public string Cost { get; set; }
        public string Commission { get; set; }
        public string DataClose { get; set; }


        public Contracts()
        {
        }

        public Contracts(int Id, int IdUser, string Data, int IdCar, string Cost, string Commission, string Info)
        {
            this.Id = Id;
            this.IdUser = IdUser;
            this.DataOpen = Data;
            this.IdCar = IdCar;
            this.Cost = Cost;
            this.Commission = Commission;
            this.DataClose = Info;

        }

        public Contracts(int Id, int IdUser, string Data, int IdCar, string Cost, string Commission)
        {
            this.Id = Id;
            this.IdUser = IdUser;
            this.DataOpen = Data;
            this.IdCar = IdCar;
            this.Cost = Cost;
            this.Commission = Commission;


        }



        public static void AddContracts(string pt, int IdUser, string Data, int IdCar, string Cost, string Commission)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            sql.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Contracts]  ([IdUser],[DataOpen],[IdCar], [Cost],[Comission])  VALUES (@Surname,@Name,@Patronymic,@Color,@Address)", sql);
            command.Parameters.AddWithValue("@Surname", IdUser);
            command.Parameters.AddWithValue("@Name", Data);
            command.Parameters.AddWithValue("@Color", Cost);
            command.Parameters.AddWithValue("@Patronymic", IdCar);
            command.Parameters.AddWithValue("@Address", Commission);


            bool res = command.ExecuteNonQuery() == 1;
            if (res)
            {
            }
            sql.Close();
            Log.AddLog(pt, "Добавление контракта", "Контракт", DateTime.Now.ToString());
        }

        public static List<Contracts> GetContracts()
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            List<Contracts> Cars = new List<Contracts>();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from [Contracts]", sql);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    try
                    {
                        string e = reader.GetString(6);
                        Cars.Add(new Contracts(
                      reader.GetInt32(0),
                      reader.GetInt32(1),
                      reader.GetString(2),
                      reader.GetInt32(3),
                      reader.GetString(4),
                      reader.GetString(5),
                      e));
                    }
                    catch
                    {
                        Cars.Add(new Contracts(
                      reader.GetInt32(0),
                      reader.GetInt32(1),
                      reader.GetString(2),
                      reader.GetInt32(3),
                      reader.GetString(4),
                      reader.GetString(5),
                      null));

                    }

                }
            }
            sql.Close();
            return Cars;
        }

        public static void Update(string pt, Contracts c)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            sql.Open();
            SqlCommand command = new SqlCommand("update [Contracts] SET IdUser=@IdUs,DataOpen=@Dat,IdCar=@IdC,Cost=@Cost,Comission=@Comis,DataClose=@Inf where id = @ID", sql);
            command.Parameters.AddWithValue("@IdUs", c.IdUser);
            command.Parameters.AddWithValue("@Dat", c.DataOpen);
            command.Parameters.AddWithValue("@IdC", c.IdCar);
            command.Parameters.AddWithValue("@Cost", c.Cost);
            command.Parameters.AddWithValue("@Comis", c.Commission);
            command.Parameters.AddWithValue("@Inf", c.DataClose);
            command.Parameters.AddWithValue("@ID", c.Id);
            bool res = command.ExecuteNonQuery() == 1;
            sql.Close();
            Log.AddLog(pt, "Изменение данных Контракта ", "Контракты", DateTime.Now.ToString());
        }

        public static Contracts GetContract(int id)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            Contracts u = new Contracts();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from [Contracts]  where id=@idu", sql);
            command.Parameters.AddWithValue("@Idu", id);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {


                    u = new Contracts(
                   reader.GetInt32(0),
                   reader.GetInt32(1),
                   reader.GetString(2),
                   reader.GetInt32(3),
                   reader.GetString(4),
                   reader.GetString(5),
                   null);

                }

            }
            sql.Close();
            return u;

        }

        public static List<Contracts> GetContracts(int idUser)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            List<Contracts> u = new List<Contracts>();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from [Contracts]  where idUser=@idu", sql);
            command.Parameters.AddWithValue("@Idu", idUser);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {


                    try
                    {
                        string e = reader.GetString(6);
                        u.Add(new Contracts(
                      reader.GetInt32(0),
                      reader.GetInt32(1),
                      reader.GetString(2),
                      reader.GetInt32(3),
                      reader.GetString(4),
                      reader.GetString(5),
                      e));
                    }
                    catch
                    {
                        u.Add(new Contracts(
                      reader.GetInt32(0),
                      reader.GetInt32(1),
                      reader.GetString(2),
                      reader.GetInt32(3),
                      reader.GetString(4),
                      reader.GetString(5),
                      null));

                    }
                }
            }
            sql.Close();
            return u;

        }

        public static void Delete(Contracts u, string pt)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            sql.Open();
            using (SqlCommand command = new SqlCommand("delete [Contracts] where id = '" + u.Id + "'", sql))
            {
                command.ExecuteNonQuery();
            }
            Log.AddLog(pt, " Удаление Контракта ", "Контракты", DateTime.Now.ToString());
            sql.Close();
        }
    }
}




