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
    public partial class Client : Form
    {
        string connectionStr = @"Data Source=DROGPC;Initial Catalog=DrogsMtrs;Integrated Security=True";
        Main m;
        string pt = "";
        public Clients c { get; set; }
        public Client(Main m, Clients c, string pt)
        {
            InitializeComponent();
            this.m = m;
            this.c = c;
            this.pt = pt;
            button2.Enabled = false;
            if (c != null)
            {
                textBox1.Text = c.surname;
                textBox2.Text = c.Name;
                textBox3.Text = c.Patronymic;
                textBox4.Text = c.Address;
                textBox5.Text = c.Phone;
                button2.Enabled = true;

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) ||
                (!string.IsNullOrEmpty(textBox1.Text) && e.KeyChar == ','))
            {
                return;
            }

            e.Handled = true;
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
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
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
              !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))



                {
                    try
                    {

                        Clients cl = new Clients(c.Id, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                        Clients.Update(pt, cl);
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
              !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))

                {


                    try
                    {
                        Clients.AddClient(pt, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
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

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Clients.Delete(c, pt);
                m.updateListView();
                Close();
            }
            catch
            {
                MessageBox.Show("Нельзя удалить клиента, пока на нем числится машина)", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void Client_Load(object sender, EventArgs e)
        {

        }
    }
}


