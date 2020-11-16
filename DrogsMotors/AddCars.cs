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
    public partial class AddCars : Form
    {
        string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
        Main m;
        string pt = "";
        public Cars c { get; set; }
        public AddCars(Main m, Cars c, string pt)
        {
            InitializeComponent();
            button3.Visible = false;
            List<Clients> list = Clients.GetAllClients();
            for (int i = 0; i < list.Count; i++)
            {
                comboBox1.Items.Add(list[i].surname);
            }
            List<Model> list2 = Cars.GetModels();
            for (int i = 0; i < list2.Count; i++)
            {
                comboBox2.Items.Add(list2[i].name);
            }

            this.m = m;
            this.c = c;
            this.pt = pt; ;
            if (c != null)
            {
                Clients v = Clients.GetClient(c.IdClient);
                comboBox1.Text = v.surname;
                List<String> a = Cars.GetModel(c.IdModel);
                comboBox2.Text = a[0];
                textBox1.Text = c.Vin;
                textBox2.Text = c.Color;
                textBox3.Text = c.IssueDate;
                textBox4.Text = c.Milleage;
                textBox5.Text = c.Transmission;
                button3.Visible = true;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (c != null)
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                     !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
              !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
              !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
              !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) &&
                    !string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text) &&
                   !string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrWhiteSpace(comboBox2.Text))

                {
                    try
                    {


                        Clients cl = Clients.GetClient(comboBox1.Text);
                        Model c2 = Cars.GetModel(comboBox2.Text);
                        Cars m = new Cars(c.Id, cl.Id, c2.id, textBox2.Text.ToString(), textBox1.Text.ToString(), textBox3.Text.ToString(), textBox4.Text.ToString(), textBox5.Text.ToString());
                        Cars.Update(pt, m);
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
            else
            {

                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                     !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
              !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
              !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
              !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) &&
                    !string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text) &&
                   !string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrWhiteSpace(comboBox2.Text))

                {


                    try
                    {
                        Clients cl = Clients.GetClient(comboBox1.Text);
                        Model c2 = Cars.GetModel(comboBox2.Text);
                        Cars.AddCar(pt, cl.Id, c2.id, textBox2.Text.ToString(), textBox1.Text.ToString(),  textBox3.Text.ToString(), textBox4.Text.ToString(), textBox5.Text.ToString());

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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) ||
                (!string.IsNullOrEmpty(textBox1.Text) && e.KeyChar == ','))
            {
                return;
            }

            e.Handled = true;

        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) ||
                (!string.IsNullOrEmpty(textBox1.Text) && e.KeyChar == ','))
            {
                return;
            }

            e.Handled = true;

        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {


                Cars.Delete(c, pt);
                m.updateListView();
                Close();
            }
            catch
            {
                MessageBox.Show("Нельзя удалить машину, которая числится в сделке", "Ошибка", MessageBoxButtons.OK);

            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            Form AddModel = new AddModel(this, pt);
            AddModel.Owner = this;
            AddModel.ShowDialog();
            comboBox2.Items.Clear();
            List<Model> list2 = Cars.GetModels();
            for (int i = 0; i < list2.Count; i++)
            {
                comboBox2.Items.Add(list2[i].name);
            }

        }

        private void AddCars_Load(object sender, EventArgs e)
        {

        }
    }
}


