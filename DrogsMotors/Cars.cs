using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DrogsMotors
{
    public class Cars
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public int IdModel { get; set; }
        public string Vin { get; set; }
        public string Color { get; set; }
        public string IssueDate { get; set; }
        public string Milleage { get; set; }
        public string Transmission { get; set; }

        public Cars()
        {
        }

        public Cars(int Id, int IdClient, int IdModel, string Vin, string Color, string IssueDate, string Milleage, string Transmission)
        {
            this.Id = Id;
            this.IdClient = IdClient;
            this.IdModel = IdModel;
            this.Color = Color;
            this.Vin = Vin;
            this.IssueDate = IssueDate;
            this.Transmission = Transmission;
            this.Milleage = Milleage;

        }

        public static List<string> GetModel(int id)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            List<string> model = new List<string>();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from [CarsModel] where id = '" + id + "'", sql);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int i = 1;

                    model.Add(reader.GetString(i));
                    i++;
                }

                sql.Close();
            }
            return model;
        }

        public static Model GetModel(string name)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            Model m = new Model();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from [CarsModel] where Name = '" + name + "'", sql);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    m = new Model(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2));
                }

                sql.Close();
            }
            return m;
        }

        public static List<Model> GetModels()
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            List<Model> model = new List<Model>();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from [CarsModel]", sql);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())

                {

                    model.Add(new Model(
                       reader.GetInt32(0),
                        reader.GetString(1),
                         reader.GetString(2)));
                }
            }

            sql.Close();

            return model;
        }

        public static void AddModel(string pt, string name, string info)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            sql.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [CarsModel]  ([Name],[Info]) VALUES (@Name,@Info)", sql);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Info", info);
            bool res = command.ExecuteNonQuery() == 1;
            sql.Close();
            Log.AddLog(pt, "Добавление  марки машины " + name, "Марка машины", DateTime.Now.ToString());

        }

        public static void AddCar(string pt, int IdClient, int IdModel, string Color, string Vin, string IssueDate, string Milleage, string Transmission)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            sql.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Cars]  ([IdClient],[IdModel], [Color], [Vin], [IssueDate],[Milleage] ,[Transmission])  VALUES (@IdClient,@IdModel,@Color,@Vin,@IssueDate,@Milleage, @Transmission)", sql);
            command.Parameters.AddWithValue("@IdClient", IdClient);
            command.Parameters.AddWithValue("@IdModel", IdModel);
            command.Parameters.AddWithValue("@Vin", Vin);
            command.Parameters.AddWithValue("@Color", Color);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@Milleage", Milleage);
            command.Parameters.AddWithValue("@Transmission", Transmission);
            bool res = command.ExecuteNonQuery() == 1;
            sql.Close();
            Log.AddLog(pt, "Добавление машины " + Vin, "Машина", DateTime.Now.ToString());
        }

        public static List<Cars> GetCars()
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            List<Cars> Cars = new List<Cars>();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from [Cars]", sql);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Cars.Add(new Cars(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        reader.GetString(7)));
                }
            }
            sql.Close();
            return Cars;
        }


        public static void Update(string pt, Cars c)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            sql.Open();
            SqlCommand command = new SqlCommand("update [Cars] SET IdClient=@IdClien,IdModel = @IdModel,Vin=@Vin,Color=@Color,IssueDate=@IssueDate,Milleage=@Milleage,Transmission=@Transmission where id = @ID", sql);
            command.Parameters.AddWithValue("@IdClient", c.IdClient);
            command.Parameters.AddWithValue("@IdModel", c.IdModel);
            command.Parameters.AddWithValue("@Vin", c.Vin);
            command.Parameters.AddWithValue("@Color", c.Color);
            command.Parameters.AddWithValue("@IssueDate", c.IssueDate);
            command.Parameters.AddWithValue("@Milleage", c.Milleage);
            command.Parameters.AddWithValue("@Transmission", c.Transmission);
            command.Parameters.AddWithValue("@ID", c.Id);
            bool res = command.ExecuteNonQuery() == 1;
            sql.Close();
            Log.AddLog(pt, "Изменение данных машины " + c.Vin, "Машины", DateTime.Now.ToString());
        }

        public static Cars GetCar(int id)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            Cars u = new Cars();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from [Cars]  where id=@Id", sql);
            command.Parameters.AddWithValue("@Id", id);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    u = new Cars(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        reader.GetString(7));

                }
            }
            sql.Close();
            return u;

        }

        public static List<Cars> GetCarr(int id)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            List<Cars> Cars = new List<Cars>();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from [Cars]  where idClient=@Id", sql);
            command.Parameters.AddWithValue("@Id", id);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Cars.Add(new Cars(
                       reader.GetInt32(0),
                       reader.GetInt32(1),
                       reader.GetInt32(2),
                       reader.GetString(3),
                       reader.GetString(4),
                       reader.GetString(5),
                       reader.GetString(6),
                       reader.GetString(7)));

                }
            }
            sql.Close();
            return Cars;

        }

        public static Cars GetCar(string vin)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            Cars u = new Cars();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from [Cars]  where Vin=@Vin", sql);
            command.Parameters.AddWithValue("@Vin", vin);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    u = new Cars(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetInt32(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        reader.GetString(7));

                }
            }
            sql.Close();
            return u;

        }

        public static void Delete(Cars u, string pt)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            sql.Open();
            using (SqlCommand command = new SqlCommand("delete [Cars] where id = '" + u.Id + "'", sql))
            {
                command.ExecuteNonQuery();
            }
            Log.AddLog(pt, " Удаление машины " + u.Vin, "Машины", DateTime.Now.ToString());
            sql.Close();
        }
    }
}

