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
    public partial class Committee : Form
    {
        Point p = new Point();
        Db_Utilities db = new Db_Utilities();
        MySqlDataAdapter adapt = new MySqlDataAdapter();
        DataTable dt = new DataTable();
        MySqlCommand cmd;
        public int ifpresident = 0;
        string lst_id, lst_date, lst_notes, status = "";
        public Committee()
        {
            InitializeComponent();
            p.X = (Screen.PrimaryScreen.Bounds.Width / 2) - (this.Size.Width / 2);
            p.Y = (Screen.PrimaryScreen.Bounds.Height / 2) - (this.Size.Height / 2);
            this.Location = p;
            ComboBoxDate();
            LoadNotes("SELECT * FROM notes ORDER BY notes_date DESC");
            dtaddattendance.Value = DateTime.Now;
            btnupdate.BackgroundImage = imageList1.Images[1];
            btndelete.BackgroundImage = imageList1.Images[2];
            btnsecretary.BackgroundImage = imageList1.Images[5];
            btnsettings.BackgroundImage = imageList1.Images[6];
            btnaddnotes.BackgroundImage = imageList1.Images[7];

        }

        private void close_Click(object sender, EventArgs e)
        {
            if (ifpresident == 1) this.Close();
            else { ExitForm ex = new ExitForm(); ex.ShowDialog(); }
            ifpresident = 0;
        }

        private void Committee_Load(object sender, EventArgs e)
        {
            if (ifpresident == 1) { btnsettings.Visible = false; }
            else { btnsettings.Visible = true; }
        }

        private void minimize_Click(object sender, EventArgs e)
        { this.WindowState = FormWindowState.Minimized; }


        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lst_id != "")
                {
                    string q = "UPDATE `dbms_mass`.`notes` SET `notes_date`='" + dtaddattendance.Text + "', `notes_note`='" + rtfieldtext.Text
                        + "' WHERE `notes_id`='" + lst_id + "';";

                    cmd = new MySqlCommand(q, db.OpenConnection());
                    cmd.ExecuteNonQuery();
                    db.CloseConnection();


                    timer1.Enabled = true;
                    timer1.Start();
                    icowarning.Visible = true;
                    lblwarning.Visible = true;
                    icowarning.BackgroundImage = imageList1.Images[3];
                    lblwarning.Text = "Update Account Success! .";
                    buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.MediumVioletRed;
                    rtfieldtext.Text = "";
                    ComboBoxDate();
                    LoadNotes("SELECT * FROM notes ORDER BY notes_date DESC");
                    lst_id = "";
                }
                else
                {
                    timer1.Enabled = true;
                    timer1.Start();
                    icowarning.Visible = true;
                    lblwarning.Visible = true;
                    icowarning.BackgroundImage = imageList1.Images[4];
                    lblwarning.Text = "Please Fiil All Details! Correctly! .";
                    buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Tomato;
                }
            }
            catch { }

        }

        private void btnaddnotes_Click(object sender, EventArgs e)
        {

            try
            {
                cmd = new MySqlCommand("SaveNotes", db.OpenConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    cmd.Parameters.Add(new MySqlParameter("?notes_date",dtaddattendance.Value.ToString("yyyy-MM-dd") ));
                    cmd.Parameters.Add(new MySqlParameter("?notes_note", rtfieldtext.Text));
                    cmd.ExecuteNonQuery();
                    db.CloseConnection();
                    ComboBoxDate();
                    LoadNotes("SELECT * FROM notes ORDER BY notes_date DESC");
                    timer1.Enabled = true;
                    timer1.Start();
                    icowarning.Visible = lblwarning.Visible  = true;
                    icowarning.BackgroundImage = imageList1.Images[3];
                    lblwarning.Text = "Add Notes Success! .";
                    buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Lime;
    
                }
                catch (Exception ex) { }
            }
            catch (Exception ex) { }
        }

        public void ComboBoxDate()
        {
            try
            {
                cmbdate.Items.Clear();
                DataTable dt = new DataTable();
                dt.Clear();
                adapt = new MySqlDataAdapter("SELECT distinct(notes_date) FROM notes ORDER BY notes_date DESC;", db.OpenConnection());
                adapt.Fill(dt);
                db.CloseConnection();
                foreach (DataRow dr in dt.Rows)
                {
                    cmbdate.Items.Add(Convert.ToDateTime(dr[0].ToString()).ToString("yyyy-MM-dd"));
                }
                cmbdate.Items.Add("All");
                cmbdate.Text = "All";
                rddescending.Checked = true;
            }
            catch { }
        }


        public void LoadNotes(string query)
        {
            lstrightnotes.Items.Clear();
            dt = new DataTable();
            adapt = new MySqlDataAdapter(query, db.OpenConnection());
            adapt.Fill(dt);
            db.CloseConnection();

           // MessageBox.Show(query);
            foreach (DataRow rd in dt.Rows)
            {
                ListViewItem itm1 = new ListViewItem(rd[0].ToString());
                itm1.SubItems.Add(Convert.ToDateTime(rd[1].ToString()).ToString("MMMM dd, yyyy") + " - " + rd[2].ToString());
                lstrightnotes.Items.Add(itm1);
            }

            if (dt.Rows.Count == 0)
            {
                pnlempty.Visible = true;
            }
        }

        private void pnlempty_Click(object sender, EventArgs e)
        {
            rtfieldtext.Focus();
            pnlempty.Visible = false;
        }

        private void cmbdate_SelectedValueChanged(object sender, EventArgs e)
        {
            lbldateselected.Text = cmbdate.Text;
            rddescending.Checked = true;
            if (cmbdate.Text == "All") { LoadNotes("SELECT * FROM notes ORDER BY  notes_date " + status); grpstatus.Enabled = true; }
            else { LoadNotes("SELECT * FROM notes where notes_date ='" + cmbdate.Text + "' ORDER BY  notes_date " + status); grpstatus.Enabled = false; }
        }

        private void lstrightnotes_Click(object sender, EventArgs e)
        {
            try
            {
                lst_id = lstrightnotes.FocusedItem.SubItems[0].Text;
                lst_date = lstrightnotes.FocusedItem.SubItems[1].Text.Split('-')[0];
                lst_notes = lstrightnotes.FocusedItem.SubItems[1].Text.Split('-')[1];

               dtaddattendance.Text =  lst_date ;
               rtfieldtext.Text = lst_notes;
            }
            catch { }
        }

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

        private void rdasecending_CheckedChanged(object sender, EventArgs e)
        {
            if (rddescending.Checked == true) { status = "DESC"; }
            else { status = "ASC"; }

            if (cmbdate.Text == "All") { LoadNotes("SELECT * FROM notes ORDER BY notes_date  " + status); }
            else { LoadNotes("SELECT * FROM notes where notes_date ='" + cmbdate.Text + "' ORDER BY  notes_date "+status); }
        }

        private void cmbdate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lst_id != null)
                {
                    cmd = new MySqlCommand("DELETE FROM `dbms_mass`.`notes` WHERE `notes_id`='" + lst_id + "';", db.OpenConnection());
                    cmd.ExecuteNonQuery();
                    db.CloseConnection();
                    icowarning.BackgroundImage = imageList1.Images[3];

                    timer1.Enabled = true;
                    timer1.Start();
                    icowarning.Visible = true;
                    lblwarning.Visible = true;
                    icowarning.BackgroundImage = imageList1.Images[3];
                    lblwarning.Text = "Delete Account Success! .";
                    buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Lime;
                    rtfieldtext.Text = "";
                    ComboBoxDate();
                    LoadNotes("SELECT * FROM notes ORDER BY notes_date DESC");
                    lst_id = "";
                }
                else
                {
                    icowarning.BackgroundImage = imageList1.Images[4];
                    lblwarning.Text = "Please Select User to Delete! .";
                    buttom.BackColor = icowarning.BackColor = lblwarning.BackColor = Color.Tomato;
                }
            }
            catch { }
        }

        private void btnsettings_Click(object sender, EventArgs e)
        {
            Settings._y = this.Location.Y;
            Settings._x = this.Location.X;
            Settings s = new Settings();
            s.ShowDialog();
        }

        private void btnsettings_MouseEnter(object sender, EventArgs e)
        {
             lblstatus.Text = "Settings";
            btnsettings.BorderColor = Color.White;
            btnsettings.BackColor = Color.White;
        }

        private void btnsettings_MouseLeave(object sender, EventArgs e)
        {
            lblstatus.Text = "";
            btnsettings.BorderColor = Color.Green;
            btnsettings.BackColor = Color.SeaGreen;
        }
    }
}
