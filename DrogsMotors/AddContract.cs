using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrogsMotors
{
    public partial class AddContract : Form
    {
        string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
        Main m;
        string pt = "";
        public Contracts c { get; set; }
        public AddContract(Main m, Contracts c, string pt)
        {

            InitializeComponent();
            label6.Visible = false;
            button3.Visible = false;
            List<Users> list = Users.GetAllUsers();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].IdUserRole == 2)
                {

                    comboBox1.Items.Add(list[i].Surname);
                }
            }
            List<Contracts> contr = Contracts.GetContracts();
            List<Cars> list2 = Cars.GetCars();
            for (int i = 0; i < list2.Count; i++)
            {
                bool a = false;
                for (int t = 0; t < contr.Count; t++)
                {


                    if (contr[t].IdCar == list2[i].Id)
                    {
                        a = true;

                    }
                }
                if (!a)
                {
                    comboBox2.Items.Add(list2[i].Vin);
                }
            }

            this.m = m;
            this.c = c;
            this.pt = pt; ;
            if (c != null)
            {
                button1.Text = "Завершить контракт";
                Users v = Users.GetUsers(c.IdUser);
                if (v.IdUserRole == 2)
                {
                    comboBox1.Text = v.Surname;
                }
                Cars a = Cars.GetCar(c.IdCar);
                comboBox2.Text = a.Vin;

                textBox2.Text = c.Cost;
                textBox3.Text = c.Commission;
                button3.Visible = true;
                label6.Visible = true;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (c != null)
            {
                if (
                     !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
              !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&

                    !string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text) &&
                   !string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrWhiteSpace(comboBox2.Text))

                {
                    Users cl = Users.GetUsers(comboBox1.Text);
                    Cars c2 = Cars.GetCar(comboBox2.Text);
                    Contracts m = new Contracts(c.Id, cl.Id, dateTimePicker1.Text.ToString(), c2.Id, textBox2.Text.ToString(), textBox3.Text.ToString(), DateTime.Today.ToString());
                    Contracts.Update(pt, m);
                    Close();

                }
                else
                {
                    MessageBox.Show("Вы ввели некорректные данные, попробуйте еще раз)", "Ошибка", MessageBoxButtons.OK);
                }

            }
            else
            {
                if (int.Parse(textBox3.Text) >= int.Parse(textBox2.Text))
                {
                    MessageBox.Show("Вы ввели некорректные данные, попробуйте еще раз)", "Ошибка", MessageBoxButtons.OK);

                }



                else
                {
                    if (
                    !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
             !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&

                   !string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text) &&
                    !string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrWhiteSpace(comboBox2.Text))
                    {


                        try
                        {
                            Users cl = Users.GetUsers(comboBox1.Text);
                            Cars c2 = Cars.GetCar(comboBox2.Text);
                            Contracts.AddContracts(pt, cl.Id, dateTimePicker1.Text.ToString(), c2.Id, textBox2.Text.ToString(), textBox3.Text.ToString());

                            Close();

                        }
                        catch
                        {
                            MessageBox.Show("Вы ввели некорректные данные, попробуйте еще раз)", "Ошибка", MessageBoxButtons.OK);

                        }

                    }


                    else
                    {
                        MessageBox.Show("Вы ввели некорректные данные, попробуйте еще раз)", "Ошибка", MessageBoxButtons.OK);
                    }
                }
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Contracts.Delete(c, pt);
                Close();
            }
            catch
            {
                DialogResult = MessageBox.Show("НЕЛЬЗЯ удалить этот контракт)");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddContract_Load(object sender, EventArgs e)
        {

        }
    }
}
