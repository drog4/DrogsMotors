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
    public partial class AddModel : Form
    {
        string pt = "";
        AddCars m;
        public AddModel(AddCars m, string pt)

        {
            this.m = m;
            this.pt = pt;
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                    !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))



                try
                {
                    Cars.AddModel(pt, textBox1.Text, textBox2.Text);

                }
                finally
                {
                    Close();
                }



            else
            {
                MessageBox.Show("Вы ввели некорректные данные, попробуйте еще раз)", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void AddModel_Load(object sender, EventArgs e)
        {

        }
    }
}
