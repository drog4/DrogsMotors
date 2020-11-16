
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace DrogsMotors
{
    public partial class Reg : Form
    {
        string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
        Main m;
        public Users u { get; set; }
        public Reg(Main m, Users u)
        {
            InitializeComponent();
            this.m = m;
            this.u = u;
            clearButton.Enabled = false;
            if (u != null)
            {
                loginBox.Text = u.Login;
                surnameBox.Text = u.Surname;
                nameBox.Text = u.Name;
                patronymicBox.Text = u.Patronymic;
                addressBox.Text = u.Address;
                phoneBox.Text = u.Phone;
                clearButton.Enabled = true;

            }
        }
        private async void saveButton_Click(object sender, EventArgs e)
        {

            {
                if (u != null)
                {
                    if (!string.IsNullOrEmpty(loginBox.Text) && !string.IsNullOrWhiteSpace(loginBox.Text) &&
              !string.IsNullOrEmpty(passwordBox.Text) && !string.IsNullOrWhiteSpace(passwordBox.Text) &&
              !string.IsNullOrEmpty(surnameBox.Text) && !string.IsNullOrWhiteSpace(surnameBox.Text) &&
              !string.IsNullOrEmpty(nameBox.Text) && !string.IsNullOrWhiteSpace(nameBox.Text) &&
              !string.IsNullOrEmpty(patronymicBox.Text) && !string.IsNullOrWhiteSpace(patronymicBox.Text) &&
              !string.IsNullOrEmpty(addressBox.Text) && !string.IsNullOrWhiteSpace(addressBox.Text) &&
              !string.IsNullOrEmpty(phoneBox.Text) && !string.IsNullOrWhiteSpace(phoneBox.Text))

                    {
                        Users us = new Users(u.Id, u.IdUserRole, loginBox.Text, passwordBox.Text, surnameBox.Text, nameBox.Text, patronymicBox.Text, addressBox.Text, phoneBox.Text);
                        Users.Update(us);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Вы ввели некорректные данные, попробуйте еще раз)", "Ошибка", MessageBoxButtons.OK);
                    }

                }
                else
                {

                    if (!string.IsNullOrEmpty(loginBox.Text) && !string.IsNullOrWhiteSpace(loginBox.Text) &&
                !string.IsNullOrEmpty(passwordBox.Text) && !string.IsNullOrWhiteSpace(passwordBox.Text) &&
                !string.IsNullOrEmpty(surnameBox.Text) && !string.IsNullOrWhiteSpace(surnameBox.Text) &&
                !string.IsNullOrEmpty(nameBox.Text) && !string.IsNullOrWhiteSpace(nameBox.Text) &&
                !string.IsNullOrEmpty(patronymicBox.Text) && !string.IsNullOrWhiteSpace(patronymicBox.Text) &&
                !string.IsNullOrEmpty(addressBox.Text) && !string.IsNullOrWhiteSpace(addressBox.Text) &&
                !string.IsNullOrEmpty(phoneBox.Text) && !string.IsNullOrWhiteSpace(phoneBox.Text))

                    {
                        bool b = false;

                        var sqlConnection = new SqlConnection(connectionStr);
                        sqlConnection.Open();
                        try
                        {
                            using (var comm = new SqlCommand("SELECT Login FROM [User]", sqlConnection))
                            {
                                SqlDataReader sqlReader = comm.ExecuteReader();

                                try
                                {
                                    while (await sqlReader.ReadAsync())
                                    {
                                        string log = sqlReader.GetValue(0).ToString();
                                        if (loginBox.Text == log)
                                        {
                                            b = true;
                                            break;
                                        }
                                    }
                                }
                                finally
                                {
                                    sqlReader.Close();
                                }
                            }
                        }
                        finally
                        {
                            sqlConnection.Close();
                        }

                        if (!b)
                        {

                            try
                            {
                                Users.AddUser(2, loginBox.Text, passwordBox.Text, surnameBox.Text, nameBox.Text, patronymicBox.Text, addressBox.Text, phoneBox.Text);

                            }
                            finally
                            {
                                Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы ввели некорректные данные, попробуйте еще раз)", "Ошибка", MessageBoxButtons.OK);
                    }
                }
            }
        }


        private void surnameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) ||
                (!string.IsNullOrEmpty(loginBox.Text) && e.KeyChar == ','))
            {
                return;
            }

            e.Handled = true;
        }

        private void nameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) ||
                (!string.IsNullOrEmpty(loginBox.Text) && e.KeyChar == ','))
            {
                return;
            }

            e.Handled = true;
        }
        private void PatronymicBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) ||
                (!string.IsNullOrEmpty(loginBox.Text) && e.KeyChar == ','))
            {
                return;
            }

            e.Handled = true;

        }
        private void phoneBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {

                Users.Delete(u);
                m.updateListView();
                Close();
                Log l = new Log();
                Log.AddLog("Дрогайцев", "Увольнение сотрудника " + u.Surname, "Сотрудники", DateTime.Now.ToString());
            }
            catch
            {
                DialogResult = MessageBox.Show("НЕЛЬЗЯ УВОЛИТЬ РАБОТНИКА, ПОКА ОН ЧИСЛИТСЯ ОТВЕТСТВЕННЫМ ЗА КОНТРАКТ:)");
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Reg_Load(object sender, EventArgs e)
        {

        }
    }
}

