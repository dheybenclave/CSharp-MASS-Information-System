using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.IO;
using Sons_of_San_Jose.Properties;

namespace Sons_of_San_Jose
{
    public partial class Login_form : Form
    {
        Db_Utilities db = new Db_Utilities();
        ConnectionSetUp setup = new ConnectionSetUp();
        MySqlCommand cmd;
        DataTable table = new DataTable();
        public static bool ifedit = false;


        public Login_form()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        int TMove;
        int MX;
        int MY;

        private void Form1_Load(object sender, EventArgs e)
        {

            if (db.OpenConnection() == null)
            {
                pnlconnection.Visible = true;
                pnllogin.Visible = false;
                db.pass = true;
                pnlconnection.BringToFront();
            }
            Point p = new Point();
            p.X = (Screen.PrimaryScreen.Bounds.Width / 2) - (this.Size.Width / 2);
            p.Y = (Screen.PrimaryScreen.Bounds.Height / 2) - (this.Size.Height / 2);
            this.Location = p;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pnllogin.Visible == true)
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
            else if (pnlconnection.Visible == true)
            {
                if (btnshow.Text == "on")
                {
                    btnshow.Text = "off";
                    txtpassword.UseSystemPasswordChar = false;
                }
                else
                {
                    btnshow.Text = "on";
                    txtpassword.UseSystemPasswordChar = true;
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {  ExitForm ef = new ExitForm(); ef.ShowDialog(); }

        private void label10_Click(object sender, EventArgs e)
        { this.WindowState = FormWindowState.Minimized; }

        private void rectangleShape2_MouseDown(object sender, MouseEventArgs e)
        { TMove = 1; MX = e.X; MY = e.Y; }

        private void rectangleShape2_MouseUp(object sender, MouseEventArgs e)
        { TMove = 0; }

        private void rectangleShape2_MouseMove(object sender, MouseEventArgs e)
        { if (TMove == 1) President_form.ActiveForm.SetDesktopLocation(MousePosition.X - MX, MousePosition.Y - MY); }

        string position;
        public void AllUser()
        {

            string q = "SELECT * FROM dbms_mass.user WHERE user_name ='" + txtname.Text.Replace("'", "''") + "'AND user_password ='" + txtpass.Text.Replace("'", "''") + "';";
            cmd = new MySqlCommand(q, db.OpenConnection());

            MySqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    Settings.id = rd[0].ToString();
                    position = rd[3].ToString();
                    if (position == "Coordinator" || position == "Co-Coordinator" || position == "Admin")
                    {
                        President_form pf = new President_form();
                        pf.Show();
                    }
                    else if (position == "Secretary")
                    {
                        Secretary ss = new Secretary();
                        ss.Show();
                    }
                    else if (position == "Treasurer")
                    {
                        Printing_Masterlist pm = new Printing_Masterlist();
                        pm.tabcontrolprint.SelectedTab = pm.tbtreasurer;
                        pm.ifpresident = 0;
                        pm.pnlhiding.Size = pm.tabcontrolprint.Size;
                        pm.pnlhiding1.Size = pm.tabcontrolprint.Size;
                        pm.Show();
                    }
                    else if (position == "Commitee")
                    {
                        Committee cm = new Committee();
                        cm.ifpresident = 0;
                        cm.Show();
                        //comittee form
                    }
                    txtname.Text = "";
                    txtpass.Text = ""; 
                    this.Visible = false;
                }
            }
            else { lblnotification.Visible = true; }
            rd.Close();
            db.CloseConnection();

        }
        private void btnlogin_Click(object sender, EventArgs e)
        {
            if ((txtname.Text != null) || (txtpass.Text != null))
                AllUser();
            else { lblnotification.Visible = true; }
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        { lblnotification.Visible = false; }

        private void txtpass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            { AllUser(); }
        }

        private void btnconnect_Click(object sender, EventArgs e)
        {
            if (txtserver.Text == "" || txtport.Text == "" || txtdatabase.Text == "" || txtusername.Text == "" || txtpassword.Text == "")
            {
                lblnoitfconnect.Text = "Please complete the Database Set-up";
                lblnoitfconnect.Visible = true;
            }
            else
            {
                lblnoitfconnect.Visible = false;
                db.server = txtserver.Text;
                db.port = txtport.Text;
                db.database = txtdatabase.Text;
                db.username = txtusername.Text;
                db.password = txtpassword.Text;

                if (db.OpenConnection() == null)
                {
                    try
                    {
                        db.SaveSettings();
                        db.OpenConnection();

                        if (db.OpenConnection() != null)
                        {
                            lblnoitfconnect.Text = "Your Database Connection Success.";
                            pnlconnection.Visible = false;
                            txtpass.Text = txtport.Text = txtserver.Text = txtusername.Text = "";
                            lbleditdbconnection.Enabled = true;
                            pnllogin.Visible = true;
                            pnllogin.BringToFront();
                        }
                        else { lblnoitfconnect.Text = "Your Database Connection is not Valid."; }
                        lblnoitfconnect.Visible = true;
                    }
                    catch
                    {
                        lblnoitfconnect.Text = "Your Database Connection is not Valid";
                        lblnoitfconnect.Visible = true;
                    }
                }
                else
                {

                    try
                    {
                        db.SaveSettings();
                        db.OpenConnection();

                        if (db.OpenConnection() != null)
                        {
                            lblnoitfconnect.Text = "Your Database Connection Success.";
                            pnlconnection.Visible = false;
                            txtpass.Text = txtport.Text = txtserver.Text = txtusername.Text = "";
                            lbleditdbconnection.Enabled = true;
                            pnllogin.Visible = true;
                            pnllogin.BringToFront();
                        }
                        else { lblnoitfconnect.Text = "Your Database Connection is not Valid."; }
                        lblnoitfconnect.Visible = true;
                    }
                    catch
                    {
                        lblnoitfconnect.Text = "Your Database Connection is not Valid";
                        lblnoitfconnect.Visible = true;
                    }
                }
            }
        }



        private void txtpassword_TextChanged(object sender, EventArgs e)
        {
            lblnoitfconnect.Visible = false;

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Notification_Input ni = new Notification_Input();
            ni.ShowDialog();
        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != ' '))
            {
                e.Handled = true;

            }
        }

        private void pnllogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbleditdbconnection_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            Notification_Input ni = new Notification_Input();
            ni.ifeditconnection = true;
            ni.lblnotification.Text = " We cannot edit database using this password!";
            ni.ShowDialog();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // MessageBox.Show(ifedit.ToString());
            if (ifedit == true)
            {
                pnlconnection.Visible = true;
                pnlconnection.BringToFront();
                lbleditdbconnection.Enabled = false;
                pnllogin.Visible = false;
                ifedit = false;
                timer1.Stop();
                timer1.Enabled = false;
            }
            else { lbleditdbconnection.Enabled = true; }

        }

    }
}

