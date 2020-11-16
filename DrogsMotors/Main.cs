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
    public partial class Main : Form
    {
        string s = "";
        bool admin;
        int id;
        public Main(bool Admin, string Name, string Patronymic, string Surname, int id)
        {
            this.id = id;
            this.s = Surname;
            this.admin = Admin;
            InitializeComponent();
            helloLabel.ForeColor = Color.WhiteSmoke;
            helloLabel.Text = "Добрый день, " + Name + " " + Patronymic;
            updateListView();
            if (Admin)
            {
                addClientButton.Visible = false;
                addContractButton.Visible = false;
                addCarButton.Visible = false;

            }
            else
            {
                registrationButton.Visible = false;
            }

        }
        public void updateListView()
        {
            historyView.Items.Clear();
            List<Log> l = Log.GetAllLog();

            for (int i = 0; i < l.Count; i++)
            {
                string[] array = new string[5];
                array[0] = l[i].IdWrite.ToString();
                array[1] = l[i].LastNameUser;
                array[2] = l[i].Act;
                array[3] = l[i].TableAct;
                array[4] = l[i].DateAct;
                historyView.Items.Add(new ListViewItem(array));
            }

            usersView.Items.Clear();
            List<Users> u = Users.GetAllUsers();

            for (int i = 0; i < u.Count; i++)
            {
                string[] array = new string[7];
                array[0] = u[i].Id.ToString();
                if (u[i].IdUserRole == 1)
                { array[1] = "админ"; }
                else
                {
                    array[1] = "диллер";
                }

                array[2] = u[i].Surname;
                array[3] = u[i].Name;
                array[4] = u[i].Patronymic;
                array[5] = u[i].Address;
                array[6] = u[i].Phone;

                usersView.Items.Add(new ListViewItem(array));
            }
            clientsView.Items.Clear();
            List<Clients> a = Clients.GetAllClients();

            for (int i = 0; i < a.Count; i++)
            {
                string[] array = new string[6];
                array[0] = a[i].Id.ToString();
                array[1] = a[i].surname;
                array[2] = a[i].Name;
                array[3] = a[i].Patronymic;
                array[4] = a[i].Address;
                array[5] = a[i].Phone;
                clientsView.Items.Add(new ListViewItem(array));
            }
            contractsView.Items.Clear();
            List<Contracts> v = Contracts.GetContracts();

            for (int i = 0; i < v.Count; i++)
            {
                string[] array = new string[6];
                if (v[i].DataClose == null)
                {
                    array[0] = v[i].Id.ToString();
                    Users g = Users.GetUsers(v[i].IdUser);
                    array[2] = v[i].DataOpen;
                    array[1] = g.Surname;
                    Cars k = Cars.GetCar(v[i].IdCar);
                    array[3] = k.Vin;
                    array[4] = v[i].Cost;
                    array[5] = v[i].Commission;
                }
                else
                {
                    continue;
                }


                contractsView.Items.Add(new ListViewItem(array));
            }
            carsView.Items.Clear();
            List<Cars> d = Cars.GetCars();

            for (int i = 0; i < d.Count; i++)
            {
                string[] array = new string[8];
                array[0] = d[i].Id.ToString();
                Clients c = Clients.GetClient(d[i].IdClient);
                array[1] = c.surname;
                List<String> m = Cars.GetModel(d[i].IdModel);
                array[2] = m[0];
                array[4] = d[i].Color;
                array[3] = d[i].Vin;
                array[5] = d[i].IssueDate;
                array[6] = d[i].Milleage;
                array[7] = d[i].Transmission;
                carsView.Items.Add(new ListViewItem(array));
            }


        }
        private void usersView_MouseClick(object sender, MouseEventArgs e)
        {
            if (admin)
            {
                string selected = usersView.SelectedItems[0].Text;
                Form Reg = new Reg(this, Users.GetUsers(int.Parse(selected)));
                Reg.Owner = this;
                Reg.ShowDialog();
                updateListView();
            }
            else
            {
                string selected = usersView.SelectedItems[0].Text;
                if (id == Users.GetUsers(int.Parse(selected)).Id)
                {
                    Form Reg = new Reg(this, Users.GetUsers(int.Parse(selected)));
                    Reg.Owner = this;
                    Reg.ShowDialog();
                    updateListView();
                }

            }

        }
        private void clientsView_MouseClick(object sender, MouseEventArgs e)
        {
            if (!admin)
            {

                string selected = clientsView.SelectedItems[0].Text;
                Form Client = new Client(this, Clients.GetClient(int.Parse(selected)), s);
                Client.Owner = this;
                Client.ShowDialog();
                updateListView();
            }
        }
        private void contractsView_MouseClick(object sender, MouseEventArgs e)
        {
            if (!admin)
            {

                string selected = contractsView.SelectedItems[0].Text;
                Form AddContract = new AddContract(this, Contracts.GetContract(int.Parse(selected)), s);
                AddContract.Owner = this;
                AddContract.ShowDialog();
                updateListView();
            }
        }

        private void carsView_MouseClick(object sender, MouseEventArgs e)
        {
            if (!admin)
            {

                string selected = carsView.SelectedItems[0].Text;
                Form AddCars = new AddCars(this, Cars.GetCar(int.Parse(selected)), s);
                AddCars.Owner = this;
                AddCars.ShowDialog();
                updateListView();
            }
        }

        private void registrationButton_Click(object sender, EventArgs e)
        {
            Form Reg = new Reg(this, null);
            Reg.Owner = this;
            Reg.ShowDialog();
            updateListView();
        }

        private void newClientButton_Click(object sender, EventArgs e)
        {
            Form Client = new Client(this, null, s);
            Client.Owner = this;
            Client.ShowDialog();
            updateListView();
        }

        private void newCarButton_Click(object sender, EventArgs e)
        {
            Form AddCars = new AddCars(this, null, s);
            AddCars.Owner = this;
            AddCars.ShowDialog();
            updateListView();
        }

        private void newContractButton_Click(object sender, EventArgs e)
        {
            Form AddContract = new AddContract(this, null, s);
            AddContract.Owner = this;
            AddContract.ShowDialog();
            updateListView();

        }

        private void showContractsButton_Click(object sender, EventArgs e)
        {
            Form AllContracts = new AllContracts();
            AllContracts.Owner = this;
            AllContracts.ShowDialog();
            updateListView();

        }

        private void exitSessionButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти из системы?", "Выйти", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Login pf = new Login();

                pf.Show();
                this.Hide();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void openContractsButton_Click(object sender, EventArgs e)
        {
            noDataLabel.Text = "";
            if (contractsBox.Text != null && contractsBox.Text != "")
            {
                Users f = Users.GetUsers(contractsBox.Text);
                List<Contracts> q = Contracts.GetContracts(f.Id);
                openContractsView.Items.Clear();

                for (int i = 0; i < q.Count; i++)
                {
                    string[] array = new string[3];
                    if (q[i].DataClose == null)
                    {
                        array[0] = q[i].Id.ToString();

                        Cars k = Cars.GetCar(q[i].IdCar);
                        array[1] = k.Vin;
                        array[2] = Cars.GetModel(k.Id).ToString();
                        openContractsView.Items.Add(new ListViewItem(array));

                    }
                    else
                    {
                        continue;
                    }

                }
            }

        }
        private void contractsBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void clientsIdBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }
        private void allContractsButton_Click(object sender, EventArgs e)
        {
            noDataLabel.Text = "";
            if (contractsBox.Text != null && contractsBox.Text != "")
            {
                Users f = Users.GetUsers(contractsBox.Text);
            
                List<Contracts> q = Contracts.GetContracts(f.Id);
                openContractsView.Items.Clear();
                if (q.Count == 0)
                {
                    noDataLabel.Text = "Ничего не найдено";
                }
                for (int i = 0; i < q.Count; i++)
                {
                    string[] array = new string[3];

                    array[0] = q[i].Id.ToString();

                    Cars k = Cars.GetCar(q[i].IdCar);
                    array[1] = k.Vin;
                    List<String> m = Cars.GetModel(k.IdModel);
                    array[2] = m[0];
                    openContractsView.Items.Add(new ListViewItem(array));

                }
            }

        }

        private void findCarsButton_Click(object sender, EventArgs e)
        {
            label5.Text = "";
            if (textBox2.Text != null && textBox2.Text != "")
            {
                Users f = Users.GetUsers(textBox2.Text);
                List<Cars> q = Cars.GetCarr(f.Id);
                listView7.Items.Clear();
                if (q.Count == 0)
                {
                    noDataLabel.Text = "Ничего не найдено";
                }
                for (int i = 0; i < q.Count; i++)
                {
                    string[] array = new string[3];

                    array[0] = q[i].Id.ToString();

                    array[1] = q[i].Vin;
                    List<String> m = Cars.GetModel(q[i].IdModel);
                    array[2] = m[0];
                    listView7.Items.Add(new ListViewItem(array));

                }
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}


