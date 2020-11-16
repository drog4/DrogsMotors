using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DrogsMotors
{
    public class Users
    {

        public int Id { get; set; }
        public int IdUserRole { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }


        public Users()
        {
        }

        public Users(int Id, int IdUserRole, string Login, string Password, string SurName, string Name, string Patronymic, string Address, string Phone)
        {
            this.Id = Id;
            this.IdUserRole = IdUserRole;
            this.Login = Login;
            this.Password = Password;
            this.Surname = SurName;
            this.Name = Name;
            this.Patronymic = Patronymic;
            this.Address = Address;
            this.Phone = Phone;
        }

        public static void AddUser(int IdUserRole, string Login, string Password, string SurName, string Name, string Patronymic, string Address, string Phone)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            sql.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [User]  ([idUserRole],[Login],[Password],[Surname],[Name],[Patronymic],[Address],[Phone])  VALUES (@idUserRole,@Login,@Password,@Surname,@Name,@Patronymic,@Address,@Phone)", sql);
            command.Parameters.AddWithValue("@idUserRole", IdUserRole);
            command.Parameters.AddWithValue("@Login", Login);
            command.Parameters.AddWithValue("@Password", PasswordHelper.GetPasswordHash(Password));
            command.Parameters.AddWithValue("@Surname", SurName);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Patronymic", Patronymic);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            bool res = command.ExecuteNonQuery() == 1;
            sql.Close();
            Log l = new Log();
            l.DateAct = DateTime.Now.ToString();
            l.Act = "Добавление сотрудника " + SurName;
            l.TableAct = "Сотрудники";
            Log.AddLog("Дрогайцев", "Добавление сотрудника " + SurName, "Сотрудники", DateTime.Now.ToString());
        }

        public static List<Users> GetAllUsers()
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            List<Users> Users = new List<Users>();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from [User]", sql);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Users.Add(new Users(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        reader.GetString(7),
                        reader.GetString(8)));
                }
            }
            sql.Close();
            return Users;
        }

        public static void Update(Users u)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            sql.Open();
            SqlCommand command = new SqlCommand("update [User] SET idUserRole = @idUserRole, Login = @Login,Password = @Password,Surname = @Surname,Name = @Name,Patronymic = @Patronymic,Address = @Address,Phone= @Phone where id = @ID", sql);
            command.Parameters.AddWithValue("@idUserRole", u.IdUserRole);
            command.Parameters.AddWithValue("@Login", u.Login);
            command.Parameters.AddWithValue("@Password", PasswordHelper.GetPasswordHash(u.Password));
            command.Parameters.AddWithValue("@Surname", u.Surname);
            command.Parameters.AddWithValue("@Name", u.Name);
            command.Parameters.AddWithValue("@Patronymic", u.Patronymic);
            command.Parameters.AddWithValue("@Address", u.Address);
            command.Parameters.AddWithValue("@Phone", u.Phone);
            command.Parameters.AddWithValue("@ID", u.Id);
            bool res = command.ExecuteNonQuery() == 1;
            sql.Close();
            Log l = new Log();
            l.DateAct = DateTime.Now.ToString();
            l.Act = "Изменение  данных - сотрудника " + u.Surname;
            l.TableAct = "Сотрудники";
            Log.AddLog("Дрогайцев", "Изменение  данных сотрудника " + u.Surname, "Сотрудники", DateTime.Now.ToString());
        }

        public static Users GetUsers(int id)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            Users u = new Users();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from [User]  where Id=@idu", sql);
            command.Parameters.AddWithValue("@Idu", id);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    u = new Users(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        reader.GetString(7),
                        reader.GetString(8));
                }
            }
            sql.Close();
            return u;

        }

        public static Users GetUsers(string name)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            Users u = new Users();
            sql.Open();
            SqlCommand command = new SqlCommand("Select * from [User]  where Surname=@idu", sql);
            command.Parameters.AddWithValue("@Idu", name);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    u = new Users(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        reader.GetString(7),
                        reader.GetString(8));
                }
            }
            sql.Close();
            return u;

        }

        public static void Delete(Users u)
        {
            string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
            SqlConnection sql = new SqlConnection(connectionStr);
            sql.Open();
            using (SqlCommand command = new SqlCommand("delete [User] where id = '" + u.Id + "'", sql))
            {
                command.ExecuteNonQuery();
            }
            sql.Close();
        }
    }
}

