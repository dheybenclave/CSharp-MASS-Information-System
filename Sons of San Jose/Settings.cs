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
namespace Sons_of_San_Jose
{
    public partial class Settings : Form
    {
        Db_Utilities db = new Db_Utilities();
        Point p = new Point();
        Point pp = new Point();
        DataTable dt = new DataTable();
        MySqlCommand cmd;
        MySqlDataAdapter adapt; 
        public static int _y, _x = 0;
        string cpassword ,user_id, lst_id, lst_username, lst_password, lst_position = "";
        public bool isdeleteuser,isnotcomplete = false;
        public static string id,ConnString = "";
        public Settings()
        {
            InitializeComponent();
            PositionObject();
            LoadImage();
            AllUser();
        }

        public void PositionObject()
        {
            p.X = _x;
            p.Y = _y;
            this.Location = p;
            LoadData();
            pnlconfirmation.Location = new Point(261, 69);
            p.X = (((pnlconfirmation.Width / 2) - (lblcusername.Width / 2)) + 5);
            p.Y = 307;
            lblcusername.Location = p;
            circle.Location = new Point(((pnlconfirmation.Width / 2) - (circle.Width / 2)), 42);
            lblnotif.Location = new Point((pnlconfirmation.Width / 2) - (lblnotif.Width / 2), 475);
            pnlchangeaccount.Visible = false;
        }

        public void LoadImage()
        {
            btnupdate.BackgroundImage = imageList1.Images[1];
            btnrightupdate.BackgroundImage = imageList1.Images[1];
            btnrightadd.BackgroundImage = imageList1.Images[4];
            btnrightdelete.BackgroundImage = imageList1.Images[5];
        }

        private void close_Click(object sender, EventArgs e)
        {
            if (isnotcomplete == true)
            {
                if (pnlchangeaccount.Enabled == false)
                {
                    timer1.Enabled = true;
                    timer1.Start();
                    icowarning.Visible = true;
                    lblwarning.Visible = true;
                    icowarning.BackgroundImage = imageList1.Images[3];
                    lblwarning.Text = "We need to complete our Database Connection! .";
                    buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Tomato;
                }
            }
            else { this.Close();  }
        }
        private void minimize_Click(object sender, EventArgs e)
        { this.WindowState = FormWindowState.Minimized; }
        public void LoadData()
        {
            dt = new DataTable();
            dt.Clear();
            adapt = new MySqlDataAdapter("SELECT * FROM user WHERE user_id ='" + id + "';", db.OpenConnection());
            adapt.Fill(dt);
            db.CloseConnection();

            foreach (DataRow d in dt.Rows)
            {
                lblcusername.Text = d[1].ToString();
                lblcposition.Text = d[3].ToString();
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (txtpassword.Text != "" && txtusername.Text != "")
            {
                string q = "UPDATE `dbms_mass`.`user` SET `user_name`='" + txtusername.Text + "', `user_password`='" + txtpassword.Text +
                                    "', `user_position`='" + lblposition.Text + "' WHERE `user_id`='" + user_id + "';";
                cmd = new MySqlCommand(q, db.OpenConnection());
                cmd.ExecuteNonQuery();
                db.CloseConnection();

                dt.Clear();
                adapt = new MySqlDataAdapter("SELECT * FROM user where user_id ='" + user_id + "' ;", db.OpenConnection());
                adapt.Fill(dt);
                db.CloseConnection();

                foreach (DataRow d in dt.Rows)
                {
                    user_id = d[0].ToString();
                    lblcusername.Text = lblusername.Text = d[1].ToString();
                    cpassword = lblpassword.Text =  d[2].ToString();
                    lblcposition.Text = d[3].ToString();
                }

                timer1.Enabled = true;
                timer1.Start();
                icowarning.Visible = true;
                lblwarning.Visible = true;
                icowarning.BackgroundImage = imageList1.Images[2];
                lblwarning.Text = "Update Account Success! .";
                buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.MediumVioletRed;
                txtusername.Text = txtpassword.Text = "";
                Confirmation();
            }
            else
            {
                timer1.Enabled = true;
                timer1.Start();
                icowarning.Visible = true;
                lblwarning.Visible = true;
                icowarning.BackgroundImage = imageList1.Images[3];
                lblwarning.Text = "Please Fiil All Details! .";
                buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Tomato;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "on")
            {
                button2.Text = "off";
                txtpassword.UseSystemPasswordChar = false;
            }
            else
            {
                button2.Text = "on";
                txtpassword.UseSystemPasswordChar = true;
            }

        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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

        private void txtpass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            { Confirmation(); }
        }
        public void Confirmation()
        {
            string q = "SELECT * FROM dbms_mass.user WHERE user_name ='" + lblcusername.Text + "'AND user_password ='" + txtpass.Text + "';";
            adapt = new MySqlDataAdapter(q, db.OpenConnection());
            dt.Clear();
            adapt.Fill(dt);
            db.CloseConnection();
            int s = 0;
            foreach (DataRow d in dt.Rows)
            {
                s = 1;
                user_id = d[0].ToString();
                cpassword = lblpassword.Text = d[2].ToString();
                lblcusername.Text = lblusername.Text = txtusername.Text = d[1].ToString();
                lblposition.Text = d[3].ToString();
                pnlconfirmation.Visible = false;
                pnlchangeaccount.Visible = true;
                tabcontrolright.Visible = true;
                if (d[3].ToString() == "Admin" || d[3].ToString() == "Coordinator" || d[3].ToString() == "Co-Coordinator") 
                    { tabcontrolright.Visible = true; }
                else { tabcontrolright.Visible = false; }
            }
            if (s != 1){ lblnotif.Visible = true; }

           

        }
        public void AllUser()
        {
            lstuser.Items.Clear();
            cmbrightposition.Items.Clear();
            adapt = new MySqlDataAdapter("SELECT * FROM dbms_mass.user ;", db.OpenConnection());
            dt = new DataTable();
            dt.Clear();
            adapt.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[3].ToString() != "Admin")
                {
                    ListViewItem itm = new ListViewItem(dr[0].ToString());
                    itm.SubItems.Add(dr[1].ToString());
                    itm.SubItems.Add(dr[2].ToString());
                    itm.SubItems.Add(dr[3].ToString());
                    lstuser.Items.Add(itm);
                }
            }

