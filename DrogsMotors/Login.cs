using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace DrogsMotors
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(passwordBox.Text) && !string.IsNullOrWhiteSpace(passwordBox.Text) &&
                !string.IsNullOrEmpty(loginBox.Text) && !string.IsNullOrWhiteSpace(loginBox.Text))
            {
                string connectionStr = @"Data Source=DROGPC; Initial Catalog=DrogsMtrs; Integrated Security=True";
                SqlConnection sql = new SqlConnection(connectionStr);
                sql.Open();
                SqlCommand command = new SqlCommand("(SELECT id, idUserRole, Login, Password, Surname, Name, Patronymic FROM [User])", sql);
                bool res = command.ExecuteNonQuery() == 1;
                bool b = false;
                bool isAdmin = false;
                int id = 0;
                var fn = "";
                var ln = "";
                var pt = "";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string login = reader.GetValue(2).ToString();
                        string pwd = reader.GetValue(3).ToString();
                        if (loginBox.Text == login &&
                            PasswordHelper.VerifyPassword(passwordBox.Text, pwd))
                        {
                            b = true;
                            ln = reader.GetValue(5).ToString();
                            fn = reader.GetValue(4).ToString();
                            pt = reader.GetValue(6).ToString();
                            id = int.Parse(reader.GetValue(0).ToString());
                            isAdmin = reader.GetValue(1).ToString() == "1";

                            break;
                        }
                    }

                    var pos = isAdmin ? "админ" : "дилер";

                    if (b)
                    {
                        Log l = new Log();
                        l.DateAct = DateTime.Now.ToString();
                        l.Act = "Вход в систему " + pos;
                        l.LastNameUser = fn;
                        l.TableAct = "-";
                        Log.AddLog(fn, "Вход в систему " + pos, "-", DateTime.Now.ToString());
                        var mainForm = new Main(isAdmin, ln, pt, fn, id);
                        mainForm.Text = fn + " " + ln + " " + pt + ", " + pos;
                        mainForm.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Неправильный логин или пароль", "Ошибка", MessageBoxButtons.OK);
                    }


                    if (reader != null)
                        reader.Close();
                    sql.Close();
                }
            }

            else
            {
                MessageBox.Show("Поля не заполнены");
            }

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
