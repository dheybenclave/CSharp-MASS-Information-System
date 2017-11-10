using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace Sons_of_San_Jose
{
    public partial class Notification_Input : Form
    {
       public bool iscorrect, ifeditconnection = false; 
        Db_Utilities db = new Db_Utilities();
        ConnectionSetUp setup = new ConnectionSetUp();
        MySqlCommand cmd;
        DataTable table = new DataTable();

        public Notification_Input()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
          public void AllUser()
        {
            string q = "SELECT user_position FROM dbms_mass.user WHERE user_password ='" + txtpass.Text.Replace("'","''") + "';";
            cmd = new MySqlCommand(q, db.OpenConnection());

            MySqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                while (rd.Read())
                {

                    if (ifeditconnection == true)
                    {
                        if (rd[0].ToString() == "Admin")
                        {
                            Login_form.ifedit = true;
                            this.Close();
                            ifeditconnection = false;
                        }
                    }
                    else
                    {
                        if (rd[0].ToString() == "Coordinator" || rd[0].ToString() == "Co-Coordinator" || rd[0].ToString() == "Secretary" || rd[0].ToString() == "Admin")
                        {
                            iscorrect = true;
                            timer1.Enabled = true;
                            timer1.Start();
                            this.Close();
                        }
                        else
                        {
                            lblnotification.Visible = true;
                        }
                    }
                }
            }
            else { lblnotification.Visible = true; }
            rd.Close();
            db.CloseConnection();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "on")
            {
                button2.Text = "off";
                txtpass.UseSystemPasswordChar = false;
            }
            else
            {
                button2.Text = "on";
                txtpass.UseSystemPasswordChar = true;
            }
        }

        private void txtpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                AllUser();
        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {
            lblnotification.Visible = false;
        }

        private void Notification_Input_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (iscorrect == true) { timer1.Stop();  Create_Account ca = new Create_Account(); ca.ShowDialog(); }
        }

        private void txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != ' '))
            {
                e.Handled = true;

            }
        }
    }
}