            string[] position = { "Coordinator", "Co-Coordinator", "Secretary", "Treasurer", "Commitee" };

            for (int i = 0; i < position.Length; i++)
            {
                cmbrightposition.Items.Add(position[i]);

            }
        }

        private void Header_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        { lblnotif.Visible = false; }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int seconds = Convert.ToInt32(lblnum.Text);
            lblnum.Text = Convert.ToString(seconds - 1);

            if (lblnum.Text == "0")
            {
                buttom.BackColor = Color.Honeydew;
                timer1.Stop();
                lblwarning.Visible = false;
                icowarning.Visible = false;
                lblnum.Text = "5";
            }
        }

        private void btnmasterlist_Click(object sender, EventArgs e)
        {

        }

        private void btnmasterlist_MouseEnter(object sender, EventArgs e)
        {

        }

        private void btnmasterlist_MouseLeave(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "on")
            {
                button3.Text = "off";
                txtrightpassword.UseSystemPasswordChar = false;
            }
            else
            {
                button3.Text = "on";
                txtrightpassword.UseSystemPasswordChar = true;
            }
        }

        private void lstuser_Click(object sender, EventArgs e)
        {
           
            lst_id = lstuser.FocusedItem.SubItems[0].Text;
            lst_username = lstuser.FocusedItem.SubItems[1].Text;
            lst_password = lstuser.FocusedItem.SubItems[2].Text;
            lst_position = lstuser.FocusedItem.SubItems[3].Text;

            txtrightusername.Text = lst_username;
            txtrightpassword.Text = lst_password;
            cmbrightposition.Text = lst_position;
         
          
        }

        private void btnrightadd_Click(object sender, EventArgs e)
        {
            string q = "SELECT * FROM user WHERE user_position ='" + cmbrightposition.Text + "' ;";
            adapt = new MySqlDataAdapter(q, db.OpenConnection());
            dt.Clear();
            adapt.Fill(dt);

            if (dt.Rows.Count != 0)
            {
                timer1.Enabled = true;
                timer1.Start();
                icowarning.Visible = true;
                lblwarning.Visible = true;
                icowarning.BackgroundImage = imageList1.Images[3];
                lblwarning.Text = cmbrightposition.Text + " has already Exist! .";
                buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Tomato;
            }
            else
            {

                if (txtrightusername.Text != "" && txtrightpassword.Text != "" && cmbrightposition.Text != "")
                {

                    if (txtrightusername.Text != " " || txtrightpassword.Text != " ")
                    {
                        cmd = new MySqlCommand("CreateUser", db.OpenConnection());
                        cmd.CommandType = CommandType.StoredProcedure;
                        try
                        {
                            cmd.Parameters.Add(new MySqlParameter("?user_name", txtrightusername.Text.Replace("'","''")));
                            cmd.Parameters.Add(new MySqlParameter("?user_password", txtrightpassword.Text.Replace("'", "''")));
                            cmd.Parameters.Add(new MySqlParameter("?user_position", cmbrightposition.Text.Replace("'", "''")));
                            cmd.ExecuteNonQuery();
                            db.CloseConnection();

                            timer1.Enabled = true;
                            timer1.Start();
                            icowarning.Visible = true;
                            lblwarning.Visible = true;
                            icowarning.BackgroundImage = imageList1.Images[3];
                            lblwarning.Text = "Add Account User Success! .";
                            buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Lime;
                            txtrightusername.Text = txtrightpassword.Text = cmbrightposition.Text = "";
                            AllUser();
                        }
                        catch { }
                    }
                }
                else
                {
                    timer1.Enabled = true;
                    timer1.Start();
                    icowarning.Visible = true;
                    lblwarning.Visible = true;
                    icowarning.BackgroundImage = imageList1.Images[3];
                    lblwarning.Text = "Please Fill All Details! .";
                    buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Tomato;
                }
            }
        }

        private void btnrightupdate_Click(object sender, EventArgs e)
        {

            string qq = "SELECT * FROM user WHERE user_position ='" + cmbrightposition.Text + "' ;";
            adapt = new MySqlDataAdapter(qq, db.OpenConnection());
            dt.Clear();
            adapt.Fill(dt);

            if (dt.Rows.Count != 0)
            {
                timer1.Enabled = true;
                timer1.Start();
                icowarning.Visible = true;
                lblwarning.Visible = true;
                icowarning.BackgroundImage = imageList1.Images[3];
                lblwarning.Text = cmbrightposition.Text + " has already Exist! .";
                buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Tomato;
                txtrightpassword.Text = txtrightusername.Text = cmbrightposition.Text = "";
            }
            else
            {
                if (txtrightpassword.Text != "" && txtrightusername.Text != "" && lst_id == "")
                {
                    timer1.Enabled = true;
                    timer1.Start();
                    icowarning.Visible = true;
                    lblwarning.Visible = true;
                    icowarning.BackgroundImage = imageList1.Images[2];
                    lblwarning.Text = "Please Select User to before Update .";
                    buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Tomato;
                    txtrightusername.Text = txtrightpassword.Text = cmbrightposition.Text = "";
                    lst_id = "";
                }
                if (txtrightpassword.Text != "" && txtrightusername.Text != "" && lst_id != "")
                {
                    string q = "UPDATE `dbms_mass`.`user` SET `user_name`='" + txtrightusername.Text.Replace("'", "''") +
                                "', `user_password`='" + txtrightpassword.Text.Replace("'", "''") + "', `user_position`='" + cmbrightposition.Text +
                                                                                                            "' WHERE `user_id`='" + lst_id + "';";
                    cmd = new MySqlCommand(q, db.OpenConnection());
                    cmd.ExecuteNonQuery();
                    db.CloseConnection();
                    AllUser();

                    timer1.Enabled = true;
                    timer1.Start();
                    icowarning.Visible = true;
                    lblwarning.Visible = true;
                    icowarning.BackgroundImage = imageList1.Images[2];
                    lblwarning.Text = "Update Account Success! .";
                    buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.MediumVioletRed;
                    txtrightusername.Text = txtrightpassword.Text = cmbrightposition.Text = "";
                    lst_id = "";
                }
                else
                {
                    timer1.Enabled = true;
                    timer1.Start();
                    icowarning.Visible = true;
                    lblwarning.Visible = true;
                    icowarning.BackgroundImage = imageList1.Images[3];
                    lblwarning.Text = "Please Fiil All Details! .";
                    buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Tomato;
                }
            }
        }

        private void btnrightdelete_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            icowarning.Visible = true;
            lblwarning.Visible = true;
            if (lst_id != null)
            {
                cmd = new MySqlCommand("DELETE FROM `dbms_mass`.`user` WHERE `user_id`='" + lst_id + "';", db.OpenConnection());
                cmd.ExecuteNonQuery();
                db.CloseConnection();
                icowarning.BackgroundImage = imageList1.Images[2];
                lblwarning.Text = "Delete Account Success! .";
                buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Lime;
                txtrightusername.Text = txtrightpassword.Text = cmbrightposition.Text = "";
                AllUser();
                lst_id = "";
            }
            else
            {
                icowarning.BackgroundImage = imageList1.Images[3];
                lblwarning.Text = "Please Select User to Delete! .";
                buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Tomato;
            }
        }

        private void btnconnect_Click(object sender, EventArgs e)
        {
            db.ifedit = 1;
            if (txtserver.Text == "" || txtport.Text == "" || txtdatabase.Text == "" || txtusernamedb.Text == "" || txtpassworddb.Text == "")
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
                db.username = txtusernamedb.Text;
                db.password = txtpassworddb.Text;
                try
                {
                    db.SaveSettings();
                    db.OpenConnection();
                    if (db.OpenConnection() != null)
                    {
                        lblnoitfconnect.Text = "Your Database Connection Success.";
                        lblnoitfconnect.Visible = true;
                        pnlchangeaccount.Enabled = true; pnlforadmin.Enabled = true;
                        isnotcomplete = false;
                    }
                    else
                    {
                        pnlchangeaccount.Enabled = pnlforadmin.Enabled = false;
                        isnotcomplete = true; lblnoitfconnect.Text = "Your Database Connection is not Valid.";
                    }
                    lblnoitfconnect.Visible = true;
                }
                catch
                {
                    isnotcomplete = true;
                    pnlchangeaccount.Enabled = pnlforadmin.Enabled = false;
                    lblnoitfconnect.Text = "Your Database Connection is not Valid";
                    lblnoitfconnect.Visible = true;
                }
            }
        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            if (btnshow.Text == "on")
            {
                btnshow.Text = "off";
                txtpassworddb.UseSystemPasswordChar = false;
            }
            else
            {
                btnshow.Text = "on";
                txtpassworddb.UseSystemPasswordChar = true;
            }
        }

        private void txtserver_TextChanged(object sender, EventArgs e)
        { lblnoitfconnect.Visible = false; }

        private void txtrightusername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != ' '))
            {
                e.Handled = true;

            }
        }

    }
}
