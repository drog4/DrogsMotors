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
    public partial class AllContracts : Form
    {
        public AllContracts()
        {
            InitializeComponent();
            listView1.Items.Clear();
            List<Contracts> v = Contracts.GetContracts();

            for (int i = 0; i < v.Count; i++)
            {
                string[] array = new string[7];
                array[0] = v[i].Id.ToString();
                Users g = Users.GetUsers(v[i].IdUser);
                array[2] = v[i].DataOpen;
                array[1] = g.Surname;
                Cars k = Cars.GetCar(v[i].IdCar);
                array[3] = k.Vin;
                array[4] = v[i].Cost;
                array[5] = v[i].Commission;
                array[6] = v[i].DataClose;

                listView1.Items.Add(new ListViewItem(array));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AllContracts_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string a = dateTimePicker1.Text;
            string d = dateTimePicker2.Text;
            int s = int.Parse(a[0].ToString() + a[1].ToString());
            int t = int.Parse(d[0].ToString() + d[1].ToString());
            listView2.Items.Clear();
            List<Contracts> contr = Contracts.GetContracts();
            for (int i = 0; i < contr.Count; i++)
            {
                int q = int.Parse(contr[i].DataOpen[0].ToString() + contr[i].DataOpen[1].ToString());
                if(q>s && q<t)
                {
                 
                    string[] array = new string[3];
                    array[0] = contr[i].Id.ToString();
                    Users g = Users.GetUsers(contr[i].IdUser);
                    array[1] = contr[i].DataOpen;
                   
                    Cars k = Cars.GetCar(contr[i].IdCar);
                    array[2] = k.Vin;
                    

                    listView2.Items.Add(new ListViewItem(array));

                }
            }
        }
    } }
  
